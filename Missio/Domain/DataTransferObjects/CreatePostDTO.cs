using System;
using JetBrains.Annotations;

namespace Domain.DataTransferObjects
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
            NameAndPassword = nameAndPassword ?? throw new ArgumentNullException(nameof(nameAndPassword));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Picture = picture;
        }
    }
}