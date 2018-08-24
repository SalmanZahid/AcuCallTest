using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcuCall.Core.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _repository;

        public UserSessionService(IUserSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddSession(string username)
        {
            UserSession session = new UserSession();
            session.Username = username;
            session.Login_DateTime = DateTime.Now;

            return await _repository.AddSessionAsync(session);
        }

        public async Task<List<UserSession>> GetSessionsFromRange(DateTime from, DateTime to)
        {
            return await _repository.GetSessionsFromRangeAsync(from, to);
        }

        public async Task<bool> Logout(int sessionId)
        {
            var session = await _repository.GetAsync(sessionId);

            if (session == null) return false;

            session.Logout_DateTime = DateTime.Now;
            return await _repository.UpdateAsync(session);
        }
    }
}
