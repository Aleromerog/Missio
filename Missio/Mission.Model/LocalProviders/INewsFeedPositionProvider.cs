using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    public interface INewsFeedPositionProvider
    {
        /// <summary>
        /// Gets the most recent posts to display on the News Feed page
        /// </summary>
        /// <returns> A list containing the news feed posts </returns>
        ObservableCollection<NewsFeedPost> GetMostRecentPosts();
    }

    public enum LogInAttemptResult
    {
        IncorrectUsername,
        IncorrectPassword,
        Succeeded
    }

    /// <summary>
    /// Validates that the given username and password are correct
    /// </summary>
    public interface IUserValidator
    {
        LogInAttemptResult IsDataCorrect(User user);
    }
}