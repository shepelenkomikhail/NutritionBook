using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NutritionalRecipeBook.Domain;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Infrastructure.Repositories;

public class Repository<T, TId> : IRepository<T, TId> where T : class, IBaseEntity<TId>
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>();
    }
    
    public IQueryable<T> GetWhereIf(IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? source.AsNoTracking().Where(predicate) : source;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
    }
    
    public async Task<bool> InsertAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return true;
    }
    
    public async Task<bool> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return await Task.FromResult(true);
    }
    
    public async Task<bool> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entityToDelete = await GetByIdAsync(id);
        if (entityToDelete != null)
        {
            return await DeleteAsync(entityToDelete);
        }
        return false;
    }
}