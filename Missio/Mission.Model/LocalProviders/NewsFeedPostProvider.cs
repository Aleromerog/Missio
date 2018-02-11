using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    public class NewsFeedPostProvider : INewsFeedPositionProvider
    {
        /// <summary>
        /// Gets a list of manually hardcoded news feed posts
        /// </summary>
        /// <returns> A list containing news feed posts</returns>
        public ObservableCollection<NewsFeedPost> GetMostRecentPosts()
        {
            return new ObservableCollection<NewsFeedPost>()
            {
                new NewsFeedPost(new PostAuthor("Jorge Romero"), "Some content 1", 10),
                new NewsFeedPost(new PostAuthor("Francisco Greco"), "Some content 2", 20),
                new NewsFeedPost(new PostAuthor("Mr Awesome"), "Some content 3", 30),
            };
        }
    }
    
    public interface INewsFeedPositionProvider
    {
        /// <summary>
        /// Gets the most recent posts to display on the News Feed page
        /// </summary>
        /// <returns> A list containing the news feed posts </returns>
        ObservableCollection<NewsFeedPost> GetMostRecentPosts();
    }
}