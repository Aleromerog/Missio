using System.Collections.Generic;
using System.Linq;
using Missio.Users;

namespace Missio.UserTests
{
    public static class UserTestUtils
    {
        public static readonly User FranciscoUser = new User("Francisco Greco", "ElPass");
        public static readonly User JorgeUser = new User("Jorge Romero", "Yolo");
        public static readonly object[] NamesAlreadyInUse = {JorgeUser.UserName, FranciscoUser.UserName};

        public static object[] GetInvalidUsers()
        {
            return new object[] { new User("Non existing user", ""), new User("Non existing user2", "") };
        }

        public static List<User> GetValidUsers()
        {
            var validUsers = new List<User> {FranciscoUser, JorgeUser};
            return validUsers;
        }

        public static object[] GetValidUsersAsObjects()
        {
            return GetValidUsers().Cast<object>().ToArray();
        }

        public static object[] GetUsersWithIncorrectPasswords()
        {
            return new object[] { new User(FranciscoUser.UserName, "Incorrect")};
        }

        public static List<string> GetUserPostsContents(User user)
        {
            if (user == JorgeUser)
                return new List<string> {"A sticky message for user zero", "Hello Jorge Romero", "Hello me"};
            if(user == FranciscoUser)
                return new List<string> { "A sticky message for user one", "Hello Greco", "Hello me" };
            return new List<string>();
        }
    }
}