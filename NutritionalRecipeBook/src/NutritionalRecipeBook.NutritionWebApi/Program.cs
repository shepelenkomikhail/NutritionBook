using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using NutritionalRecipeBook.NutritionWebApi.Context;
using NutritionalRecipeBook.NutritionWebApi.Contracts;
using NutritionalRecipeBook.NutritionWebApi.Models;
using NutritionalRecipeBook.NutritionWebApi.Services;

namespace NutritionalRecipeBook.NutritionWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;
            const string dataNutrientsJson = "Data/nutrients.json";
            var baseUrl = builder.Configuration["App:ApiUrl"];
            
            if (jwtSettings == null)
            {
                throw new Exception("JWT SETTINGS ARE NULL");
            }
            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(jwtSettings.SigningKey))
            {
                throw new InvalidOperationException(
                    "Configuration is missing: 'App:ApiUrl'. "
                );
            }
            
            var nutrientsFilePath = Path.Combine(builder.Environment.ContentRootPath, dataNutrientsJson);
            Nutrient[] nutrients;
            if (File.Exists(nutrientsFilePath))
            {
                var json = File.ReadAllText(nutrientsFilePath);
                nutrients = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.NutrientArray)
                            ?? Array.Empty<Nutrient>();
            }
            else
            {
                nutrients = Array.Empty<Nutrient>();
            }
            
            builder.Services.AddSingleton(nutrients);
            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddSingleton<IJwtService, JwtService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddTransient<IEmailSenderService, EmailSender>();
            builder.Services.AddSingleton<IUserService, UserService>();
            
            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain
                    .Insert(0, AppJsonSerializerContext.Default);
            });
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };
                });
            
            
            
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            
            var app = builder.Build();
             
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
              
            app.Run();
          }
      }
  }
