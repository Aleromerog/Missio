using System.Linq;
using System.Threading.Tasks;
using Domain.DataTransferObjects;

namespace Domain.Repositories
{
    public interface IPostRepository
    {
        Task PublishPost(CreatePostDTO post);
        Task<IOrderedEnumerable<IPost>> GetMostRecentPostsInOrder(NameAndPassword nameAndPassword);
    }
}