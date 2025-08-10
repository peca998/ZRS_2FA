using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // Size of the salt in bytes
        private const int HashSize = 32; // Size of the hash in bytes
        private const int Iterations = 10000; // Number of iterations for PBKDF2

        // we call this when user registers
        public static (string Salt, string Hash) HashPassword(string password)
        {
            // generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // hash the password with the salt using PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);
            // convert the salt and hash to base64 strings for storage
            return (Convert.ToBase64String(salt), Convert.ToBase64String(hash));

        }

        // we call this when user logs in
        public static bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] hash = Convert.FromBase64String(storedHash);

            using var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(HashSize);
            // compare the computed hash with the stored hash
            //return computedHash.SequenceEqual(hash); -- not secure enough, use a constant time comparison
            return CryptographicOperations.FixedTimeEquals(computedHash, hash);
        }
    }
}
