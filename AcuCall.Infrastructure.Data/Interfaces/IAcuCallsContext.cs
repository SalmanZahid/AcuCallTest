using AcuCall.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace AcuCall.Infrastructure.Data.Interfaces
{
    public interface IAcuCallsContext
    {
        DbSet<User> Users { get; set; }

        DbSet<UserSession> Sessions { get; set; }

        DatabaseFacade Database { get; }

        DbContext Context { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
