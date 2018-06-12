using Mission.Model.Services;
using StringResources;

namespace Mission.Model.Exceptions
{
    public class PasswordTooShortException : RegistrationException
    {
        /// <inheritdoc />
        public PasswordTooShortException() : base(new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok))
        {
        }
    }
}