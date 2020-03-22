using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class SqlQueryParam
    {
        public SqlQueryParam(string parameterName, SqlDbType dbType, object value)
        {
            ParameterName = parameterName;
            DbType = dbType;
            Value = value;
        }

        public string ParameterName { get; set; }
        public SqlDbType DbType { get; set; }
        public object Value { get; set; }
    }
}
