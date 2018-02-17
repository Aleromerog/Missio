using System;
using System.Collections.ObjectModel;
using Mission.Model.LocalProviders;

namespace ViewModel
{
    public class NewsFeedViewModel
    {
        private readonly INewsFeedPositionProvider PostProvider;
        public ObservableCollection<string> NewsFeedPosts { get; }

        public NewsFeedViewModel(INewsFeedPositionProvider postProvider)
        {
            PostProvider = postProvider;
            NewsFeedPosts = PostProvider.GetMostRecentPosts();
        }
    }

    public class PostViewModel
    {
        
    }
}