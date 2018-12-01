using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using MissioServer.Controllers;
using MissioServer.Services;
using NUnit.Framework;

namespace MissioServer.Tests
{
    [TestFixture]
    public class PostsControllerTests
    {
        private static PostsController MakePostsController(MissioContext missioContext = null)
        {
            if(missioContext == null)
                missioContext = Utils.MakeMissioContext();
            var usersService = new UsersService(missioContext, new MockPasswordService());
            return new PostsController(usersService, new PostsService(missioContext, usersService, new FakeTimeService()));
        }

        [Test]
        public async Task GetFriendsNewsFeedPosts_GivenUser_ReturnsPosts()
        {
            var postsController = MakePostsController();

            var posts = (await postsController.GetFriendsNewsFeedPosts("Francisco Greco", "ElPass")).Value;

            Assert.AreEqual(3, posts.Count);
        }

        [Test]
        public void GetStickyPosts_GivenUser_ReturnsPosts()
        {
            var postsController = MakePostsController();

            var posts = postsController.GetStickyPosts().Value;

            Assert.AreEqual(1, posts.Count);
        }

        [Test]
        public async Task PublishPost_GivenPost_PublishesPost()
        {
            var missioContext = Utils.MakeMissioContext();
            var grecoUser = missioContext.Users.First(x => x.UserName == "Francisco Greco");
            var createPostDTO = new CreatePostDTO(new NameAndPassword("Francisco Greco", "ElPass"), "A new message", null);
            var postsController = MakePostsController(missioContext);

            await postsController.PublishPost(createPostDTO);

            Assert.IsTrue(missioContext.Posts.Any(x => x.Author == grecoUser && x.Message == "A new message" && x.PublishedDate == new DateTime(2018, 9, 2)));
        }
    }
}