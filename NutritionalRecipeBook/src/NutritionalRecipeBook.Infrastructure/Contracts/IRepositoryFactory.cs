using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Infrastructure.Contracts;

public interface IRepositoryFactory
{
    IRepository<T, TId> GetRepository<T, TId>() where T : class, IBaseEntity<TId>;
}