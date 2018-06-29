using Missio.Navigation;
using StringResources;

namespace Missio.Registration
{
    public class PasswordTooShortException : RegistrationException
    {
        /// <inheritdoc />
        public PasswordTooShortException() : base(new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok))
        {
        }
    }
}