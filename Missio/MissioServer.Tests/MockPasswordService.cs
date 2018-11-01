using Microsoft.AspNetCore.Identity;
using Missio.Users;

namespace MissioServer.Tests
{
    public class MockPasswordService : IPasswordHasher<User>
    {
        /// <inheritdoc />
        public string HashPassword(User user, string password)
        {
            return "Hashed" + password;
        }

        /// <inheritdoc />
        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == "HashedElPass" && providedPassword == "ElPass")
                return PasswordVerificationResult.Success;
            if (hashedPassword == "HashedYolo" && providedPassword == "Yolo")
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;
        }
    }
}