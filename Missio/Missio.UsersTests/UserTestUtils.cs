using System.Collections.Generic;
using Missio.Users;

namespace Missio.UserTests
{
    public static class UserTestUtils
    {
        public static readonly User FranciscoUser = new User("Francisco Greco");
        public static readonly User JorgeUser = new User("Jorge Romero");
        public static readonly object[] NamesAlreadyInUse = {JorgeUser.UserName, FranciscoUser.UserName};

        public static List<User> GetValidUsers()
        {
            var validUsers = new List<User> {FranciscoUser, JorgeUser};
            return validUsers;
        }
    }
}