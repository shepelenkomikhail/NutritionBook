using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NutritionalRecipeBook.Domain;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IRepositoryFactory _repositoryFactory;
    private IDbContextTransaction? _transaction;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ApplicationDbContext context, IRepositoryFactory repositoryFactory)
    {
        _context = context;
        _repositoryFactory = repositoryFactory;
    }
    
    public IRepository<T, TId> Repository<T, TId>() where T : class, IBaseEntity<TId>
    {
        var type = typeof(T);

        if (_repositories.TryGetValue(type, out var existingRepository))
        {
            return (IRepository<T, TId>)existingRepository;
        }

        var repository = _repositoryFactory.GetRepository<T, TId>();
        _repositories[type] = repository;

        return repository;
    }

    public async Task<bool> SaveAsync()
    {
        var transactionActive = _transaction != null;

        if (!transactionActive)
            _transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.SaveChangesAsync();

            if (!transactionActive)
            {
                await _transaction!.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            return true;
        }
        catch (DbUpdateException ex)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            foreach (var entry in ex.Entries)
                entry.State = EntityState.Detached;

            return false;
        }
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public void Dispose() => _context.Dispose();
}