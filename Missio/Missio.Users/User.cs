using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Missio.Navigation;
using StringResources;

namespace Missio.Users
{
    public class User : IEquatable<User>
    {
        [UsedImplicitly]
        public int Id { get; set; }
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }
        public byte[] Picture { get; }
        public ICollection<User> Friends { get; }

        [UsedImplicitly]
        public User()
        {
        }

        public User([NotNull] string userName, [NotNull] string password = "",  byte[] picture = null, [NotNull] string email = "")
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Picture = picture;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public List<AlertTextMessage> GetOfflineErrors()
        {
            var errorMessages = new List<AlertTextMessage>();
            if (IsUserNameTooShort())
                errorMessages.Add(new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok));
            if (IsPasswordTooShort())
                errorMessages.Add(new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok));
            return errorMessages;
        }

        private bool IsPasswordTooShort()
        {
            return Password.Length < 5;
        }

        private bool IsUserNameTooShort()
        {
            return UserName.Length < 3;
        }

        /// <inheritdoc />
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(UserName, other.UserName) && string.Equals(Password, other.Password);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((User) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (UserName.GetHashCode() * 397) ^ Password.GetHashCode();
            }
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}