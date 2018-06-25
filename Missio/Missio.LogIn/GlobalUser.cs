using System;

namespace Missio.LogIn
{
    /// <summary>
    /// Class for getting the user that is logged in
    /// </summary>
    public class GlobalUser : IGetLoggedInUser, ISetLoggedInUser, IOnUserLoggedIn
    {
        private User.User _loggedInUser;

        public User.User LoggedInUser
        {
            get
            {
                if (_loggedInUser == null)
                    throw new InvalidOperationException("No user is currently logged in");
                return _loggedInUser;
            }
            set
            {
                _loggedInUser = value;
                OnUserLoggedIn();
            }
        }

        public event Action OnUserLoggedIn = delegate { };
    }
}