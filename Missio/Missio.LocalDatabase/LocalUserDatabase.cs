using System.Collections.Generic;
using Missio.LogIn;
using Missio.Registration;
using Missio.User;

namespace Missio.LocalDatabase
{
    /// <summary>
    /// A fake user and password validator that checks the given parameters against the hardcoded data
    /// </summary>
    public class LocalUserDatabase : IValidateUser, IDoesUserExist, IRegisterUser
    {
        /// <summary>
        /// A list of users that are guaranteed to exist, useful for testing purposes
        /// </summary>
        public static readonly List<User.User> ValidUsers =
            new List<User.User> {new User.User("Jorge Romero", "Yolo"), new User.User("Francisco Greco", "ElPass") };

        /// <summary>
        /// A list of users that are guaranteed to not exist, useful for testing purposes
        /// </summary>
        public static readonly List<User.User> InvalidUsers = new List<User.User>()
        {
            new User.User("Invalid user", ""),
            new User.User("Invalid user 2", "")
        };

        /// <summary>
        /// Cast a list of users to an array objects to use them as a NUnit test case source
        /// </summary>
        /// <param name="users">The list to cast</param>
        /// <returns> An array of objects with the users information </returns>
        public static object[] GetListOfUsersInTestForm(List<User.User> users)
        {
            var testData = new object[users.Count];
            for (var i = 0; i < users.Count; i++)
            {
                testData[i] = new object[] { users[i].UserName, users[i].Password };
            }
            return testData;
        }

        /// <inheritdoc />
        public void ValidateUser(User.User user)
        {
            if (!ValidUsers.Exists(x => x.UserName == user.UserName))
                throw new InvalidUserNameException();
            foreach (var validUser in ValidUsers)
            {
                if (validUser.UserName == user.UserName && validUser.Password != user.Password)
                    throw new InvalidPasswordException();
            }
        }

        /// <inheritdoc />
        public bool DoesUserExist(string userName)
        {
            return ValidUsers.Exists(x => x.UserName == userName);
        }

        /// <inheritdoc />
        public void RegisterUser(RegistrationInfo registrationInfo)
        {
            registrationInfo.ThrowExceptionIfStateIsInvalid();
            if(DoesUserExist(registrationInfo.UserName))
                throw new UserNameAlreadyInUseException();
            ValidUsers.Add(new User.User(registrationInfo.UserName, registrationInfo.Password));
        }
    }
}