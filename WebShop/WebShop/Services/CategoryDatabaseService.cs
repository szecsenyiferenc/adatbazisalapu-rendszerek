using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DatabaseModels;

namespace WebShop.Services.DatabaseServices
{
    public class CategoryDatabaseService : DatabaseServiceBase
    {
        public List<CategoryModel> GetCategories()
        {
            string[] columns = new string[]{
                "Id",
                "Name"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Category";
            Func<SqlDataReader, CategoryModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    prop[i] = sqlreader.GetValue(i);
                }
                return CreateInstance<CategoryModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }
    }
}
