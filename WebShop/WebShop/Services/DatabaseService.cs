using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services
{
    public class DatabaseService
    {
        string serverName = "DESKTOP-5FVPMH2";
        string databaseName = "WebShop";

        public DatabaseService()
        {
        }

        public List<CategoryModel> GetAllCategories()
        {
            return GetData<CategoryModel>();
        }

        public List<ProductModel> GetAllProduct()
        {
            return GetData<ProductModel>();
        }

        public List<CustomerModel> GetAllCustomer()
        {
            
        }

        public List<StorageModel> GetAllStorage()
        {
            return GetData<StorageModel>();
        }

        private List<T> GetData<T>(Dictionary<string,string> propertyOverrides = null, Dictionary<string, string> columnOverrides = null)
        {
            var queryParams = new List<QueryParam>();
           
            Type type = typeof(T);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                queryParams.Add(new QueryParam(prop.Name, prop.PropertyType));
            }

            string query = $"SELECT {CreateSqlParams(queryParams)} FROM {type.Name}";

            return QueryDatabase<T>(query, queryParams.ToArray());
        }

        private string CreateSqlParams(List<QueryParam> parameters)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < parameters.Count; i++)
            {
                str.Append(parameters[i].PropertyName);
                if(i != parameters.Count - 1)
                {
                    str.Append(",");
                }
                str.Append(" ");
            }
            return str.ToString();
        }

        private T CreateInstance<T>(params object[] paramArray)
        {
            return (T)Activator.CreateInstance(typeof(T), args: paramArray);
        }

        private List<T> QueryDatabase<T>(string query, params QueryParam[] list)
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
                            object[] paramArray = new object[list.Length];
                            for (int i = 0; i < list.Length; i++)
                            {
                                switch (list[i].QueryParamType)
                                {
                                    case QueryParamType.Int:
                                        paramArray[i] = reader.GetInt32(i);
                                        break;
                                    case QueryParamType.Boolean:
                                        paramArray[i] = reader.GetBoolean(i);
                                        break;
                                    case QueryParamType.DateTime:
                                        paramArray[i] = reader.GetDateTime(i);
                                        break;
                                    case QueryParamType.Double:
                                        paramArray[i] = reader.GetDouble(i);
                                        break;
                                    case QueryParamType.Decimal:
                                        paramArray[i] = reader.GetDecimal(i);
                                        break;
                                    case QueryParamType.String:
                                    default:
                                        paramArray[i] = reader.GetString(i);
                                        break;
                                }
                            }
                            result.Add(CreateInstance<T>(paramArray));
                        }
                    }
                }
                return result;
            }
        }
    }
}
