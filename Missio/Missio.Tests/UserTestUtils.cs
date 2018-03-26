using Mission.Model.LocalProviders;

namespace Missio.Tests
{
    public static class UserTestUtils
    {
        public static object[] GetInvalidUsers()
        {
            return LocalUserDatabase.GetListOfUsersInTestForm(LocalUserDatabase.InvalidUsers);
        }

        public static object[] GetValidUsers()
        {
            return LocalUserDatabase.GetListOfUsersInTestForm(LocalUserDatabase.ValidUsers);
        }

        public static object[] GetLogIncorrectPasswordTestsCases()
        {
            var testData = new object[LocalUserDatabase.ValidUsers.Count];
            for (int i = 0; i < LocalUserDatabase.ValidUsers.Count; i++)
            {
                var user = LocalUserDatabase.ValidUsers[i];
                testData[i] = new object[] { user.UserName, "" };
            }
            return testData;
        }
    }
}