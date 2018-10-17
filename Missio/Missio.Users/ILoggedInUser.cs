using Missio.Users;

namespace Missio.LogIn
{
    public interface ILoggedInUser
    {
        User LoggedInUser { get; set; }
    }
}