using Common.Communication;
using Common.Domain;
using Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.SystemOperations.Utils;

namespace Server.SystemOperations
{
    public class LoginSecondStep : SystemOperationBase
    {
        public LoginResult Result { get; set; }
        public Credentials Credentials { get; set; }
        public LoginSecondStep(Credentials c)
        {
            Credentials = c;
        }
        public override void Execute()
        {
            User? existingUser = _broker.GetUser(Credentials.Username) ??
                throw new InvalidOperationException("User does not exist.");

            string secret = _broker.GetTwoFaSecret(existingUser.Id) ??
                throw new InvalidOperationException("Two-factor authentication is not enabled for this user.");
            LoginAttempt attempt = new()
            {
                UserId = existingUser.Id,
                AttemptTime = DateTime.UtcNow,
                Success = false,
                AttemptType = LoginAttemptType.TwoFactor
            };
            if(!Utils.Utils.IsLoginAllowed(existingUser.Id, _broker))
            {
                Result = LoginResult.InTimeout;
                _broker.Insert(attempt);
                return;
            }

            bool isCodeValid = TwoFaHelper.ValidateCode(secret, Credentials.Password);
            if (!isCodeValid)
            {
                Result = LoginResult.WrongTwoFactorCode;
                _broker.Insert(attempt);
                return;
            }
            attempt.Success = true;
            Result = LoginResult.SuccessTwoFa;
            _broker.Insert(attempt);
        }
    }
}
