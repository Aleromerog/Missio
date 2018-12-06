using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;

namespace MissioServer.Services
{
    public interface IPostsService
    {
        IQueryable<Post> GetPosts(User user);
        IQueryable<StickyPost> GetStickyPosts();
        Task PublishPost(CreatePostDTO createPostDTO);
    }
}