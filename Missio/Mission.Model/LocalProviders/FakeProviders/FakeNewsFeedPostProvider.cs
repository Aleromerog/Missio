using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    /// <summary>
    /// A news feed provider that returns hard coded data
    /// </summary>
    public class FakeNewsFeedPostProvider : INewsFeedPositionProvider
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
}