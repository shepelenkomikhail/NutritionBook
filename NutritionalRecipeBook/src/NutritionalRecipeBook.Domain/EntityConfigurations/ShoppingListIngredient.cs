using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.EntityConfigurations
{
    public class ShoppingListIngredientConfiguration : IEntityTypeConfiguration<ShoppingListIngredient>
    {
        public void Configure(EntityTypeBuilder<ShoppingListIngredient> builder)
        {
            builder.ToTable("ShoppingListIngredients");
            builder.HasKey(sli => new { sli.ShoppingListId, sli.IngredientId });

            builder.HasOne(sli => sli.ShoppingList)
                .WithMany(sl => sl.ShoppingListIngredients)
                .HasForeignKey(sli => sli.ShoppingListId);

            builder.HasOne(sli => sli.Ingredient)
                .WithMany(i => i.ShoppingListIngredients)
                .HasForeignKey(sli => sli.IngredientId);

            builder.Property(sli => sli.Unit)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(sli => sli.Amount)
                .HasPrecision(10, 2);
        }
    }
}