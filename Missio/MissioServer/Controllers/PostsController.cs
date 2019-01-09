using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using MissioServer.Services;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly UsersService _userService;
        private readonly PostsService _postsService;
        private readonly NotificationsService _notificationsService;

        public PostsController(UsersService userService, PostsService postsService, NotificationsService notificationsService)
        {
            _userService = userService;
            _postsService = postsService;
            _notificationsService = notificationsService;
        }

        [HttpGet("getFriendsPosts/{userName}/{password}")]
        public async Task<List<Post>> GetFriendsNewsFeedPosts(string userName, string password)
        {
            var user = await _userService.GetUserIfValid(new NameAndPassword(userName, password));
            return _postsService.GetPosts(user).ToList();
        }

        [HttpGet("getStickyPosts")]
        public List<StickyPost> GetStickyPosts()
        {
            return _postsService.GetStickyPosts().ToList();
        }

        [HttpPost("publishPost/{createPostDTO}")]
        public async Task<ActionResult> PublishPost(CreatePostDTO createPostDTO)
        {
            await _postsService.PublishPost(createPostDTO);
            return Ok();
        }

        [HttpPost("likePost/{postId}/{userName}/{password}")]
        public async Task LikePost(int postId, string userName, string password)
        {
            var user = await _userService.GetUserIfValid(new NameAndPassword(userName, password));
            var post = await _postsService.GetPostById(postId);
            _postsService.AddUserToPostLikes(post, user);
            _notificationsService.NotifyPostLiked(post, user);
        }
    }
}