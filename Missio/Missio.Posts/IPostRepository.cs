using System.Collections.Generic;

namespace Missio.Posts
{
    public interface IPostRepository
    {
        void PublishPost(CreatePostDTO post);
        List<IPost> GetMostRecentPostsInOrder(string userName, string password);
    }
}