using System;
using JetBrains.Annotations;
using Missio.Users;

namespace Missio.Posts
{
    public class Post : IPost
    {
        [UsedImplicitly] public string Message { get; }
        [UsedImplicitly] public byte[] Image { get; }
        [UsedImplicitly] public User Author { get; }

        public Post()
        {
        }

        public Post([NotNull] User author, [NotNull] string message, byte[] image = null)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Image = image;
        }
        
        /// <inheritdoc />
        public int GetPostPriority()
        {
            return 0;
        }
    }
}