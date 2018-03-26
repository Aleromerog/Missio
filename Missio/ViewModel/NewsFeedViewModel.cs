using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using Xamarin.Forms;

namespace ViewModel
{
    public class NewsFeedViewModel : INewsFeedViewPosts, IUpdateViewPosts
    {
        private readonly INewsFeedPostsUpdater _postsUpdater;
        private readonly IGoToView _goToView;

        [UsedImplicitly]
        public string Title { get; } = "News feed page";

        [UsedImplicitly]
        public ICommand UpdatePostsCommand { get; }

        [UsedImplicitly]
        public ICommand GoToPublicationPageCommand { get; }

        [UsedImplicitly]
        public ObservableCollection<NewsFeedPost> Posts { get; } = new ObservableCollection<NewsFeedPost>();

        public NewsFeedViewModel([NotNull] INewsFeedPostsUpdater postsUpdater, [NotNull] IOnUserLoggedIn onUserLoggedIn, [NotNull] IGoToView goToView)
        {
            _postsUpdater = postsUpdater ?? throw new ArgumentNullException(nameof(postsUpdater));
            _goToView = goToView ?? throw new ArgumentNullException(nameof(goToView));
            if (onUserLoggedIn == null)
                throw new ArgumentNullException(nameof(onUserLoggedIn));
            UpdatePostsCommand = new Command(UpdatePosts);
            onUserLoggedIn.OnUserLoggedIn += UpdatePosts;
            GoToPublicationPageCommand = new Command(async() => await GoToPublicationPage());
        }

        public void UpdatePosts()
        {
            _postsUpdater.UpdatePosts(Posts);
        }

        private Task GoToPublicationPage()
        {
            return _goToView.GoToView("Publication page");
        }
    }
}