using NutritionalRecipeBook.Domain;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Infrastructure.Repositories
{
    public class DummyRepository : IDummyRepository
    {
        public Task<BaseEntity> GetEntity(Guid id)
        {
            return Task.FromResult(new BaseEntity() { Id = id });
        }
    }
}