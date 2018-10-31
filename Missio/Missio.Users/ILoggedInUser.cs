namespace Missio.Users
{
    public interface ILoggedInUser
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}