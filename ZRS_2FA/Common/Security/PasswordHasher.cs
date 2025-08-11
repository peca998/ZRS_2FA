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

        public static (string Salt, string Hash) HashCode(string password, string? existingSalt = null)
        {
            byte[] saltBytes;

            if (string.IsNullOrEmpty(existingSalt))
            {
                // generate a random salt
                saltBytes = new byte[SaltSize];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(saltBytes);
            }
            else
            {
                // use provided salt
                saltBytes = Convert.FromBase64String(existingSalt);
            }

            // hash the password (or backup code) with the salt using PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // return salt and hash (salt will be the same as passed in if provided)
            return (Convert.ToBase64String(saltBytes), Convert.ToBase64String(hash));
        }


        // we call this when user logs in
        public static bool VerifyCode(string enteredCode, string storedSalt, string storedHash)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] hash = Convert.FromBase64String(storedHash);

            using var pbkdf2 = new Rfc2898DeriveBytes(enteredCode, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(HashSize);
            // compare the computed hash with the stored hash
            //return computedHash.SequenceEqual(hash); -- not secure enough, use a constant time comparison
            return CryptographicOperations.FixedTimeEquals(computedHash, hash);
        }
    }
}
