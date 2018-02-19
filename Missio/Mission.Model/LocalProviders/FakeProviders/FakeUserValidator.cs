using System;
using System.Collections.Generic;
using Mission.Model.Data;

namespace Mission.Model.LocalProviders
{
    /// <summary>
    /// A fake user and password validator that checks the given parameters against the hardcoded data
    /// </summary>
    public class FakeUserValidator : IUserValidator
    {
        private List<User> ValidUsers =
            new List<User> {new User("Jorge Romero", "Yolo"), new User("Francisco Greco", "ElPass"),};

        /// <inheritdoc />
        public LogInAttemptResult IsDataCorrect(User user)
        {
            if (!ValidUsers.Exists(x => x.UserName == user.UserName))
                return LogInAttemptResult.IncorrectUsername;
            foreach (var validUser in ValidUsers)
            {
                if (validUser.UserName == user.UserName && validUser.Password != user.Password)
                    return LogInAttemptResult.IncorrectPassword;
                if (validUser.UserName == user.UserName && validUser.Password == user.Password)
                    return LogInAttemptResult.Succeeded;
            }
            throw new ArgumentException(nameof(user));
        }
    }
}