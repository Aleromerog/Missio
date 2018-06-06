using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace Mission.Model.Services
{
    /// <summary>
    /// Updates a collection of news feed posts
    /// </summary>
    public interface INewsFeedPostsUpdater
    {
        /// <summary>
        /// Updates the collection of posts with the most recent posts
        /// </summary>
        void UpdatePosts(ObservableCollection<NewsFeedPost> posts);
    }
}