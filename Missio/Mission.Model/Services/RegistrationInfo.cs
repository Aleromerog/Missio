using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using StringResources;

namespace Mission.Model.Services
{
    public class RegistrationInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string ConfirmPassword { get; set; }

        public RegistrationInfo([NotNull] string userName, [NotNull] string password, [NotNull] string confirmPassword,
            [NotNull] string email)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            ConfirmPassword = confirmPassword ?? throw new ArgumentNullException(nameof(password));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public List<AlertTextMessage> GetOfflineErrorMessages()
        {
            var errorMessages = new List<AlertTextMessage>();
            if (UserName.Length <= 3)
                errorMessages.Add(new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok));
            if (Password != ConfirmPassword)
                errorMessages.Add(new AlertTextMessage(AppResources.PasswordsDontMatchTitle, AppResources.PasswordsDontMatchMessage, AppResources.Ok));
            if (Password.Length < 5)
                errorMessages.Add(new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok));
            return errorMessages;
        }
    }
}