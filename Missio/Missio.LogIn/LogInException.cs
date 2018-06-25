using System;
using Missio.Navigation;

namespace Missio.LogIn
{
    public class LogInException : Exception
    {
        public readonly AlertTextMessage AlertTextMessage;

        protected LogInException(AlertTextMessage alertTextMessage)
        {
            AlertTextMessage = alertTextMessage;
        }
    }
}