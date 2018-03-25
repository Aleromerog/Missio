using System;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private NewsFeedViewModel NewsFeedViewModel;
        private INewsFeedPostsUpdater NewsFeedPostsUpdater;
        private IOnUserLoggedIn OnUserLoggedIn;
        private IGoToNextPage GoToPublicationPage;

        [SetUp]
        public void SetUp()
        {
            NewsFeedPostsUpdater = Substitute.For<INewsFeedPostsUpdater>();
            OnUserLoggedIn = Substitute.For<IOnUserLoggedIn>();
            GoToPublicationPage = Substitute.For<IGoToNextPage>();
            NewsFeedViewModel = new NewsFeedViewModel(NewsFeedPostsUpdater, OnUserLoggedIn, GoToPublicationPage);
        }

        [Test]
        public void OnUserLoggedIn_GivenUser_UpdatesPosts()
        {
            //Arrange
            //Act
            OnUserLoggedIn.OnUserLoggedIn += Raise.Event<Action>();
            //Assert
            NewsFeedPostsUpdater.Received(1).UpdatePosts(NewsFeedViewModel.Posts);
        }

        [Test]
        public void UpdatePosts_GivenPosts_UpdatesPosts()
        {
            //Arrange
            //Act
            NewsFeedViewModel.UpdatePosts();
            //Assert
            NewsFeedPostsUpdater.Received(1).UpdatePosts(NewsFeedViewModel.Posts);
        }

        [Test]
        public void GoToPublicationPage_NormalCall_GoesToPublicationPage()
        {
            //Arrange

            //Act
            NewsFeedViewModel.GoToPublicationPage();
            //Assert
            GoToPublicationPage.Received(1).GoToNextPage();
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            //Arrange
            
            //Act
            NewsFeedViewModel.GoToPublicationPageCommand.Execute(null);
            //Assert
            GoToPublicationPage.Received(1).GoToNextPage();
    }
}
}