using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.EntityConfigurations;

public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
{
    public void Configure(EntityTypeBuilder<ShoppingList> builder)
    {
        builder.ToTable("ShoppingLists");
        builder.HasKey(sl => sl.Id);
        builder.Property(sl => sl.Id).ValueGeneratedOnAdd();
        
        builder.HasIndex(sl => sl.UserId)
            .IsUnique();
    }
}
