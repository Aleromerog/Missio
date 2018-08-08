using System.Collections.Generic;
using System.Linq;
using Missio.Posts;
using Missio.Users;

namespace Missio.LocalDatabase
{
    /// <summary>
    /// A news feed provider that returns hard coded data
    /// </summary>
    public class LocalNewsFeedPostRepository : IPostRepository
    {
        /// <summary>
        /// Maps users to collections of news feed posts that should be displayed to them (different users see different posts)
        /// </summary>
        private readonly Dictionary<User, List<IPost>> _usersNewsFeedPosts =
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

        /// <summary>
        /// Gets a list of manually hardcoded news feed posts
        /// </summary>
        /// <returns> A list containing news feed posts</returns>
        public List<IPost> GetMostRecentPostsInOrder(User user)
        {
            if (_usersNewsFeedPosts.TryGetValue(user, out var posts))
                return posts.OrderByDescending(x => x.GetPostPriority()).ToList();
            return new List<IPost>();
        }

        /// <inheritdoc/>
        public void PublishPost(User user, IPost post)
        {
            _usersNewsFeedPosts[user].Insert(0, post);
        }
    }
}