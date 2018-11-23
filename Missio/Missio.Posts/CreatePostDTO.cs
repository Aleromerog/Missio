using System;
using JetBrains.Annotations;
using Missio.Users;

namespace Missio.Posts
{
    public class CreatePostDTO
    {
        [UsedImplicitly]
        public NameAndPassword NameAndPassword { get; set; }

        [UsedImplicitly]
        public string Message { get; set; }

        [UsedImplicitly]
        public byte[] Picture { get; set; }

        [UsedImplicitly]
        private CreatePostDTO()
        {
        }

        public CreatePostDTO([NotNull] NameAndPassword nameAndPassword, [NotNull] string message, byte[] picture)
        {
            NameAndPassword = nameAndPassword;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Picture = picture;
        }
    }
}