using Mission.Model.LocalProviders;

namespace ViewModel
{
    public class NewsFeedViewModel
    {
        private readonly INewsFeedPositionProvider PostProvider;

        public NewsFeedViewModel(INewsFeedPositionProvider postProvider)
        {
            PostProvider = postProvider;
        }
    }

    public class PostViewModel
    {

    }
}