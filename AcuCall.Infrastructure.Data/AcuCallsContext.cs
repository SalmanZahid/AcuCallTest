using AcuCall.Core.Objects;
using AcuCall.Infrastructure.Data.DbMapping;
using AcuCall.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcuCall.Infrastructure.Data
{
    public class AcuCallsContext : DbContext, Interfaces.IAcuCallsContext
    {
        public AcuCallsContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> Sessions { get; set; }

        public DbContext Context => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapUser();
            modelBuilder.MapUserSession();
        }
    }
}
