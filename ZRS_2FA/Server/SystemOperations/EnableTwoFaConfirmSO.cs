using Common.Domain;
using Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperations
{
    internal class EnableTwoFaConfirmSO : SystemOperationBase
    {
        public long UserId { get; set; }
        public string EnteredCode { get; set; }
        public List<string> Result { get; set; }

        public EnableTwoFaConfirmSO(long userId, string enteredCode)
        {
            UserId = userId;
            EnteredCode = enteredCode;
            Result = [];
        }
        public override void Execute()
        {
            string? secretKey = 
                _broker.GetTwoFaSecret(UserId) ?? 
                throw new InvalidOperationException("Two-factor authentication is not initialized for this user.");
            bool isCodeValid = TwoFaHelper.ValidateCode(secretKey, EnteredCode);
            if (!isCodeValid)
            {
                throw new InvalidOperationException("Invalid two-factor authentication code.");
            }
            _broker.SetTwoFaEnabled(UserId, true);
            // Generate backup codes
            User u = _broker.GetUserById(UserId) 
                ?? throw new InvalidOperationException("User does not exist.");
            List<string> plainBackupCodes = [];
            List<string> hashedBackupCodes = [];
            (plainBackupCodes, hashedBackupCodes) = TwoFaHelper.GenerateRecoveryCodes(u.Salt);
            foreach (string code in hashedBackupCodes)
            {
                BackupCode b = new()
                {
                    UserId = u.Id,
                    Code = code,
                    IsUsed = false
                };
                _broker.Insert(b);
            }
            Result = plainBackupCodes;
        }
    }
}
