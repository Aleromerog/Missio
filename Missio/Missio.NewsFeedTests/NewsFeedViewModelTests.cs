using System;
using System.Collections.Generic;
using Missio.LogIn;
using Missio.NewsFeed;
using Missio.Posts;
using NSubstitute;
using NUnit.Framework;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private NewsFeedViewModel<PublicationPage> _newsFeedViewModel;
        private IGetMostRecentPosts _fakeGetMostRecentPosts;
        private IOnUserLoggedIn _onUserLoggedIn;
        private INavigation _fakeNavigation;

        [SetUp]
        public void SetUp()
        {
            _fakeGetMostRecentPosts = Substitute.For<IGetMostRecentPosts>();
            _onUserLoggedIn = Substitute.For<IOnUserLoggedIn>();
            _fakeNavigation = Substitute.For<INavigation>();
            _newsFeedViewModel = new NewsFeedViewModel<PublicationPage>(_fakeGetMostRecentPosts, _onUserLoggedIn, _fakeNavigation);
        }

        [Test]
        public void UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            //Arrange
            _fakeGetMostRecentPosts.GetMostRecentPostsInOrder().Returns(new List<IPost>());
            _newsFeedViewModel.IsRefreshing = true;
            //Act
            _newsFeedViewModel.UpdatePosts();
            //Assert
            Assert.IsFalse(_newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            //Arrange
            
            //Act
            _newsFeedViewModel.GoToPublicationPageCommand.Execute(null);
            //Assert
            _fakeNavigation.Received(1).GoToPage<PublicationPage>();
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.ExtraPosts))]
        public void OnUserLoggedIn_UserLoggedIn_UpdatesPosts(List<IPost> postsToAdd)
        {
            //Arrange
            _fakeGetMostRecentPosts.GetMostRecentPostsInOrder().Returns(postsToAdd);
            //Act
            _onUserLoggedIn.OnUserLoggedIn += Raise.Event<Action>();
            //Assert
            Assert.That(_newsFeedViewModel.Posts, Is.EquivalentTo(postsToAdd));
        }
    }
}