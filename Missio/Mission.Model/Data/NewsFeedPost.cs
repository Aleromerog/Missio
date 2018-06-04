namespace Mission.Model.Data
{
    public class NewsFeedPost : IPriority
    {
        /// <inheritdoc />
        public virtual int Priority { get; } = 0;
    }
}