namespace Missio.Users
{
    public interface ILoggedInUser
    {
        User LoggedInUser { get; set; }
    }
}