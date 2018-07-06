using System;
using JetBrains.Annotations;

namespace Missio.NewsFeed
{
    public class StickyPost : IPost, ITitle, IMessage
    {
        public string Title { [UsedImplicitly] get; }
        public string Message { [UsedImplicitly] get; }

        public StickyPost([NotNull] string title, [NotNull] string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        /// <inheritdoc />
        public int GetPostPriority()
        {
            return 10;
        }
    }
}