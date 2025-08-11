using Common.Communication;
using Common.Domain;
using DBBroker;
using Server.SystemOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Controller
    {
        private Broker _broker;
        private static Controller? _instance;
        public static Controller Instance => _instance ??= new Controller();
        private Controller()
        {
            _broker = new Broker();
        }

        public void Register(Credentials c)
        {
            RegisterSO so = new(c);
            so.ExecuteTemplate();
        }

        public LoginResultData LoginFirstStep(Credentials c)
        {
            LoginFirstStepSO so = new(c);
            so.ExecuteTemplate();
            switch (so.Result.LoginResult)
            {
                case LoginResult.SuccessOneStep:
                    break;
                case LoginResult.TwoFactorRequired:
                    break;
                case LoginResult.UserNotFound:
                    throw new InvalidOperationException("User does not exist!");
                case LoginResult.WrongPassword:
                    throw new InvalidOperationException("Wrong password!");
                case LoginResult.InTimeout:
                    throw new InvalidOperationException("Too many failed attempts, try again later.");
                default:
                    break;
            }
            return so.Result;
        }

        public string EnableTwoFaInit(long userId, bool regenerate)
        {
            EnableTwoFaInitSO so = new(userId, regenerate);
            so.ExecuteTemplate();
            return so.Result;
        }

        public List<string> EnableTwoFaConfirm(long userId, string code)
        {
            EnableTwoFaConfirmSO so = new(userId, code);
            so.ExecuteTemplate();
            return so.Result;
        }

        public LoginResult LoginSecondStep(Credentials c)
        {
            LoginSecondStepSO so = new(c);
            so.ExecuteTemplate();
            switch (so.Result)
            {
                case LoginResult.SuccessTwoFa:
                    break;
                case LoginResult.WrongTwoFactorCode:
                    throw new InvalidOperationException("Wrong two-factor code!");
                case LoginResult.InTimeout:
                    throw new InvalidOperationException("Too many failed attempts, try again later.");
                default:
                    break;
            }
            return so.Result;
        }

        public LoginResult LoginBackupCode(Credentials c)
        {
            LoginBackupCodeSO so = new(c);
            so.ExecuteTemplate();
            switch (so.Result)
            {
                case LoginResult.SuccessTwoFa:
                    break;
                case LoginResult.WrongTwoFactorCode:
                    throw new InvalidOperationException("Wrong backup code!");
                case LoginResult.InTimeout:
                    throw new InvalidOperationException("Too many failed attempts, try again later.");
                default:
                    break;
            }
            return so.Result;
        }
    }
}
