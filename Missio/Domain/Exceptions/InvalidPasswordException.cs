using Missio.ApplicationResources;

namespace Domain.Exceptions
{
    public class InvalidPasswordException : LogInException
    {
        /// <inheritdoc />
        public InvalidPasswordException() : base(Strings.InvalidPassword)
        {
        }
    }
}