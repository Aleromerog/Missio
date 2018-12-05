using System.Threading.Tasks;
using Domain.DataTransferObjects;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task AttemptToRegisterUser(CreateUserDTO createUserDTO);

        /// <summary>
        /// Validates that the user given user information is correct
        /// </summary>
        Task ValidateUser(NameAndPassword nameAndPassword);
    }
}