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
                    new User("Jorge Romero", "Yolo", "https://scontent.felp1-1.fna.fbcdn.net/v/t1.0-9/26168930_10208309305130065_9014358028033259242_n.jpg?_nc_cat=0&oh=a6dc6203053aa3c830edffd107f346e4&oe=5BF1FC2B"),
                    new List<IPost>
                    {
                        new StickyPost("Super important news", "A sticky message for user zero"),
                        new TextOnlyPost("Francisco Greco", "Hello Jorge Romero"),
                        new TextOnlyPost("Francisco Greco", "Hello Jorge Romero"),
                        new TextOnlyPost("Jorge Romero", "Hello me"),
                        new TextAndImagePost(new User("Ana Gaxiola","AmoAJorge", "https://scontent.fntr6-1.fna.fbcdn.net/v/t1.0-9/36355412_2372799529413493_5210179261469556736_n.jpg?_nc_cat=0&oh=43acb1611fbedb33963ced1540d79c94&oe=5BFC87A5"),
                                            "Hola amigos", "https://scontent.fntr6-1.fna.fbcdn.net/v/t1.0-9/39442197_2476652019028243_8630219233457864704_n.jpg?_nc_cat=0&oh=49664b70ae34ad2d95b88533209d9a19&oe=5C0280F4" )
                        
                    }
                },
                {
                    new User("Francisco Greco", "ElPass", "https://scontent.felp1-1.fna.fbcdn.net/v/t1.0-9/26168930_10208309305130065_9014358028033259242_n.jpg?_nc_cat=0&oh=a6dc6203053aa3c830edffd107f346e4&oe=5BF1FC2B"),
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