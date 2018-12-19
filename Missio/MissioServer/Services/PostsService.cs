using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace MissioServer.Services
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
        public IQueryable<Post> GetPosts(User user)
        {
            var userFriends = _userService.GetFriends(user);
            return _missioContext.Posts.Where(post => userFriends.Contains(post.Author) || post.Author == user).Include(x => x.Comments).Include(x => x.Author);
        }

        public IQueryable<StickyPost> GetStickyPosts()
        {
            return _missioContext.StickyPosts;
        }

        /// <inheritdoc />
        public async Task PublishPost(CreatePostDTO createPostDTO)
        {
            var user = await _userService.GetUserIfValid(createPostDTO.NameAndPassword);
            _missioContext.Posts.Add(new Post(user, createPostDTO.Message, _timeService.GetCurrentTime(), createPostDTO.Picture));
            _missioContext.SaveChanges();
        }
    }
}