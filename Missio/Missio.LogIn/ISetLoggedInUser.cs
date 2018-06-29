using Missio.Users;

namespace Missio.LogIn
{
    public interface ISetLoggedInUser
    {
        User LoggedInUser { set; }
    }
}