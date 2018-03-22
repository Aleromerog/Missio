using System.Collections.Generic;
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
        List<NewsFeedPost> GetMostRecentPosts(User user);
    }
}