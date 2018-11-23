using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Missio.Posts;
using Missio.Users;
using MissioServer.Services.Services;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostsService _postsService;

        public PostsController(IUserService userService, IPostsService postsService)
        {
            _userService = userService;
            _postsService = postsService;
        }

        [HttpGet("getFriendsPosts/{userName}/{password}")]
        public async Task<ActionResult<List<Post>>> GetFriendsNewsFeedPosts(string userName, string password)
        {
            var user = await _userService.GetUserIfValid(new NameAndPassword(userName, password));
            return (await _postsService.GetPosts(user)).ToList();
        }

        [HttpGet("getStickyPosts")]
        public ActionResult<List<StickyPost>> GetStickyPosts()
        {
            return _postsService.GetStickyPosts().ToList();
        }

        [HttpPost]
        public async Task<ActionResult> PublishPost(CreatePostDTO createPostDTO)
        {
            await _postsService.PublishPost(createPostDTO);
            return Ok();
        }
    }
}