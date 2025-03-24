using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Convert to a hex string
            }
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            string hashedInputPassword = HashPassword(password);
            return hashedPassword.Equals(hashedInputPassword);
        }
    }
}
