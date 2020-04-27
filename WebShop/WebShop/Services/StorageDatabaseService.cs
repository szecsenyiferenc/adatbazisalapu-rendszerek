using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DatabaseModels;

namespace WebShop.Services.DatabaseServices
{
    public class StorageDatabaseService : DatabaseServiceBase
    {
        public List<StorageModel> GetStorages()
        {
            string[] columns = new string[]{
                "Id",
                "Location"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Storage";
            Func<SqlDataReader, StorageModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    prop[i] = sqlreader.GetValue(i);
                }
                return CreateInstance<StorageModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }

        public List<StoragedProductModel> GetStoragedProduct()
        {
            string[] columns = new string[]{
                "StorageId",
                "ProductId",
                "Quantity"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM ProductOnStorage";
            Func<SqlDataReader, StoragedProductModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    prop[i] = sqlreader.GetValue(i);
                }
                return CreateInstance<StoragedProductModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }
    }
}
