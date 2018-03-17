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

    /// <summary>
    /// Updates a collection of news feed posts
    /// </summary>
    public interface INewsFeedPostsUpdater
    {
        /// <summary>
        /// Updates the collection of posts with the most recent posts
        /// </summary>
        /// <param name="posts">The collection to update </param>
        void UpdatePosts(ObservableCollection<NewsFeedPost> posts);
    }
}