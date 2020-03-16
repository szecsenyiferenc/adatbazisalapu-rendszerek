using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Services.DatabaseServices
{
    public class DatabaseServiceBase
    {
        protected readonly string serverName = "DESKTOP-5FVPMH2";
        protected readonly string databaseName = "WebShop";



        public DatabaseServiceBase()
        {
     
        }

        protected T CreateInstance<T>(params object[] paramArray)
        {
            return (T)Activator.CreateInstance(typeof(T), args: paramArray);
        }

        protected List<T> QueryDatabase<T>(string query, Func<SqlDataReader, T> function)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(@$"Server={serverName};Database={databaseName};Trusted_Connection=True;"))
            {
                connection.Open();
                var result = new List<T>();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(function(reader));
                        }
                    }
                }
                return result;
            }
        }
    }
}
