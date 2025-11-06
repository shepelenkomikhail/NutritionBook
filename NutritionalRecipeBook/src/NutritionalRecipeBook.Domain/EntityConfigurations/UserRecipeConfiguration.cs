using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.EntityConfigurations
{
    public class UserRecipeConfiguration : IEntityTypeConfiguration<UserRecipe>
    {
        public void Configure(EntityTypeBuilder<UserRecipe> builder)
        {
            builder.ToTable("UserRecipes");
            builder.HasKey(ur => new { ur.UserId, ur.RecipeId });
            
            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRecipes)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(ur => ur.Recipe)
                .WithMany(r => r.UserRecipes)
                .HasForeignKey(ur => ur.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}