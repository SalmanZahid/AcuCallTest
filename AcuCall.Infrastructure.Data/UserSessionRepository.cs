using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using AcuCall.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcuCall.Infrastructure.Data
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly Interfaces.IAcuCallsContext _dbContext;
        public UserSessionRepository(Interfaces.IAcuCallsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddSessionAsync(UserSession session)
        {
            _dbContext.Sessions.Add(session);
            await _dbContext.SaveChangesAsync();
            return session.SessionId;
        }

        public async Task<UserSession> GetAsync(int sessionId)
        {
            return await _dbContext.Sessions.FindAsync(sessionId);
        }

        public Task<List<UserSession>> GetSessionsFromRangeAsync(DateTime from, DateTime to)
        {

            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UserSession session)
        {
            _dbContext.Sessions.Update(session);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
