using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.LocalProviders;

namespace ViewModel
{
    public class NewsFeedViewModel
    {
        private readonly INewsFeedPositionProvider PostProvider;
        public ObservableCollection<NewsFeedPost> NewsFeedPosts { get; }

        public NewsFeedViewModel(INewsFeedPositionProvider postProvider)
        {
            PostProvider = postProvider;
            NewsFeedPosts = PostProvider.GetMostRecentPosts();
        }
    }
}