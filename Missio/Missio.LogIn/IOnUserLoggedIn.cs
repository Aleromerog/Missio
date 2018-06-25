using System;

namespace Missio.LogIn
{
    public interface IOnUserLoggedIn
    {
        event Action OnUserLoggedIn;
    }
}