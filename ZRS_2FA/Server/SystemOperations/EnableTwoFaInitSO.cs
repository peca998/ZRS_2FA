using Common.Domain;
using Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperations
{
    public class EnableTwoFaInitSO : SystemOperationBase
    {
        public long UserId { get; set; }
        public string Result { get; set; }
        public EnableTwoFaInitSO(long userId)
        {
            UserId = userId;
        }
        public override void Execute()
        {
            User? u = _broker.GetUserById(UserId) ?? throw new InvalidOperationException("User does not exist!");
            string secretKey = TwoFaHelper.GenerateSecret();
            _broker.SetTwoFaSecret(UserId, secretKey);
            string url = TwoFaHelper.GenerateQrCodeUrl(secretKey, u.Username);
            Result = TwoFaHelper.GenerateQrCodeImage(url);
        }
    }
}
