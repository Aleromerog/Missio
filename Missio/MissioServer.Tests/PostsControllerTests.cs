using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using MissioServer.Controllers;
using MissioServer.Services;
using NSubstitute;
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
            var usersService = new UsersService(missioContext, new MockPasswordService(), Substitute.For<IWebClientService>());
            var postsService = new PostsService(missioContext, usersService, new FakeTimeService());
            var notificationsService = new NotificationsService(missioContext);
            return new PostsController(usersService, postsService, notificationsService);
        }

        [Test]
        public async Task GetFriendsNewsFeedPosts_GivenUser_ReturnsPosts()
        {
            var postsController = MakePostsController();

            var posts = await postsController.GetFriendsNewsFeedPosts("Francisco Greco", "ElPass");

            Assert.AreEqual(3, posts.Count);
            Assert.IsTrue(posts.First(x => x.Id == -1).Comments.Count > 0);
            CollectionAssert.AllItemsAreNotNull(posts);
            CollectionAssert.AllItemsAreNotNull(posts.Select(x => x.Author));
        }

        [Test]
        public void GetStickyPosts_GivenUser_ReturnsPosts()
        {
            var postsController = MakePostsController();

            var posts = postsController.GetStickyPosts();

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

        [Test]
        public async Task LikePost_Should_LikePost()
        {
            var missioContext = Utils.MakeMissioContext();
            var post = missioContext.Posts.First(x => x.Id == -1);
            var postsController = MakePostsController(missioContext);

            await postsController.LikePost(-1, "Francisco Greco", "ElPass");

            Assert.AreEqual(1, post.Likes.Count);
        }

        [Test]
        public async Task LikePost_Should_SendNotification()
        {
            var missioContext = Utils.MakeMissioContext();
            var postsController = MakePostsController(missioContext);

            await postsController.LikePost(-3, "Francisco Greco", "ElPass");

            Assert.AreEqual(2, missioContext.PostLikedNotifications.Count());
        }
    }
}