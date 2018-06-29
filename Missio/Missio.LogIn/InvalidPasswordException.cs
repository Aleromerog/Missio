using Missio.Navigation;
using StringResources;

namespace Missio.LogIn
{
    public class InvalidPasswordException : LogInException
    {
        /// <inheritdoc />
        public InvalidPasswordException() : base(new AlertTextMessage(AppResources.IncorrectPasswordTitle, AppResources.IncorrectPasswordMessage, AppResources.Ok))
        {
        }
    }
}