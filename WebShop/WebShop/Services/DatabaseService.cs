using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services
{
    public class DatabaseService
    {
        string serverName = "DESKTOP-XXXXXXX";
        string databaseName = "WebShop";

        public DatabaseService()
        {
            var res = GetProducts();
            Console.WriteLine(res);
        }

        private List<Product> GetProducts()
        {
            string query = "SELECT Id, Name, Price FROM Product";
            var queryParams = new List<QueryParam>()
            {
                new QueryParam("Id", QueryParamType.Int),
                new QueryParam("Name", QueryParamType.String),
                new QueryParam("Price", QueryParamType.Double)
            };

            return QueryDatabase<List<Product>>(query, queryParams.ToArray());
        }

        private T QueryDatabase<T>(string query, params QueryParam[] list)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(@$"Server={serverName};Database={databaseName};Trusted_Connection=True;"))
            {
                connection.Open();
                string value = null;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("[");
                        while (reader.Read())
                        {
                            str.Append("{");
                            for (int i = 0; i < list.Length; i++)
                            {
                                switch (list[i].QueryParamType)
                                {
                                    case QueryParamType.Int:
                                        str.Append($"\"{list[i].PropertyName}\":\"{reader.GetInt32(i)}\"");
                                        break;
                                    case QueryParamType.Boolean:
                                        str.Append($"\"{list[i].PropertyName}\":\"{reader.GetBoolean(i)}\"");
                                        break;
                                    case QueryParamType.DateTime:
                                        str.Append($"\"{list[i].PropertyName}\":\"{reader.GetDateTime(i)}\"");
                                        break;
                                    case QueryParamType.Double:
                                        str.Append($"\"{list[i].PropertyName}\":\"{reader.GetDouble(i)}\"");
                                        break;
                                    case QueryParamType.String:
                                    default:
                                        str.Append($"\"{list[i].PropertyName}\":\"{reader.GetString(i)}\"");
                                        break;
                                }

                                if(i != list.Length - 1)
                                {
                                    str.Append(",");
                                }

                            }
                            str.Append("},");
                        }
                        str.Append("]");
                        value = str.ToString();
                    }
                }


                T result = JsonConvert.DeserializeObject<T>(value);

                return result;

            }
        }
    }
}
