using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.EntityConfigurations;

public class IngredientConfiguration: IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("Ingredients");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.Property(i => i.IsLiquid).IsRequired();
    }
}