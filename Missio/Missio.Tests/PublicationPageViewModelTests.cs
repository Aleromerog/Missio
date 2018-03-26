using Mission.Model.Data;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class PublicationPageViewModelTests
    {
        private PublicationPageViewModel PublicationPageViewModel;
        private IPublishPost FakePublishPost;
        private IGetLoggedInUser FakeGetLoggedInUser;
        private IReturnToPreviousPage FakeReturnOnePage;
        private IUpdateViewPosts FakeUpdatePostsView;

        [SetUp]
        public void SetUp()
        {
            FakePublishPost = Substitute.For<IPublishPost>();
            FakeGetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            FakeReturnOnePage = Substitute.For<IReturnToPreviousPage>();
            FakeUpdatePostsView = Substitute.For<IUpdateViewPosts>();
            PublicationPageViewModel = new PublicationPageViewModel(FakePublishPost, FakeGetLoggedInUser, FakeUpdatePostsView, FakeReturnOnePage);
        }

        [Test]
        public void PublishPost_TextOnly_UpdatesPostsView()
        {
            //Arrange
            FakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));
            //Act
            PublicationPageViewModel.PublishPostCommand.Execute(null);
            //Assert
            FakeUpdatePostsView.Received(1).UpdatePosts();
        }

        [Test]
        public void PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            //Arrange
            FakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));
            //Act
            PublicationPageViewModel.PublishPostCommand.Execute(null);
            //Assert
            FakeReturnOnePage.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public void PublishPostCommand_TextOnly_PublishesPost()
        {
            //Arrange
            var authorName = "Name of the author";
            var newPostText = "The content of the new post";
            FakeGetLoggedInUser.LoggedInUser.Returns(new User(authorName, ""));
            PublicationPageViewModel.PostText = newPostText;
            //Act
            PublicationPageViewModel.PublishPostCommand.Execute(null);
            //Assert
            FakePublishPost.Received(1).PublishPost(Arg.Is<TextOnlyPost>(x => x.Text == newPostText && x.Author == authorName));
        }
    }
}