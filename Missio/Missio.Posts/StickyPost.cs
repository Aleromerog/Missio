using System;
using JetBrains.Annotations;

namespace Missio.Posts
{
    public class StickyPost : IPost
    {
        [UsedImplicitly]
        public int Id {  get; private set; }
        [UsedImplicitly] 
        public string Title { get; private set; }
        [UsedImplicitly]
        public string Message {  get; private set; }

        private StickyPost()
        {
        }

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