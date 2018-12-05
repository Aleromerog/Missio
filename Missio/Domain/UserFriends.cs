using System.Collections.Generic;
using JetBrains.Annotations;

namespace Domain
{
    //TODO: Change this to a friendship class
    public class UserFriends
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public User User { get; private set; }

        [UsedImplicitly]
        public ICollection<User> Friends { get; private set; } = new List<User>();

        private UserFriends()
        {
        }

        public UserFriends(User user, ICollection<User> friends)
        {
            User = user;
            if(friends == null)
                friends = new List<User>();
            Friends = friends;
        }
    }
}