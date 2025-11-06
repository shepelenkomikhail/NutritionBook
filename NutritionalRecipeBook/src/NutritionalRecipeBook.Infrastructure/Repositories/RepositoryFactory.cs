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

    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new Repository<T>(_context);
    }
}