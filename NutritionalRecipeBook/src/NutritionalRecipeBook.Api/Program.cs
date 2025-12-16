using Microsoft.EntityFrameworkCore;
using NutritionalRecipeBook.Api.Configurations;
using NutritionalRecipeBook.Domain;
using Serilog;
using QuestPDF.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Application is starting up...");

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());
    
    var config = builder.Configuration;
    
    builder.AddServices(config);
    
    builder.Services.AddIdentityConfiguration(config);
    builder.Services.AddTokenConfiguration(config);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Frontend", policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:3000",
                    "https://nutrition-book.shepelenkomykhailo.com",
                    "https://www.nutrition-book.shepelenkomykhailo.com"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
        
    });

    
    QuestPDF.Settings.License = LicenseType.Community;
    
    var app = builder.Build();
    
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();
    app.UseHttpsRedirection();
    
    app.UseRouting();
    
    app.UseCors("Frontend");
    app.Use(async (context, next) =>
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        context.Response.Headers["Access-Control-Allow-Methods"] = "*";
        context.Response.Headers["Access-Control-Allow-Headers"] = "*";

        await next();
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}