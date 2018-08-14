using System.Threading.Tasks;
using Missio.Users;

namespace Missio.LocalDatabase
{
    public interface IUserRepository
    {
        void AttemptToRegisterUser(User user);

        /// <summary>
        /// Validates that the user given user information is correct
        /// </summary>
        /// <param name="user"> The user info to validate </param>
        /// <returns></returns>
        void ValidateUser(User user);

        Task<User> GetUserByName(string userName);
    }
}