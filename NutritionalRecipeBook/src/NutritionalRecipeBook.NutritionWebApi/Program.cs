using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
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
            var baseUrl = builder.Configuration["App:ApiUrl"];
            var apiKey = builder.Configuration["Gemini:ApiKey"];
            
            var nutrientsPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "nutrients.json");
            if (!File.Exists(nutrientsPath))
            {
                throw new FileNotFoundException($"Nutrients file not found: {nutrientsPath}");
            }

            var nutrientsJson = File.ReadAllText(nutrientsPath);
            var nutrients = JsonSerializer
                .Deserialize<List<Nutrient>>(nutrientsJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Nutrient>();
            
            if (jwtSettings == null)
            {
                throw new Exception("JWT SETTINGS ARE NULL");
            }
            if (string.IsNullOrWhiteSpace(baseUrl) || 
                string.IsNullOrWhiteSpace(jwtSettings.SigningKey) || 
                string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "Some configuration is missing"
                );
            }
            
            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddSingleton<IEnumerable<Nutrient>>(nutrients);
 
            builder.Services.AddSingleton<IGeminiService>(sp => new GeminiService(apiKey));            
            builder.Services.AddSingleton<INutrientsService, NutrientsService>();
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
