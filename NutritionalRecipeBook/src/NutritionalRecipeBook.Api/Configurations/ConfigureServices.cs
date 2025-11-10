using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.Services;
using NutritionalRecipeBook.Infrastructure.Contracts;
using NutritionalRecipeBook.Infrastructure.Repositories;

namespace NutritionalRecipeBook.Api.Configurations
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this WebApplicationBuilder builder, IConfiguration config)
        {
            var services = builder.Services;
            
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRecipeService, RecipeService>();    
            services.AddScoped<IIngredientService, IngredientService>();
            
            return services;
        }
    }
}