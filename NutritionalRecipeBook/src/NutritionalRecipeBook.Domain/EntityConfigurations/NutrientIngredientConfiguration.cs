using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.EntityConfigurations
{
    public class NutrientIngredientConfiguration : IEntityTypeConfiguration<NutrientIngredient>
    {
        public void Configure(EntityTypeBuilder<NutrientIngredient> builder)
        {
            builder.ToTable("NutrientIngredients"); 
            builder.HasKey(ni => new { ni.NutrientId, ni.IngredientId });

            builder.HasOne(ni => ni.Nutrient)
                .WithMany(n => n.NutrientIngredients)
                .HasForeignKey(ni => ni.NutrientId);

            builder.HasOne(ni => ni.Ingredient)
                .WithMany(i => i.NutrientIngredients)
                .HasForeignKey(ni => ni.IngredientId);
        }
    }
}