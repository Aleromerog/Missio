using Missio.LocalDatabase;

namespace Missio.LocalDatabaseTests
{
    public class UserNamesAlreadyInUse
    {
        public static readonly object[] NamesAlreadyInUse = {
            LocalUserDatabase.ValidUsers[0].UserName, LocalUserDatabase.ValidUsers[1].UserName
        };
    }
}