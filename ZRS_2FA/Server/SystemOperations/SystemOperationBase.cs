using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperations
{
    public abstract class SystemOperationBase
    {
        protected Broker _broker;
        public SystemOperationBase()
        {
            _broker = new Broker();
        }

        public void ExecuteTemplate()
        {
            try
            {
                _broker.OpenConnection();
                _broker.BeginTransaction();
                Execute();
                _broker.CommitTransaction();
            }
            catch (Exception)
            {
                _broker.RollbackTransaction();
                throw;
            }
            finally
            {
                _broker.CloseConnection();
            }
        }

        public abstract void Execute();
    }
}
