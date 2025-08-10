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

        public LoginResult LoginFirstStep(Credentials c)
        {
            LoginFirstStepSO so = new(c);
            so.ExecuteTemplate();
            switch (so.Result)
            {
                case LoginResult.Success:
                    break;
                case LoginResult.TwoFactorRequired:
                    break;
                case LoginResult.UserNotFound:
                    throw new InvalidOperationException("User does not exist!");
                case LoginResult.WrongPassword:
                    throw new InvalidOperationException("Wrong password!");
                case LoginResult.WrongTwoFactorCode:
                    break;
                case LoginResult.InTimeout:
                    throw new InvalidOperationException("Too many failed attempts, try again later.");
                default:
                    break;
            }
            return so.Result;
        }
    }
}
