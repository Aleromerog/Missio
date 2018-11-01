﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Missio.NewsFeed;
using Missio.PostPublication;
using Missio.Posts;
using Missio.Users;
using NSubstitute;
using NUnit.Framework;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private static NewsFeedViewModel MakeNewsFeedViewModel(INavigation navigation = null)
        {
            if(navigation == null)
                navigation = Substitute.For<INavigation>();
            var fakeRepository = Substitute.For<IPostRepository>();
            var orderedPosts = new List<IPost> { Utils.MakeDummyPost() }.OrderByDescending(x => x.GetPostPriority());
            fakeRepository.GetMostRecentPostsInOrder("Francisco Greco", "ElPass").Returns(orderedPosts);
            return new NewsFeedViewModel(fakeRepository, navigation, MakeLoggedInUser());
        }

        private static ILoggedInUser MakeLoggedInUser()
        {
            var loggedInUser = Substitute.For<ILoggedInUser>();
            loggedInUser.UserName.Returns("Francisco Greco");
            loggedInUser.Password.Returns("ElPass");
            return loggedInUser;
        }

        [Test]
        public async Task UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel();
            newsFeedViewModel.IsRefreshing = true;

            await newsFeedViewModel.UpdatePosts();

            Assert.IsFalse(newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            var fakeNavigation = Substitute.For<INavigation>();
            var newsFeedViewModel = MakeNewsFeedViewModel(fakeNavigation);

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            fakeNavigation.Received(1).GoToPage<PublicationPage>();
        }

        [Test]
        public void Constructor_NormalConstructor_UpdatesPosts()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel();

            Assert.AreEqual(1, newsFeedViewModel.Posts.Count);
        }
    }
}