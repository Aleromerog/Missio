using System.Collections.ObjectModel;
using Mission.Model.Data;

namespace ViewModel
{
    public interface INewsFeedViewPosts
    {
        ObservableCollection<NewsFeedPost> Posts { get; }
    }
}