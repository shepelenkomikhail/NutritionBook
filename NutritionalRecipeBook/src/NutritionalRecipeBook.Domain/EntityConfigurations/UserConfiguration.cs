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
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();            

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasIndex(u => u.Username)
                .IsUnique();
            
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