using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.EntityConfigurations;

public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.ToTable("RecipeIngredients"); 
        builder.HasKey(ri => new { ri.RecipeId, ri.IngredientId });
        
        builder.HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId);

        builder.HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId);

        builder.Property(ri => ri.Unit)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(ri => ri.Amount)
            .HasPrecision(10, 2);
        
        builder.Ignore(ri => ri.Id);
    }
}