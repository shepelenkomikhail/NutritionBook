using NutritionalRecipeBook.Domain;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Infrastructure.Repositories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly ApplicationDbContext _context;

    public RepositoryFactory(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<T, TId> GetRepository<T, TId>() where T : class, IBaseEntity<TId>
    {
        return new Repository<T, TId>(_context);
    }
}