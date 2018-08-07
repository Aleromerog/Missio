using System;
using System.Collections.Generic;
using System.Linq;
using Missio.LogIn;
using Missio.Navigation;
using Missio.Users;
using StringResources;

namespace Missio.LocalDatabase
{
    /// <summary>
    /// A fake user and password validator that checks the given parameters against the hardcoded data
    /// </summary>
    public class LocalUserDatabase : IUserRepository
    {
        /// <summary>
        /// A list of users that are guaranteed to exist, useful for testing purposes
        /// </summary>
        private readonly List<User> _validUsers =
            new List<User> {new User("Jorge Romero", "Yolo"), new User("Francisco Greco", "ElPass") };

        /// <inheritdoc />
        public void ValidateUser(User user)
        {
            if (!_validUsers.Exists(x => x.UserName == user.UserName))
                throw new InvalidUserNameException();
            foreach (var validUser in _validUsers)
            {
                if (validUser.UserName == user.UserName && validUser.Password != user.Password)
                    throw new InvalidPasswordException();
            }
        }

        /// <inheritdoc />
        public bool DoesUserExist(string userName)
        {
            return _validUsers.Exists(x => x.UserName == userName);
        }

        /// <inheritdoc />
        public User GetUserByName(string userName)
        {
            var user = _validUsers.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
                throw new ArgumentException("User with username " + nameof(userName) + " does not exist");
            return user;
        }

        /// <inheritdoc />
        public void AttemptToRegisterUser(User user)
        {
            var errors = user.GetOfflineErrors();
            if (errors.Count > 0)
                throw new UserRegistrationException(errors);
            if(DoesUserExist(user.UserName))
                throw new UserRegistrationException(new List<AlertTextMessage>
                {
                    new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok)
                });
            _validUsers.Add(user);
        }
    }
}