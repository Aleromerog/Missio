using System;

namespace Mission.Model.Services
{
    public class AlertTextMessage : IEquatable<AlertTextMessage>
    {
        public readonly string Title;
        public readonly string Message;
        public readonly string AcceptMessage;

        public AlertTextMessage(string title, string message, string acceptMessage)
        {
            Title = title;
            Message = message;
            AcceptMessage = acceptMessage;
        }

        /// <inheritdoc />
        public bool Equals(AlertTextMessage other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Title, other.Title) && string.Equals(Message, other.Message) && string.Equals(AcceptMessage, other.AcceptMessage);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((AlertTextMessage) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Title.GetHashCode();
                hashCode = (hashCode * 397) ^ Message.GetHashCode();
                hashCode = (hashCode * 397) ^ AcceptMessage.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(AlertTextMessage left, AlertTextMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AlertTextMessage left, AlertTextMessage right)
        {
            return !Equals(left, right);
        }
    }
}