namespace Missio.NewsFeed
{
    public class NewsFeedPost : IPostPriority
    {
        /// <inheritdoc />
        public virtual int PostPriority { get; } = 0;
    }
}