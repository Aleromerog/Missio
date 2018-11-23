using System.Threading.Tasks;

namespace Missio.Users
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