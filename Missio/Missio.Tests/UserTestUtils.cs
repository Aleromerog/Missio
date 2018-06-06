using System.Collections.Generic;
using Mission.Model.Data;
using Mission.Model.LocalServices;

namespace Missio.Tests
{
    public static class UserTestUtils
    {
        public static object[] GetInvalidUsers()
        {
            var invalidUsers = new List<User> { LocalUserDatabase.InvalidUsers[0] };
            return LocalUserDatabase.GetListOfUsersInTestForm(invalidUsers);
        }

        public static object[] GetValidUsers()
        {
            var validUsers = new List<User> { LocalUserDatabase.ValidUsers[0] };
            return LocalUserDatabase.GetListOfUsersInTestForm(validUsers);
        }

        public static object[] GetLogIncorrectPasswordTestsCases()
        {
            return new object[] { new object[] { LocalUserDatabase.ValidUsers[0].UserName, "" } };
        }
    }
}