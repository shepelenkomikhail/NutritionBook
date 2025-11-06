using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services
{
    public class DummyService : IDummyService
    {
        private readonly IDummyRepository _dummyRepository;

        public DummyService(IDummyRepository dummyRepository) 
        {
            _dummyRepository = dummyRepository;
        }

        public async Task<BaseEntity> GetDummy(Guid id)
        {
            return await _dummyRepository.GetEntity(id);
        }
    }
}