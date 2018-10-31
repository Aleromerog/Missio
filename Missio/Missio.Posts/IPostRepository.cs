using System.Collections.Generic;
using System.Threading.Tasks;

namespace Missio.Posts
{
    public interface IPostRepository
    {
        Task PublishPost(CreatePostDTO post);
        Task<List<IPost>> GetMostRecentPostsInOrder(string userName, string password);
    }
}