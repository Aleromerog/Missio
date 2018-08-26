using System;
using JetBrains.Annotations;

namespace Missio.Posts
{
    public class TextOnlyPost : IPost, IAuthorName, IMessage, IImage
    {
        public string AuthorName { [UsedImplicitly] get; }
        public string Message { [UsedImplicitly] get; }
        public string Image { [UsedImplicitly] get; }


        public TextOnlyPost([NotNull] string author, [NotNull] string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            AuthorName = author ?? throw new ArgumentNullException(nameof(author));
        }

        
        
        /// <inheritdoc />
        public int GetPostPriority()
        {
            return 0;
        }

    }
}