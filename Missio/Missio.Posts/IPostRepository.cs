using System.Linq;
using System.Threading.Tasks;
using Missio.Users;

namespace Missio.Posts
{
    public interface IPostRepository
    {
        Task PublishPost(CreatePostDTO post);
        Task<IOrderedEnumerable<IPost>> GetMostRecentPostsInOrder(NameAndPassword nameAndPassword);
    }
}