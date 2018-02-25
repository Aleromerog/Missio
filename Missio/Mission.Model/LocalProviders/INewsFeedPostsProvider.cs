using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    public interface INewsFeedPostsProvider
    {
        /// <summary>
        /// Gets the most recent posts to display on the News Feed page
        /// </summary>
        /// <param name="user"> The logged in user </param>
        /// <returns> A list containing the news feed posts </returns>
        ObservableCollection<NewsFeedPost> GetMostRecentPosts(User user);
    }

    public enum UserValidationResult
    {
        IncorrectUsername,
        IncorrectPassword,
        Succeeded
    }

    /// <summary>
    /// Validates that the given user information is correct
    /// </summary>
    public interface IUserValidator
    {
        /// <summary>
        /// Validates that the user given user information is correct
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserValidationResult IsUserValid(User user);
    }
}