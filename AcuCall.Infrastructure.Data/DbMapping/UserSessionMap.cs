using AcuCall.Core.Objects;
using Microsoft.EntityFrameworkCore;

namespace AcuCall.Infrastructure.Data.DbMapping
{
    public static class UserSessionMap
    {
        public static ModelBuilder MapUserSession(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserSession>();
            entity.ToTable("UserSessions");

            entity.Property(b => b.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(b => b.Login_DateTime)
                .IsRequired();

            entity.HasIndex(x => x.Login_DateTime);
            entity.HasIndex(x => x.Logout_DateTime);

            return modelBuilder;
        }
    }
}
