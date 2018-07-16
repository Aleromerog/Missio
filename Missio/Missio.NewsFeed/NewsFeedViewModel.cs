using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.LogIn;
using Missio.Posts;
using Mission.ViewModel;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.NewsFeed
{
    public class NewsFeedViewModel<TPublicationPage> : ViewModel, IUpdateViewPosts where TPublicationPage : Page
    {
        [UsedImplicitly]
        public ICommand UpdatePostsCommand { get; }

        [UsedImplicitly]
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetField(ref _isRefreshing, value);
        }

        [UsedImplicitly]
        public ICommand GoToPublicationPageCommand { get; }

        [UsedImplicitly]
        public ObservableCollection<IPost> Posts { get; } = new ObservableCollection<IPost>();

        private readonly IGetMostRecentPosts _getMostRecentPosts;
        private readonly INavigation _navigation;
        private bool _isRefreshing;

        public NewsFeedViewModel([NotNull] IGetMostRecentPosts getMostRecentPosts, [NotNull] IOnUserLoggedIn onUserLoggedIn, [NotNull] INavigation navigation)
        {
            _getMostRecentPosts = getMostRecentPosts ?? throw new ArgumentNullException(nameof(getMostRecentPosts));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            if (onUserLoggedIn == null)
                throw new ArgumentNullException(nameof(onUserLoggedIn));
            UpdatePostsCommand = new Command(UpdatePosts);
            onUserLoggedIn.OnUserLoggedIn += UpdatePosts;
            GoToPublicationPageCommand = new Command(async() => await GoToPublicationPage());
        }

        public void UpdatePosts()
        {
            Posts.Clear();
            foreach (var post in _getMostRecentPosts.GetMostRecentPostsInOrder())
                Posts.Add(post);
            IsRefreshing = false;
        }

        private async Task GoToPublicationPage()
        {
            await _navigation.GoToPage<TPublicationPage>();
        }
    }
}