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

            //TODO: Add your DI configuration
            services.AddTransient<IDummyService, DummyService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}