using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DatabaseModels;

namespace WebShop.Services.DatabaseServices
{
    public class StatusDatabaseService : DatabaseServiceBase
    {
        public List<StatusModel> GetStatuses()
        {
            string[] columns = new string[]{
                "Id",
                "Name"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Status";
            Func<SqlDataReader, StatusModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    prop[i] = sqlreader.GetValue(i);
                }
                return CreateInstance<StatusModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }
    }
}
