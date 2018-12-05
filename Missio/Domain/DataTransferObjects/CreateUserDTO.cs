using System;
using JetBrains.Annotations;

namespace Domain.DataTransferObjects
{
    public class CreateUserDTO
    {
        [UsedImplicitly]
        public string UserName { get; set; }

        [UsedImplicitly]
        public string Password { get; set; }

        [UsedImplicitly]
        public string Email { get; set; }

        [UsedImplicitly]
        public byte[] Picture { get; set; }

        private CreateUserDTO()
        {
        }

        public CreateUserDTO([NotNull] string userName, [NotNull] string password, [NotNull] string email, byte[] picture = null)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Picture = picture;
        }
    }
}