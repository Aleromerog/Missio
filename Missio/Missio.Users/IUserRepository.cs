using System.Threading.Tasks;

namespace Missio.Users
{
    public interface IUserRepository
    {
        Task AttemptToRegisterUser(RegistrationDTO registrationDto);

        /// <summary>
        /// Validates that the user given user information is correct
        /// </summary>
        /// <returns></returns>
        Task<User> GetUserIfValid(string userName, string password);
    }
}