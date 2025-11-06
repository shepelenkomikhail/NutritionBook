using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Infrastructure.Contracts;

public interface IRepositoryFactory
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
}