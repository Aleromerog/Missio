using Missio.NewsFeed;

namespace PostPublication
{
    public interface IPublishPost
    {
        void PublishPost(NewsFeedPost post);
    }
}