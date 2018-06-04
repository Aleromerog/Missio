namespace Mission.Model.Data
{
    public class NewsFeedPost : IPostPriority
    {
        /// <inheritdoc />
        public virtual int PostPriority { get; } = 0;
    }
}