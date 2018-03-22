using System;

namespace ViewModel
{
    public interface IOnUserLoggedIn
    {
        event Action OnUserLoggedIn;
    }
}