using AcuCall.Core.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcuCall.Core.Interfaces
{
    public interface IUserSessionRepository
    {
        Task<UserSession> GetAsync(int sessionId);
        Task<List<UserSession>> GetSessionsFromRangeAsync(DateTime from, DateTime to);
        Task<int> AddSessionAsync(UserSession session);
        Task<bool> UpdateAsync(UserSession session);
    }
}
