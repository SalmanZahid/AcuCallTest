using System.Threading.Tasks;

namespace AcuCall.Core.Interfaces
{
    public interface IUserSessionService
    {
        Task<int> AddSession(string username);
        Task<bool> Logout(int sessionId);
    }
}
