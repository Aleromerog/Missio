using System.Linq;
using System.Threading.Tasks;
using Missio.Posts;
using MissioServer.Controllers;
using MissioServer.Services.Services;
using NUnit.Framework;

namespace MissioServer.Tests
{
    [TestFixture]
    public class PostsControllerTests
    {
        private static PostsController MakePostsController()
        {
            var missioContext = Utils.MakeMissioContext();
            var usersService = new UsersService(missioContext, new MockPasswordService());
            return new PostsController(usersService, new PostsService(missioContext, usersService));
        }

        [Test]
        public async Task GetPosts_GivenUser_ReturnsPosts()
        {
            var postsController = MakePostsController();

            var posts = (await postsController.GetNewsFeedPosts("Francisco Greco", "ElPass")).Value;

            Assert.AreEqual(2, posts.Count);
        }

        [Test]
        public async Task PublishPost_GivenPost_PublishesPost()
        {
            var missioContext = Utils.MakeMissioContext();
            var grecoUser = missioContext.Users.First(x => x.UserName == "Francisco Greco");
            var createPostDTO = new CreatePostDTO("Francisco Greco", "ElPass", "A new message", null);
            var postsController = MakePostsController();

            await postsController.PublishPost(createPostDTO);

            Assert.IsTrue(missioContext.Posts.Any(x => x.Author == grecoUser && x.Message == "A new message"));
        }
    }
}