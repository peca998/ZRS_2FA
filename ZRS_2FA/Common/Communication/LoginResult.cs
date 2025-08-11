using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Communication
{
    public enum LoginResult
    {
        SuccessOneStep,
        SuccessTwoFa,
        SuccessBackup,
        TwoFactorRequired,
        UserNotFound,
        WrongPassword,
        WrongTwoFactorCode,
        InTimeout
    }
}
