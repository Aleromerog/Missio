using System.Threading.Tasks;
using Domain.DataTransferObjects;

namespace MissioServer.Services
{
    public interface IRegisterUserService
    {
        Task RegisterUser(CreateUserDTO createUserDTO);
    }
}