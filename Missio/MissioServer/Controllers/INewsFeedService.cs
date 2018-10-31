using System.Collections.Generic;
using Missio.Posts;
using Missio.Users;

namespace MissioServer.Controllers
{
    public interface INewsFeedService
    {
        List<Post> GetNewsFeedPosts(User user);
    }
}