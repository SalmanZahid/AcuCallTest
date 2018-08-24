using AcuCall.Core.Objects;
using Microsoft.EntityFrameworkCore;

namespace AcuCall.Infrastructure.Data.DbMapping
{
    public static class UserMap
    {
        public static ModelBuilder MapUser(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();
            entity.ToTable("Users");

            entity.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(b => b.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(b => b.Password)
                .HasMaxLength(100)
                .IsRequired();

            entity.Ignore(c => c.IsDirty);

            return modelBuilder;
        }
    }
}
