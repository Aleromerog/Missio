using JetBrains.Annotations;

namespace Domain
{
    public class UserCredentials
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public User User { get; private set; }

        [UsedImplicitly]
        public string HashedPassword { get; private set; }

        private UserCredentials()
        {
        }

        public UserCredentials(User user, string hashedPassword)
        {
            User = user;
            HashedPassword = hashedPassword;
        }
    }
}