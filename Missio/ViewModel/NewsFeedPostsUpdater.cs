using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.Services;

namespace ViewModel
{
    public class NewsFeedPostsUpdater : INewsFeedPostsUpdater
    {
        private readonly IGetMostRecentPosts _postsProvider;

        public NewsFeedPostsUpdater(IGetMostRecentPosts postsProvider)
        {
            _postsProvider = postsProvider;
        }
        
        /// <inheritdoc />
        public void UpdatePosts(ObservableCollection<NewsFeedPost> posts)
        {
            posts.Clear();
            foreach (var post in _postsProvider.GetMostRecentPostsInOrder())
            {
                posts.Add(post);
            }
        }
    }
}