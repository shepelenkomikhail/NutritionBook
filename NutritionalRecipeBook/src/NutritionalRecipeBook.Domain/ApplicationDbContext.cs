using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Domain.EntityConfigurations;

namespace NutritionalRecipeBook.Domain;

public class ApplicationDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly string _connectionString = string.Empty;
    
    public virtual DbSet<Recipe> Recipes { get; set; } 
    public virtual DbSet<Ingredient> Ingredients { get; set; } 
    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
    public virtual DbSet<Nutrient> Nutrients { get; set; }
    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public virtual DbSet<NutrientIngredient> NutrientIngredients { get; set; }
    public virtual DbSet<ShoppingListIngredient> ShoppingListIngredients { get; set; }
    public virtual DbSet<UserRecipe> UserRecipe { get; set; }
    
    public ApplicationDbContext()
    {
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new NutrientConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingListConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserRecipeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientConfiguration());
        modelBuilder.ApplyConfiguration(new NutrientIngredientConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingListIngredientConfiguration());
    }
}