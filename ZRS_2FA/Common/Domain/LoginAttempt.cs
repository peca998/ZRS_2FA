using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class LoginAttempt : IEntity
    {
        public long AttemptId { get; set; }
        public long UserId { get; set; }
        public DateTime AttemptTime { get; set; }
        public bool Success { get; set; }
        public LoginAttemptType AttemptType { get; set; }
        
        public string GetTableName() => "LoginAttempts";
        public string GetInsertColumns() => "UserId, AttemptTime, Success, AttemptType";
        public Dictionary<string, object?> GetInsertValues() => new()
        {
            {"@UserId", UserId},
            {"@AttemptTime", AttemptTime},
            {"@Success", Success},
            {"@AttemptType", AttemptType}
        };
        public LoginAttempt ReadFromReader(SqlDataReader reader)
        {
            return new LoginAttempt
            {
                AttemptId = (long)reader["AttemptId"],
                UserId = (long)reader["UserId"],
                AttemptTime = (DateTime)reader["AttemptTime"],
                Success = (bool)reader["Success"],
                AttemptType = (LoginAttemptType)Enum.Parse(typeof(LoginAttemptType), reader["AttemptType"].ToString() ?? string.Empty)
            };
        }

    }
}
