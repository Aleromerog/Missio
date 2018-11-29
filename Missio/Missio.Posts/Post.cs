using System;
using JetBrains.Annotations;
using Missio.Users;

namespace Missio.Posts
{
    public class Post : IPost
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public int LikeCount { get; private set; }
        
        [UsedImplicitly]
        public int CommentCount { get; private set; }

        [UsedImplicitly]
        public string Message { get; private set; }

        [UsedImplicitly]
        public byte[] Image { get; private set; }

        [UsedImplicitly]
        public User Author { get; private set; }

        [UsedImplicitly]
        public DateTime PublishedDate { get; private set; }

        private Post()
        {
        }

        public Post([NotNull] User author, [NotNull] string message, DateTime publishedDate, int likeCount, int commentCount, byte[] image = null)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            LikeCount = likeCount;
            CommentCount = commentCount;
            PublishedDate = publishedDate;
            Image = image;
        }
        
        /// <inheritdoc />
        public int GetPostPriority()
        {
            return 0;
        }
    }
}