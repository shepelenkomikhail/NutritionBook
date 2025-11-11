using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NutritionalRecipeBook.Domain;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Api.Configurations;

public static class ConfigureIdentity
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
