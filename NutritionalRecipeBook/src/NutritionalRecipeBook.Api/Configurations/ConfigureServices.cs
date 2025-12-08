using Microsoft.AspNetCore.Identity.UI.Services;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.Services;
using NutritionalRecipeBook.Infrastructure.Contracts;
using NutritionalRecipeBook.Infrastructure.Repositories;
using System;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentsService, CommentService>();
            services.AddScoped<INutrientService, NutrientService>();
            services.AddScoped<IShoppingListService, ShoppingListService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IJWTService, JWTService>();

            services.AddHttpClient<INutrientService, NutrientService>(client =>
            {
                client.BaseAddress = new Uri(config["NutrientsApi:BaseUrl"]!);
            });
            
            return services;
        }
    }
}