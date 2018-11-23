using System.Threading.Tasks;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public interface IUserService
    {
        Task<User> GetUserIfValid(NameAndPassword nameAndPassword);
        Task<UserFriends> GetFriends(User user);
    }
}