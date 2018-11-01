namespace Missio.Users
{
    public class LoggedInUser : ILoggedInUser
    {
        /// <inheritdoc />
        public string UserName { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }
    }
}