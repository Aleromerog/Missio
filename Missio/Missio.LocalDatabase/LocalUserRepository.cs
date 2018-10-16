using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Missio.LogIn;
using Missio.Navigation;
using Missio.Users;
using StringResources;

namespace Missio.LocalDatabase
{
    /// <summary>
    /// A fake user and password validator that checks the given parameters against the hardcoded data
    /// </summary>
    public class LocalUserRepository : IUserRepository
    {
        /// <summary>
        /// A list of users that are guaranteed to exist, useful for testing purposes
        /// </summary>
        private readonly List<User> _validUsers =
            new List<User> {new User("Jorge Romero", "Yolo", ""), new User("Francisco Greco", "ElPass", "") };

        /// <inheritdoc />
        public async Task ValidateUser(User user)
        {
            if (!_validUsers.Exists(x => x.UserName == user.UserName))
                throw new InvalidUserNameException();
            foreach (var validUser in _validUsers)
            {
                if (validUser.UserName == user.UserName && validUser.Password != user.Password)
                    throw new InvalidPasswordException();
            }
            await Task.CompletedTask; // Fix lack of awaits warning
        }

        private bool DoesUserExist(string userName)
        {
            return _validUsers.Exists(x => x.UserName == userName);
        }

        /// <inheritdoc />
        public Task<User> GetUserByName(string userName)
        {
            var user = _validUsers.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
                throw new ArgumentException("User with username " + nameof(userName) + " does not exist");
            return Task.FromResult(user);
        }

        /// <inheritdoc />
        public async Task AttemptToRegisterUser(User user)
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
            await Task.CompletedTask; // Fix lack of awaits warning
        }
    }
}