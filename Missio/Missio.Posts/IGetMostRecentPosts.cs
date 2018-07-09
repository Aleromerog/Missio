using System.Collections.Generic;
using Missio.Posts;

namespace Missio.NewsFeed
{
    public interface IGetMostRecentPosts
    {
        /// <summary>
        /// Gets the most recent posts to display on the News Feed page
        /// </summary>
        /// <returns> A list containing the news feed posts </returns>
        List<IPost> GetMostRecentPostsInOrder();
    }
}