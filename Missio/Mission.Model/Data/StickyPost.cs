using System;
using JetBrains.Annotations;

namespace Mission.Model.Data
{
    public class StickyPost : NewsFeedPost, ITitle, IMessage
    {
        public string Title { [UsedImplicitly] get; }
        public string Message { [UsedImplicitly] get; }

        /// <inheritdoc />
        public override int PostPriority { get; } = 10;

        public StickyPost([NotNull] string title, [NotNull] string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
    }
}