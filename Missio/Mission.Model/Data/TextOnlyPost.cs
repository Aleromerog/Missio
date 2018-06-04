using System;
using JetBrains.Annotations;

namespace Mission.Model.Data
{
    public class TextOnlyPost : NewsFeedPost, IAuthorName, IMessage
    {
        public string AuthorName { [UsedImplicitly] get; }
        public string Message { [UsedImplicitly] get; }

        public TextOnlyPost([NotNull] string author, [NotNull] string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            AuthorName = author ?? throw new ArgumentNullException(nameof(author));
        }
    }
}