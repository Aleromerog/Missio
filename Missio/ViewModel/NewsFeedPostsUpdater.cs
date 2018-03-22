using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.LocalProviders;

namespace ViewModel
{
    public class NewsFeedPostsUpdater : INewsFeedPostsUpdater
    {
        private readonly IGetLoggedInUser _getLoggedInUser;
        private readonly INewsFeedPostsProvider _postsProvider;

        public NewsFeedPostsUpdater(IGetLoggedInUser getLoggedInUser, INewsFeedPostsProvider postsProvider)
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
}