using System.Threading.Tasks;
using Missio.Navigation;
using Missio.PostPublication;
using Missio.Posts;
using Missio.Users;
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
            _postRepository = Substitute.For<IPostRepository>();
            _fakeLoggedInUser = Substitute.For<ILoggedInUser>();
            _fakeNavigation = Substitute.For<INavigation>();
            _fakeUpdatePostsView = Substitute.For<IUpdateViewPosts>();
            _publicationPageViewModel = new PublicationPageViewModel(_postRepository, _fakeLoggedInUser, _fakeUpdatePostsView, _fakeNavigation);
        }

        [Test]
        public async Task PublishPost_TextOnly_UpdatesPostsView()
        {
            await _publicationPageViewModel.PublishPost();

            _fakeUpdatePostsView.Received(1).UpdatePosts();
        }

        [Test]
        public async Task PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            await _publicationPageViewModel.PublishPost();

            await _fakeNavigation.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public async Task PublishPostCommand_TextOnly_PublishesPost()
        {
            var userName = "Francisco Greco";
            var password = "ElPass";
            _fakeLoggedInUser.UserName.Returns(userName);
            _fakeLoggedInUser.Password.Returns(password);
            var newPostText = "The content of the new post";
            _publicationPageViewModel.PostText = newPostText;

            await _publicationPageViewModel.PublishPost();

            _postRepository.Received().PublishPost(Arg.Is<CreatePostDTO>(x => x.Message == newPostText && x.UserName == userName && x.Password == password));
        }
    }
}