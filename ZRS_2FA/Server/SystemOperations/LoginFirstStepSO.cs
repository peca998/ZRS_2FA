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
    public class LoginFirstStepSO : SystemOperationBase
    {
        public Credentials Credentials { get; set; }
        public LoginResult Result { get; set; } = LoginResult.WrongPassword;
        private const int TimeoutSeconds = 30;
        private const int AllowedAttempts = 3;
        private readonly TimeSpan LookbackPeriod = TimeSpan.FromMinutes(5);

        public LoginFirstStepSO(Credentials c)
        {
            Credentials = c;
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

            if (!IsLoginAllowed(existingUser.Id))
            {
                Result = LoginResult.InTimeout;
                _broker.Insert(attempt);
                return;
            }

            if (!PasswordHasher.VerifyPassword(Credentials.Password, existingUser.Salt, existingUser.HashedPassword))
            {
                Result = LoginResult.WrongPassword;
                _broker.Insert(attempt);
                return;
            }

            attempt.Success = true;
            if (!existingUser.TwoFactorEnabled)
            {
                Result = LoginResult.Success;
                _broker.Insert(attempt);
                return;
            }
            else
            {
                Result = LoginResult.TwoFactorRequired;
                attempt.AttemptType = LoginAttemptType.TwoFactor;
                _broker.Insert(attempt);
                return;
            }
        }

        public bool IsLoginAllowed(long userId)
        {
            DateTime lookbackStart = DateTime.UtcNow - LookbackPeriod;
            (int attempts, DateTime? lastAttempt) = _broker.GetLoginAttempts(userId, lookbackStart);

            if (attempts >= AllowedAttempts)
            {
                if (lastAttempt.HasValue)
                {
                    TimeSpan timeSinceLastAttempt = DateTime.UtcNow - lastAttempt.Value;
                    if (timeSinceLastAttempt.TotalSeconds < TimeoutSeconds)
                    {
                        // User is still in timeout lockout period
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
