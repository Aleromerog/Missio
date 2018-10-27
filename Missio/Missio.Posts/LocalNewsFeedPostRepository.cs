using System.Collections.Generic;
using System.Net;
using Missio.Users;

namespace Missio.Posts
{
    /// <summary>
    /// A news feed provider that returns hard coded data
    /// </summary>
    public class LocalNewsFeedPostRepository : IPostRepository
    {
        /// <summary>
        /// Maps users to collections of news feed posts that should be displayed to them (different users see different posts)
        /// </summary>
        private readonly List<IPost> _newsFeedPosts = new List<IPost>
            {
                new StickyPost("Super important news", "A sticky message for user zero"),
                new Post(new User("Francisco Greco"), "Hello Jorge Romero"),
                new Post(new User("Jorge Romero"), "Hello me"),
                new StickyPost("Super important news", "A sticky message for user one"),
                new Post(new User("Francisco Greco"), "Hello me"),
                new Post(new User("Francisco Greco"), "Hello Greco")
            };

        public LocalNewsFeedPostRepository()
        {
            using(var webClient = new WebClient())
            {
                var anaGaxiola = new User("Ana Gaxiola", "HashedPassword", webClient.DownloadData("https://scontent.fntr6-1.fna.fbcdn.net/v/t1.0-9/36355412_2372799529413493_5210179261469556736_n.jpg?_nc_cat=0&oh=43acb1611fbedb33963ced1540d79c94&oe=5BFC87A5"));
                var postDeAna = new Post(anaGaxiola, "Hola amigos", webClient.DownloadData("https://scontent.fntr6-1.fna.fbcdn.net/v/t1.0-9/36355412_2372799529413493_5210179261469556736_n.jpg?_nc_cat=0&oh=43acb1611fbedb33963ced1540d79c94&oe=5BFC87A5"));
                _newsFeedPosts.Add(postDeAna);
            }
        }

        /// <summary>
        /// Gets a list of manually hardcoded news feed posts
        /// </summary>
        /// <returns> A list containing news feed posts</returns>
        public List<IPost> GetMostRecentPostsInOrder(User user)
        {
            _newsFeedPosts.Sort((x, y) => x.GetPostPriority().CompareTo(y.GetPostPriority()));
            return _newsFeedPosts;
        }

        /// <inheritdoc/>
        public void PublishPost(IPost post)
        {
            _newsFeedPosts.Insert(0, post);
        }
    }
}