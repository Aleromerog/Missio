using System;
using Mission.Model.Data;

namespace ViewModel
{
    /// <summary>
    /// Class for getting the user that is logged in
    /// </summary>
    public class GlobalUser : IGetLoggedInUser, ISetLoggedInUser, IOnUserLoggedIn
    {
        private User loggedInUser;

        public User LoggedInUser
        {
            get
            {
                if (loggedInUser == null)
                    throw new InvalidOperationException("No user is currently logged in");
                return loggedInUser;
            }
            set
            {
                loggedInUser = value;
                OnUserLoggedIn();
            }
        }

        public event Action OnUserLoggedIn = delegate { };
    }
}