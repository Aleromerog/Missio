using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace MissioServer.Services
{
    public interface IUserService
    {
        Task<User> GetUserIfValid(NameAndPassword nameAndPassword);
        IQueryable<User> GetFriends(User user);
    }
}