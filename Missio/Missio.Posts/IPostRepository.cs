using System.Linq;
using System.Threading.Tasks;

namespace Missio.Posts
{
    public interface IPostRepository
    {
        Task PublishPost(CreatePostDTO post);
        Task<IOrderedEnumerable<IPost>> GetMostRecentPostsInOrder(string userName, string password);
    }
}