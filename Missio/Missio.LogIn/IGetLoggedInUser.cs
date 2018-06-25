namespace Missio.LogIn
{
    public interface IGetLoggedInUser
    {
        User.User LoggedInUser { get; }
    }
}