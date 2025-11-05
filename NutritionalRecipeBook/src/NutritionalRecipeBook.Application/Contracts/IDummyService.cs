using NutritionalRecipeBook.Domain;

namespace NutritionalRecipeBook.Application.Contracts
{
    public interface IDummyService
    {
        public Task<BaseEntity> GetDummy(Guid id);
    }
}