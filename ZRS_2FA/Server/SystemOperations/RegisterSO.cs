using Common.Domain;
using Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperations
{
    public class RegisterSO : SystemOperationBase
    {
        public Credentials Credentials { get; set; }
        public RegisterSO(Credentials c)
        {
            Credentials = c;
        }
        public override void Execute()
        {
            User? existingUser = _broker.GetUser(Credentials.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var hashes = PasswordHasher.HashPassword(Credentials.Password);
            User user = new User
            {
                Username = Credentials.Username,
                HashedPassword = hashes.Hash,
                Salt = hashes.Salt,
                TwoFactorSecret = string.Empty, // No 2FA for now
                TwoFactorEnabled = false
            };
            _broker.Insert(user);
        }
    }
}
