using System.Collections.Generic;

namespace Missio.NewsFeed
{
    public interface IGetMostRecentPosts
    {
        /// <summary>
        /// Gets the most recent posts to display on the News Feed page
        /// </summary>
        /// <returns> A list containing the news feed posts </returns>
        List<NewsFeedPost> GetMostRecentPostsInOrder();
    }
}