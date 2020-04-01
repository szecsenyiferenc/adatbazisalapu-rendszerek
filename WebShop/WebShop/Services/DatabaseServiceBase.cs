using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services.DatabaseServices
{
    public class DatabaseServiceBase
    {
        protected readonly string serverName = "DESKTOP-DIJF3QE";
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

        protected bool ExecuteQuery(string query, List<SqlQueryParam> parameters = null)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(@$"Server={serverName};Database={databaseName};Trusted_Connection=True;"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if(parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                SqlParameter param = command.Parameters.Add(parameter.ParameterName, parameter.DbType);
                                param.Value = parameter.Value;
                            }
                        }

                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        protected int InsertQuery(string query, List<SqlQueryParam> parameters = null)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(@$"Server={serverName};Database={databaseName};Trusted_Connection=True;"))
                {
                    connection.Open();
                    int id = -1;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                SqlParameter param = command.Parameters.Add(parameter.ParameterName, parameter.DbType);
                                param.Value = parameter.Value;
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader.GetDecimal(0);
                            }
                        }
                    }
                    return id;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

    }
}
