namespace Missio.Users
{
    public class NameAndPassword
    {
        public string UserName { get; }
        public string Password { get; }

        public NameAndPassword(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}