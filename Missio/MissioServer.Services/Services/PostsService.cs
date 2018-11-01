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
        private readonly ITimeService _timeService;

        public PostsService(MissioContext missioContext, IUserService userService, ITimeService timeService)
        {
            _userService = userService;
            _timeService = timeService;
            _missioContext = missioContext;
        }

        /// <inheritdoc />
        public async Task<IQueryable<Post>> GetPosts(User user)
        {
            var userFriends = await _userService.GetFriends(user);
            return _missioContext.Posts.Where(post => userFriends.Friends.Contains(post.Author) || post.Author == user);
        }

        public IQueryable<StickyPost> GetStickyPosts()
        {
            return _missioContext.StickyPosts;
        }

        /// <inheritdoc />
        public async Task PublishPost(CreatePostDTO createPostDTO)
        {
            var user = await _userService.GetUserIfValid(createPostDTO.UserName, createPostDTO.Password);
            _missioContext.Posts.Add(new Post(user, createPostDTO.Message, _timeService.GetCurrentTime(), createPostDTO.Picture));
            _missioContext.SaveChanges();
        }
    }
}