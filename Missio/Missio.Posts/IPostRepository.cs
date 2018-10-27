using System.Collections.Generic;
using Missio.Users;

namespace Missio.Posts
{
    public interface IPostRepository
    {
        void PublishPost(IPost post);
        List<IPost> GetMostRecentPostsInOrder(User user);
    }
}