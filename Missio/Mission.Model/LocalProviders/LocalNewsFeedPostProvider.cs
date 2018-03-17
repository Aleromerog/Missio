using System.Collections.Generic;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    /// <summary>
    /// A news feed provider that returns hard coded data
    /// </summary>
    public class LocalNewsFeedPostProvider : INewsFeedPostsProvider
    {
        /// <summary>
        /// Maps users to collections of news feed posts that should be displayed to them (different users see different posts)
        /// </summary>
        private static readonly Dictionary<User, List<NewsFeedPost>> UsersNewsFeedPosts =
            new Dictionary<User, List<NewsFeedPost>>
            {
                {
                    LocalUserDatabase.ValidUsers[0],
                    new List<NewsFeedPost>
                    {
                        new StickyPost("Super important news", "A sticky message for user zero"),
                        new TextOnlyPost("Francisco Greco", "Hello Jorge Romero"),
                        new TextOnlyPost("Jorge Romero", "Hello me")
                    }
                },
                {
                    LocalUserDatabase.ValidUsers[1],
                    new List<NewsFeedPost>
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
        public List<NewsFeedPost> GetMostRecentPosts(User user)
        {
            return UsersNewsFeedPosts[user];
        }

        public void SetMostRecentPosts(User user, List<NewsFeedPost> newPosts)
        {
            UsersNewsFeedPosts[user] = newPosts;
        }
    }
}