﻿using System;
using Missio.Users;

namespace Missio.LogIn
{
    /// <summary>
    /// Class for getting the user that is logged in
    /// </summary>
    public class GlobalUser : IGetLoggedInUser, ISetLoggedInUser
    {
        private User _loggedInUser;

        public User LoggedInUser
        {
            get
            {
                if (_loggedInUser == null)
                    throw new InvalidOperationException("No user is currently logged in");
                return _loggedInUser;
            }
            set => _loggedInUser = value;
        }
    }
}