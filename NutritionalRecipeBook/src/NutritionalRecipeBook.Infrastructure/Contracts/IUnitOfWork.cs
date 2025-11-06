using Microsoft.EntityFrameworkCore.Storage;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Infrastructure.Contracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<bool> SaveAsync();
    IDbContextTransaction BeginTransaction();
}