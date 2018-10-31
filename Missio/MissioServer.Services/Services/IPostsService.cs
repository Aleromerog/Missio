using System.Linq;
using System.Threading.Tasks;
using Missio.Posts;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public interface IPostsService
    {
        Task<IQueryable<IPost>> GetPosts(User user);
        Task PublishPost(CreatePostDTO createPostDTO);
    }
}