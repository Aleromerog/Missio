using System.Collections.Generic;
using Missio.Posts;

namespace Missio.LocalDatabase
{
    public interface IPostRepository
    {
        /// <summary>
        /// Gets the most recent posts to display on the News Feed page
        /// </summary>
        /// <returns> A list containing the news feed posts </returns>
        List<IPost> GetMostRecentPostsInOrder();

        void PublishPost(IPost post);
    }
}