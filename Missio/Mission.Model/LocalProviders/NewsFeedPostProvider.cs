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
            return new ObservableCollection<NewsFeedPost>
            {
                new StickyPost("Super important news", "A sticky message"), 
                new TextOnlyPost("Francisco Greco", "Hola"), 
                new TextOnlyPost("Jorge Romero", "Hola x2"), 
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