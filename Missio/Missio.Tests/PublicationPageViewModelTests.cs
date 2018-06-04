using Mission.Model.Data;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class PublicationPageViewModelTests
    {
        private PublicationPageViewModel _publicationPageViewModel;
        private IPublishPost _fakePublishPost;
        private IGetLoggedInUser _fakeGetLoggedInUser;
        private IReturnToPreviousPage _fakeReturnOnePage;
        private IUpdateViewPosts _fakeUpdatePostsView;

        [SetUp]
        public void SetUp()
        {
            _fakePublishPost = Substitute.For<IPublishPost>();
            _fakeGetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            _fakeReturnOnePage = Substitute.For<IReturnToPreviousPage>();
            _fakeUpdatePostsView = Substitute.For<IUpdateViewPosts>();
            _publicationPageViewModel = new PublicationPageViewModel(_fakePublishPost, _fakeGetLoggedInUser, _fakeUpdatePostsView, _fakeReturnOnePage);
        }

        [Test]
        public void PublishPost_TextOnly_UpdatesPostsView()
        {
            //Arrange
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));
            //Act
            _publicationPageViewModel.PublishPostCommand.Execute(null);
            //Assert
            _fakeUpdatePostsView.Received(1).UpdatePosts();
        }

        [Test]
        public void PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            //Arrange
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));
            //Act
            _publicationPageViewModel.PublishPostCommand.Execute(null);
            //Assert
            _fakeReturnOnePage.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public void PublishPostCommand_TextOnly_PublishesPost()
        {
            //Arrange
            var authorName = "Name of the author";
            var newPostText = "The content of the new post";
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User(authorName, ""));
            _publicationPageViewModel.PostText = newPostText;
            //Act
            _publicationPageViewModel.PublishPostCommand.Execute(null);
            //Assert
            _fakePublishPost.Received(1).PublishPost(Arg.Is<TextOnlyPost>(x => x.Message == newPostText && x.AuthorName == authorName));
        }
    }
}