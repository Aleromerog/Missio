using Missio.Navigation;
using StringResources;

namespace Missio.Registration
{
    public class UserNameAlreadyInUseException : RegistrationException
    {
        /// <inheritdoc />
        public UserNameAlreadyInUseException() : base(new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok))
        {
        }
    }
}