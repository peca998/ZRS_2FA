using Common.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBroker
{
    public class Broker
    {
        private readonly DBConnection _dbConnection;
        public Broker()
        {
            _dbConnection = new DBConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Zrs_Projekat;Integrated Security=True;");
        }
        public void OpenConnection()
        {
            _dbConnection.Open();
        }
        public void CloseConnection()
        {
            _dbConnection.Close();
        }
        public void BeginTransaction()
        {
            _dbConnection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _dbConnection.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            _dbConnection.RollbackTransaction();
        }

        public User? GetUser(string username)
        {
            string cmdText = $"SELECT * FROM Users WHERE Username = @Username";
            using SqlCommand cmd = _dbConnection.CreateCommand(cmdText);
            cmd.Parameters.AddWithValue("@Username", username);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return User.ReadFromReader(reader);
            }
            return null;
        }
        public (int attemptCount, DateTime? lastFailedAttempt) GetLoginAttempts(long userId, DateTime sinceTime)
        {
            const string cmdText = @"
            SELECT 
              COUNT(*) AS AttemptCount, 
              MAX(AttemptTime) AS LastFailedAttempt
            FROM LoginAttempts 
            WHERE UserId = @UserId 
              AND Success = 0 
              AND AttemptTime >= @SinceTime";

            using SqlCommand cmd = _dbConnection.CreateCommand(cmdText);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@SinceTime", sinceTime);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int attemptCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                DateTime? lastFailedAttempt = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1);
                return (attemptCount, lastFailedAttempt);
            }
            else
            {
                return (0, null);
            }
        }

        public void Insert(IEntity e)
        {
            string keys = string.Join(", ", e.GetInsertValues().Keys);
            string cmdText = $"INSERT INTO {e.GetTableName()} ({e.GetInsertColumns()}) VALUES ({keys})";
            using SqlCommand cmd = _dbConnection.CreateCommand(cmdText);
            foreach (var kvp in e.GetInsertValues())
            {
                cmd.Parameters.AddWithValue(kvp.Key, kvp.Value ?? DBNull.Value);
            }
            cmd.ExecuteNonQuery();
        }
    }
}
