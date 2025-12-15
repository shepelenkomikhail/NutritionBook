using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(u => u.ShoppingList)
                .WithOne(sl => sl.User)
                .HasForeignKey<ShoppingList>(sl => sl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}