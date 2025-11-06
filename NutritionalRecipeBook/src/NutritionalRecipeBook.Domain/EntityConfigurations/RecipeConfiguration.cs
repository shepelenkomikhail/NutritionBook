using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.EntityConfigurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Instructions)
                .HasMaxLength(10000)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(1000);
            
            builder.HasMany(r => r.Comments)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}