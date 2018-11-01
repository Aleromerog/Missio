using System;
using JetBrains.Annotations;

namespace Missio.Posts
{
    public class CreatePostDTO
    {
        [UsedImplicitly]
        public string UserName { get; set; }

        [UsedImplicitly]
        public string Password { get; set; }

        [UsedImplicitly]
        public string Message { get; set; }

        [UsedImplicitly]
        public byte[] Picture { get; set; }

        [UsedImplicitly]
        public CreatePostDTO()
        {
        }

        public CreatePostDTO([NotNull] string userName, [NotNull] string password, [NotNull] string message, byte[] picture)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Picture = picture;
        }
    }
}