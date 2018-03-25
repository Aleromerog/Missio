using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using Ninject;
using Xamarin.Forms;

namespace ViewModel
{
    public class NewsFeedViewModel : INewsFeedViewPosts
    {
        private readonly INewsFeedPostsUpdater _postsUpdater;
        private readonly IGoToNextPage _goToPublicationPage;

        [UsedImplicitly]
        public ICommand UpdatePostsCommand { get; }

        [UsedImplicitly]
        public ICommand GoToPublicationPageCommand { get; }

        [UsedImplicitly]
        public ObservableCollection<NewsFeedPost> Posts { get; } = new ObservableCollection<NewsFeedPost>();

        public NewsFeedViewModel([NotNull] INewsFeedPostsUpdater postsUpdater, [NotNull] IOnUserLoggedIn onUserLoggedIn, [Named("GoToPublicationPage"), NotNull] IGoToNextPage goToPublicationPage)
        {
            _postsUpdater = postsUpdater ?? throw new ArgumentNullException(nameof(postsUpdater));
            _goToPublicationPage = goToPublicationPage ?? throw new ArgumentNullException(nameof(goToPublicationPage));
            if (onUserLoggedIn == null)
                throw new ArgumentNullException(nameof(onUserLoggedIn));
            UpdatePostsCommand = new Command(UpdatePosts);
            onUserLoggedIn.OnUserLoggedIn += UpdatePosts;
            GoToPublicationPageCommand = new Command(GoToPublicationPage);
        }

        public void UpdatePosts()
        {
            _postsUpdater.UpdatePosts(Posts);
        }

        public async void GoToPublicationPage()
        {
            await _goToPublicationPage.GoToNextPage();
        }
    }
}