using JetBrains.Annotations;

namespace Domain
{
    public class Friendship
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public User User { get; private set; }

        [UsedImplicitly]
        public User Friend { get; private set; }

        private Friendship()
        {
        }

        public Friendship(User user, User friend)
        {
            User = user;
            Friend = friend;
        }
    }
}