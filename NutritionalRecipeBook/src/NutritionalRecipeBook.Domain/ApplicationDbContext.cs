using Microsoft.EntityFrameworkCore;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain;

public class ApplicationDbContext: DbContext
{
    private readonly string _connectionString = string.Empty;
    
    public virtual DbSet<User> Users { get; set; } 
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

        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

        modelBuilder.Entity<NutrientIngredient>()
            .HasKey(ni => new { ni.NutrientId, ni.IngredientId });

        modelBuilder.Entity<ShoppingListIngredient>()
            .HasKey(sli => new { sli.ShoppingListId, sli.IngredientId });

        modelBuilder.Entity<UserRecipe>()
            .HasKey(ur => new { ur.UserId, ur.RecipeId });
    }
}