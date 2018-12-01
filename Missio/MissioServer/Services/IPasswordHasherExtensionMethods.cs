using Microsoft.AspNetCore.Identity;

namespace MissioServer.Services
{
    public static class PasswordHasherExtensionMethods
    {
        public static string HashPassword<T>(this IPasswordHasher<T> passwordHasher, string password) where T : class 
        {
            return passwordHasher.HashPassword(null, password);
        }

        public static PasswordVerificationResult VerifyHashedPassword<T>(this IPasswordHasher<T> passwordHasher, string hashedPassword, string providedPassword) where T : class
        {
            return passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}