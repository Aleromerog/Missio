using System;
using Missio.Navigation;
using StringResources;

namespace Missio.LogIn
{
    public class LogInException : Exception
    {
        public readonly AlertTextMessage AlertTextMessage;

        public LogInException(string message)
        {
            AlertTextMessage = new AlertTextMessage(AppResources.TheLogInWasUnsuccessful, message, AppResources.Ok);
        }
    }
}