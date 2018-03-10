using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    /// <summary>
    /// A news feed provider that returns hard coded data
    /// </summary>
    public class FakeNewsFeedPostProvider : INewsFeedPostsProvider
    {
        /// <summary>
        /// Maps users to collections of news feed posts that should be displayed to them (different users see different posts)
        /// </summary>
        private static readonly Dictionary<User, ObservableCollection<NewsFeedPost>> UsersNewsFeedPosts =
            new Dictionary<User, ObservableCollection<NewsFeedPost>>()
            {
                {
                    FakeUserValidator.ValidUsers[0],
                    new ObservableCollection<NewsFeedPost>
                    {
                        new StickyPost("Super important news", "A sticky message for user zero"),
                        new TextOnlyPost("Francisco Greco", "Hello Jorge Romero"),
                        new TextOnlyPost("Jorge Romero", "Hello me")
                    }
                },
                {
                    FakeUserValidator.ValidUsers[1],
                    new ObservableCollection<NewsFeedPost>
                    {
                        new StickyPost("Super important news", "A sticky message for user one"),
                        new TextOnlyPost("Francisco Greco", "Hello me"),
                        new TextOnlyPost("Jorge Romero", "Hello Greco")
                    }
                },
            };
        
        /// <summary>
        /// Gets the contents of the most recent posts as strings, useful for testing 
        /// </summary>
        /// <param name="user"> The user logged in </param>
        /// <returns> A list of strings containing the contents of the posts </returns>
        public static List<string> GetMostRecentPostsAsStrings(User user)
        {
            var posts = UsersNewsFeedPosts[user];
            var contents = new List<string>();
            foreach (var post in posts)
            {
                switch (post)
                {
                    case TextOnlyPost onlyPost:
                        contents.Add(onlyPost.Text);
                        break;
                    case StickyPost stickyPost:
                        contents.Add(stickyPost.Message);
                        break;
                }
            }
            return contents;
        }

        /// <summary>
        /// Gets a list of manually hardcoded news feed posts
        /// </summary>
        /// <returns> A list containing news feed posts</returns>
        public ObservableCollection<NewsFeedPost> GetMostRecentPosts(User user)
        {
            return UsersNewsFeedPosts[user];
        }
    }
}