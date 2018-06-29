using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Missio.Registration
{
    public class RegistrationInfo
    {
        private string _email;
        private string _password;
        private string _userName;

        public string Email
        {
            get => _email ?? "";
            set => _email = value;
        }

        public string Password
        {
            get => _password ?? "";
            set => _password = value;
        }

        public string UserName
        {
            get => _userName ?? "";
            set => _userName = value;
        }

        public RegistrationInfo([NotNull] string userName, [NotNull] string password, [NotNull] string email)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public List<RegistrationException> GetOfflineErrorExceptions()
        {
            var errorMessages = new List<RegistrationException>();
            if (IsUserNameTooShort())
                errorMessages.Add(new UserNameTooShortException());
            if (IsPasswordTooShort())
                errorMessages.Add(new PasswordTooShortException());
            return errorMessages;
        }

        public void ThrowExceptionIfStateIsInvalid()
        {
            var errorMessages = GetOfflineErrorExceptions();
            foreach (var errorMessage in errorMessages)
                throw errorMessage;
        }

        private bool IsPasswordTooShort()
        {
            return Password.Length < 5;
        }

        private bool IsUserNameTooShort()
        {
            return UserName.Length < 3;
        }

        public bool DoesUserNameHaveErrors()
        {
            return IsUserNameTooShort();
        }

        public bool DoesPasswordHaveErrors()
        {
            return IsPasswordTooShort();
        }
    }
}