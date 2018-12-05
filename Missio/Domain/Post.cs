using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Domain
{
    public class Post : IPost
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public ICollection<Like> Likes { get; private set; } = new List<Like>();

        [UsedImplicitly]
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

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

        public Post([NotNull] User author, [NotNull] string message, DateTime publishedDate, byte[] image = null)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Message = message ?? throw new ArgumentNullException(nameof(message));
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