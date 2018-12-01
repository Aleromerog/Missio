using System;
using JetBrains.Annotations;

namespace Domain
{
    public class User 
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public string UserName { get; private set; }

        [UsedImplicitly]
        public string Email { get; private set; }

        [UsedImplicitly]
        public byte[] Picture { get; private set; }

        public User([NotNull] string userName, [NotNull] byte[] picture, [NotNull] string email)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Picture = picture ?? throw new ArgumentNullException(nameof(picture));
        }
    }
}