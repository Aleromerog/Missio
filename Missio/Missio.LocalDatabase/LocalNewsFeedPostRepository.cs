using System.Collections.Generic;
using System.Linq;
using Missio.LogIn;
using Missio.Posts;
using Missio.Users;

namespace Missio.LocalDatabase
{
    /// <summary>
    /// A news feed provider that returns hard coded data
    /// </summary>
    public class LocalNewsFeedPostRepository : IPostRepository
    {
        private readonly IGetLoggedInUser _getLoggedInUser;

        /// <summary>
        /// Maps users to collections of news feed posts that should be displayed to them (different users see different posts)
        /// </summary>
        private static readonly Dictionary<User, List<IPost>> UsersNewsFeedPosts =
            new Dictionary<User, List<IPost>>
            {
                {
                    new User("Jorge Romero", "Yolo"),
                    new List<IPost>
                    {
                        new StickyPost("Super important news", "A sticky message for user zero"),
                        new TextOnlyPost("Francisco Greco", "Hello Jorge Romero"),
                        new TextOnlyPost("Jorge Romero", "Hello me")
                    }
                },
                {
                    new User("Francisco Greco", "ElPass"),
                    new List<IPost>
                    {
                        new StickyPost("Super important news", "A sticky message for user one"),
                        new TextOnlyPost("Francisco Greco", "Hello me"),
                        new TextOnlyPost("Jorge Romero", "Hello Greco")
                    }
                },
            };

        public LocalNewsFeedPostRepository(IGetLoggedInUser getLoggedInUser)
        {
            _getLoggedInUser = getLoggedInUser;
        }

        /// <summary>
        /// Gets the contents of the most recent posts as strings, useful for testing 
        /// </summary>
        /// <param name="user"> The user logged in </param>
        /// <returns> A list of strings containing the contents of the posts </returns>
        public static List<string> GetMostRecentPostsAsStrings(User user)
        {
            var posts = UsersNewsFeedPosts[user].Where(x => x is IMessage).Cast<IMessage>();
            var contents = new List<string>();
            foreach (var post in posts)
            {
                contents.Add(post.Message);
            }
            return contents;
        }

        /// <summary>
        /// Gets a list of manually hardcoded news feed posts
        /// </summary>
        /// <returns> A list containing news feed posts</returns>
        public List<IPost> GetMostRecentPostsInOrder()
        {
            if (UsersNewsFeedPosts.TryGetValue(_getLoggedInUser.LoggedInUser, out var posts))
            {
                return posts.OrderByDescending(x => x.GetPostPriority()).ToList();
            }

            return new List<IPost>();
        }

        public void SetMostRecentPosts(List<IPost> newPosts)
        {
            UsersNewsFeedPosts[_getLoggedInUser.LoggedInUser] = newPosts;
        }

        /// <inheritdoc />
        public void PublishPost(IPost post)
        {
            UsersNewsFeedPosts[_getLoggedInUser.LoggedInUser].Insert(0, post);
        }
    }
}