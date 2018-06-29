using Missio.Users;

namespace Missio.LogIn
{
    public interface IGetLoggedInUser
    {
        User LoggedInUser { get; }
    }
}