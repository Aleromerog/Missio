using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using Xamarin.Forms;

namespace ViewModel
{
    public class LocalNewsFeedPostsUpdater : INewsFeedPostsUpdater
    {
        private readonly IGetLoggedInUser _getLoggedInUser;
        private readonly INewsFeedPostsProvider _postsProvider;

        public LocalNewsFeedPostsUpdater(IGetLoggedInUser getLoggedInUser, INewsFeedPostsProvider postsProvider)
        {
            _getLoggedInUser = getLoggedInUser;
            _postsProvider = postsProvider;
        }
        
        /// <inheritdoc />
        public void UpdatePosts(ObservableCollection<NewsFeedPost> posts)
        {
            posts.Clear();
            foreach (var post in _postsProvider.GetMostRecentPosts(_getLoggedInUser.LoggedInUser))
            {
                posts.Add(post);
            }
        }
    }
    
    public interface INewsFeedViewPosts
    {
        ObservableCollection<NewsFeedPost> Posts { get; }
    }

    public class NewsFeedViewModel : INewsFeedViewPosts
    {
        private readonly INewsFeedPostsUpdater _postsUpdater;

        [UsedImplicitly]
        public ICommand UpdatePostsCommand;

        [UsedImplicitly]
        public ObservableCollection<NewsFeedPost> Posts { get; } = new ObservableCollection<NewsFeedPost>();

        public NewsFeedViewModel([NotNull] INewsFeedPostsUpdater postsUpdater, [NotNull] IOnUserLoggedIn onUserLoggedIn)
        {
            _postsUpdater = postsUpdater ?? throw new ArgumentNullException(nameof(postsUpdater));
            if (onUserLoggedIn == null)
                throw new ArgumentNullException(nameof(onUserLoggedIn));
            UpdatePostsCommand = new Command(UpdatePosts);
            onUserLoggedIn.OnUserLoggedIn += UpdatePosts;
        }

        public void UpdatePosts()
        {
            _postsUpdater.UpdatePosts(Posts);
        }
    }
}