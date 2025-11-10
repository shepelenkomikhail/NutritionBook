using Microsoft.EntityFrameworkCore.Storage;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Infrastructure.Contracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<T, TId> Repository<T, TId>() where T : class, IBaseEntity<TId>;
    Task<bool> SaveAsync();
    IDbContextTransaction BeginTransaction();
}