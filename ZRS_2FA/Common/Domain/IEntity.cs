using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public interface IEntity
    {
        public string GetTableName();
        public string GetInsertColumns();
        public Dictionary<string, object?> GetInsertValues();
    }
}
