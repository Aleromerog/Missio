using System.Linq;
using System.Threading.Tasks;
using Missio.Posts;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public class PostsService : IPostsService
    {
        private readonly MissioContext _missioContext;
        private readonly IUserService _userService;

        public PostsService(MissioContext missioContext, IUserService userService)
        {
            _userService = userService;
            _missioContext = missioContext;
        }

        /// <inheritdoc />
        public async Task<IQueryable<IPost>> GetPosts(User user)
        {
            var userFriends = await _userService.GetFriends(user, user);
            return _missioContext.Posts.Where(post => userFriends.Friends.Contains(post.Author));
        }

        /// <inheritdoc />
        public async Task PublishPost(CreatePostDTO createPostDTO)
        {
            var user = await _userService.GetUserIfValid(createPostDTO.UserName, createPostDTO.Password);
            _missioContext.Posts.Add(new Post(user, createPostDTO.Message, createPostDTO.Picture));
            _missioContext.SaveChanges();
        }
    }
}