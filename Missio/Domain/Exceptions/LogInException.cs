using System;

namespace Domain.Exceptions
{
    public class LogInException : Exception
    {
        public string ErrorMessage { get; }

        public LogInException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}