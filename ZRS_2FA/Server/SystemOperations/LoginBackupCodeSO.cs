using Common.Communication;
using Common.Domain;
using Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperations
{
    public class LoginBackupCodeSO : SystemOperationBase
    {
        public LoginResult Result { get; set; }
        public Credentials Credentials { get; set; }

        public LoginBackupCodeSO(Credentials c)
        {
            Credentials = c;
        }
        public override void Execute()
        {
            User? existingUser = _broker.GetUser(Credentials.Username) ??
                throw new InvalidOperationException("User does not exist.");
            List<string> backupCodes = _broker.GetBackupCodes(existingUser.Id);
            LoginAttempt attempt = new()
            {
                UserId = existingUser.Id,
                AttemptTime = DateTime.UtcNow,
                Success = false,
                AttemptType = LoginAttemptType.TwoFactor
            };
            if (!Utils.Utils.IsLoginAllowed(existingUser.Id, _broker))
            {
                Result = LoginResult.InTimeout;
                _broker.Insert(attempt);
                return;
            }
            bool matched = false;
            foreach (string code in backupCodes)
            {
                if (PasswordHasher.VerifyCode(Credentials.Password, existingUser.Salt, code))
                {
                    matched = true;
                    _broker.SetBackupCodeUsed(existingUser.Id, code);
                    break;
                }
            }
            if(!matched)
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
