using System.Threading.Tasks;
using Domain;

namespace MissioServer.Services
{
    public interface IUserService
    {
        Task<User> GetUserIfValid(NameAndPassword nameAndPassword);
        Task<UserFriends> GetFriends(User user);
    }
}