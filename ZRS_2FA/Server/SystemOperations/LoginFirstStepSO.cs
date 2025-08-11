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
    public class LoginFirstStepSO : SystemOperationBase
    {
        public Credentials Credentials { get; set; }
        public LoginResultData Result { get; set; }

        public LoginFirstStepSO(Credentials c)
        {
            Credentials = c;
            Result = new LoginResultData
            {
                LoginResult = LoginResult.UserNotFound,
                UserId = -1
            };
        }
        public override void Execute()
        {
            User? existingUser = _broker.GetUser(Credentials.Username) ??
                throw new InvalidOperationException("User does not exist.");

            LoginAttempt attempt = new LoginAttempt
            {
                UserId = existingUser.Id,
                AttemptTime = DateTime.UtcNow,
                Success = false,
                AttemptType = LoginAttemptType.Password
            };

            if (!Utils.Utils.IsLoginAllowed(existingUser.Id, _broker))
            {
                Result.LoginResult = LoginResult.InTimeout;
                _broker.Insert(attempt);
                return;
            }

            if (!PasswordHasher.VerifyPassword(Credentials.Password, existingUser.Salt, existingUser.HashedPassword))
            {
                Result.LoginResult = LoginResult.WrongPassword;
                _broker.Insert(attempt);
                return;
            }

            attempt.Success = true;
            Result.UserId = existingUser.Id;
            Result.LoginResult = LoginResult.SuccessOneStep;
            _broker.Insert(attempt);
            if (!existingUser.TwoFactorEnabled)
            {
                return;
            }
            else
            {
                Result.LoginResult = LoginResult.TwoFactorRequired;
                return;
            }
        }

    }
}
