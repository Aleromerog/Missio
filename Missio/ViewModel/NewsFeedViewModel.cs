using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Mission.Model.Data;
using Mission.Model.LocalProviders;

namespace ViewModel
{
    public class NewsFeedViewModel
    {
        private readonly INewsFeedPostsUpdater _postsUpdater;

        [UsedImplicitly]
        public ObservableCollection<NewsFeedPost> Posts { get; } = new ObservableCollection<NewsFeedPost>();

        public NewsFeedViewModel(INewsFeedPostsUpdater postsUpdater)
        {
            _postsUpdater = postsUpdater;
        }

        public void UpdatePosts()
        {
            _postsUpdater.UpdatePosts(Posts);
        }
    }
}