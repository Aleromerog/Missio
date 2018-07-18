using Missio.LogIn;
using Missio.Navigation;
using Missio.NewsFeed;
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
        private IPublishPost _fakePublishPost;
        private IGetLoggedInUser _fakeGetLoggedInUser;
        private INavigation _fakeNavigation;
        private IUpdateViewPosts _fakeUpdatePostsView;

        [SetUp]
        public void SetUp()
        {
            _fakePublishPost = Substitute.For<IPublishPost>();
            _fakeGetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            _fakeNavigation = Substitute.For<INavigation>();
            _fakeUpdatePostsView = Substitute.For<IUpdateViewPosts>();
            _publicationPageViewModel = new PublicationPageViewModel(_fakePublishPost, _fakeGetLoggedInUser, _fakeUpdatePostsView, _fakeNavigation);
        }

        [Test]
        public void PublishPost_TextOnly_UpdatesPostsView()
        {
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));

            _publicationPageViewModel.PublishPostCommand.Execute(null);

            _fakeUpdatePostsView.Received(1).UpdatePosts();
        }

        [Test]
        public void PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));

            _publicationPageViewModel.PublishPostCommand.Execute(null);

            _fakeNavigation.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public void PublishPostCommand_TextOnly_PublishesPost()
        {
            var authorName = "Name of the author";
            var newPostText = "The content of the new post";
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User(authorName, ""));
            _publicationPageViewModel.PostText = newPostText;

            _publicationPageViewModel.PublishPostCommand.Execute(null);

            _fakePublishPost.Received(1).PublishPost(Arg.Is<TextOnlyPost>(x => x.Message == newPostText && x.AuthorName == authorName));
        }
    }
}