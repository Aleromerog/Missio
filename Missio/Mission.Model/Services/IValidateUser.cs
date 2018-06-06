using Mission.Model.Data;

namespace Mission.Model.Services
{
    /// <summary>
    /// Validates that the given user information is correct
    /// </summary>
    public interface IValidateUser
    {
        /// <summary>
        /// Validates that the user given user information is correct, if it is 
        /// </summary>
        /// <param name="user"> The user info to validate </param>
        /// <returns></returns>
        void ValidateUser(User user);
    }
}