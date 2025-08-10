using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class User : IEntity
    {
        public long Id { get;set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string TwoFactorSecret { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public string GetTableName() => "Users";
        public string GetInsertColumns() => "Username, PasswordHash, Salt, TwoFaSecretKey, TwoFactorEnabled";
        public Dictionary<string, object?> GetInsertValues() => new()
        {
            {"@Username", Username},
            {"@PasswordHash", HashedPassword},
            {"@Salt", Salt },
            {"@TwoFaSecretKey", TwoFactorSecret},
            {"@TwoFactorEnabled", TwoFactorEnabled}
        };

        public static User ReadFromReader(SqlDataReader reader)
        {
            User u = new()
            {
                Id = (long)reader["UserId"],
                Username = reader["Username"] as string ?? string.Empty,
                HashedPassword = reader["PasswordHash"] as string ?? string.Empty,
                Salt = reader["Salt"] as string ?? string.Empty,
                TwoFactorSecret = reader["TwoFaSecretKey"] as string ?? string.Empty,
                TwoFactorEnabled = (bool)reader["TwoFactorEnabled"]
            };
            return u;
        }
    }
}
