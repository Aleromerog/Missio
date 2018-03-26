using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    /// <summary>
    /// Adds a user to the database
    /// </summary>
    public interface IAddUser
    {
        /// <summary>
        /// Adds the given user to the database
        /// </summary>
        /// <param name="user"> The user to add </param>
        void AddUser(User user);
    }
}