using JetBrains.Annotations;

namespace Missio.Users
{
    public class UserCredentials
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public User User { get; private set; }

        [UsedImplicitly]
        public string HashedPassword { get; private set; }

        [UsedImplicitly]
        public UserCredentials()
        {
        }

        public UserCredentials(User user, string hashedPassword)
        {
            User = user;
            HashedPassword = hashedPassword;
        }
    }
}