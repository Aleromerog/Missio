using Missio.NewsFeed;

namespace Missio.PostPublication
{
    public interface IPublishPost
    {
        void PublishPost(NewsFeedPost post);
    }
}