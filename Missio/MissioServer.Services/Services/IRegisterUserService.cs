using System.Threading.Tasks;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public interface IRegisterUserService
    {
        Task RegisterUser(CreateUserDTO createUserDTO);
    }
}