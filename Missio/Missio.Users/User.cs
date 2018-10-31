using System;
using JetBrains.Annotations;

namespace Missio.Users
{
    public class User : IEquatable<User>
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public string UserName { get; private set; }

        [UsedImplicitly]
        public string Email { get; private set; }

        // TODO: Move this to a separate class
        [UsedImplicitly]
        public string HashedPassword { get; private set; }

        [UsedImplicitly]
        public byte[] Picture { get; private set; }

        //TODO: Make picture mandatory (We can have a default picture)
        public User([NotNull] string userName, [NotNull] string hashedPassword, byte[] picture, [NotNull] string email)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            HashedPassword = hashedPassword ?? throw new ArgumentNullException(nameof(hashedPassword));
            Picture = picture;
        }

        /// <inheritdoc />
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((User) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}