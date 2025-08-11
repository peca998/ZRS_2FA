using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class BackupCode : IEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long UserId { get; set; }
        public bool IsUsed { get; set; }
        public string GetTableName() => "BackupCodes";
        public string GetInsertColumns() => "UserId, Code, IsUsed";
        public Dictionary<string, object?> GetInsertValues() => new()
        {
            {"@UserId", UserId},
            {"@Code", Code},
            {"@IsUsed", IsUsed}
        };
        public BackupCode ReadFromReader(SqlDataReader reader)
        {
            BackupCode backupCode = new()
            {
                Id = (long)reader["Id"],
                Code = reader["Code"] as string ?? string.Empty,
                UserId = (long)reader["UserId"],
                IsUsed = (bool)reader["IsUsed"]
            };
            return backupCode;
        }
    }
}
