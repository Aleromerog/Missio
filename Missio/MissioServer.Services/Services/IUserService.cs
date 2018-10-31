using System.Threading.Tasks;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public interface IUserService
    {
        Task<User> GetUserIfValid(string userName, string password);
        Task<UserFriends> GetFriends(User user);
    }
}