using System.Threading.Tasks;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.Navigation;
using Missio.NewsFeed;
using Missio.PostPublication;
using Missio.Posts;
using Missio.UserTests;
using NSubstitute;
using NUnit.Framework;

namespace Missio.PostPublicationTests
{
    [TestFixture]
    public class PublicationPageViewModelTests
    {
        private PublicationPageViewModel _publicationPageViewModel;
        private IPostRepository _postRepository;
        private ILoggedInUser _fakeLoggedInUser;
        private INavigation _fakeNavigation;
        private IUpdateViewPosts _fakeUpdatePostsView;

        [SetUp]
        public void SetUp()
        {
            _postRepository = new LocalNewsFeedPostRepository();
            _fakeLoggedInUser = Substitute.For<ILoggedInUser>();
            _fakeNavigation = Substitute.For<INavigation>();
            _fakeUpdatePostsView = Substitute.For<IUpdateViewPosts>();
            _publicationPageViewModel = new PublicationPageViewModel(_postRepository, _fakeLoggedInUser, _fakeUpdatePostsView, _fakeNavigation);
        }

        [Test]
        public async Task PublishPost_TextOnly_UpdatesPostsView()
        {
            _fakeLoggedInUser.LoggedInUser.Returns(UserTestUtils.FranciscoUser);

            await _publicationPageViewModel.PublishPost();

            _fakeUpdatePostsView.Received(1).UpdatePosts();
        }

        [Test]
        public async Task PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            _fakeLoggedInUser.LoggedInUser.Returns(UserTestUtils.FranciscoUser);

            await _publicationPageViewModel.PublishPost();

            await _fakeNavigation.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public async Task PublishPostCommand_TextOnly_PublishesPost()
        {
            var user = UserTestUtils.FranciscoUser;
            _fakeLoggedInUser.LoggedInUser.Returns(user);
            var newPostText = "The content of the new post";
            _publicationPageViewModel.PostText = newPostText;

            await _publicationPageViewModel.PublishPost();

            Assert.IsTrue(_postRepository.GetMostRecentPostsInOrder(user).Exists(x =>
            {
                if (x is TextOnlyPost post)
                    return post.Message == newPostText && post.AuthorName == user.UserName;
                return false;
            }));
        }
    }
}