using System.Linq;
using System.Threading.Tasks;
using Missio.Posts;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public interface IPostsService
    {
        Task<IQueryable<Post>> GetPosts(User user);
        IQueryable<StickyPost> GetStickyPosts();
        Task PublishPost(CreatePostDTO createPostDTO);
    }
}