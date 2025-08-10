using Microsoft.Data.SqlClient;

namespace DBBroker
{
    public class DBConnection
    {
        private readonly SqlConnection _connection;
        private SqlTransaction? _transaction;

        public DBConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            if(_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void Close() { 
            if(_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public void BeginTransaction()
        {
            if(_connection.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection must be open to begin a transaction.");
            }
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }

        public SqlCommand CreateCommand(string cmd = "")
        {
            return new SqlCommand(cmd, _connection, _transaction);
        }
    }
}
