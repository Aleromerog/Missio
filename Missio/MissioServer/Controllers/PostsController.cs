using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Missio.Posts;
using MissioServer.Services.Services;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostsService _postsService;

        public PostsController(IUserService userService, IPostsService postsService)
        {
            _userService = userService;
            _postsService = postsService;
        }

        public async Task<ActionResult<List<IPost>>> GetNewsFeedPosts(string userName, string password)
        {
            var user = await _userService.GetUserIfValid(userName, password);
            return (await _postsService.GetPosts(user)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult> PublishPost(CreatePostDTO createPostDTO)
        {
            return Ok();
        }
    }
}