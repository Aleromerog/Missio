using Missio.Navigation;
using StringResources;

namespace Missio.Registration
{
    public class UserNameTooShortException : RegistrationException
    {
        /// <inheritdoc />
        public UserNameTooShortException() : base(new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok))
        {
        }
    }
}