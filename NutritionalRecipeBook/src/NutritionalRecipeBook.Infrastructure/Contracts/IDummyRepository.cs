using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Infrastructure.Contracts
{
    public interface IDummyRepository
    {
        public Task<BaseEntity> GetEntity(Guid id);
    }
}