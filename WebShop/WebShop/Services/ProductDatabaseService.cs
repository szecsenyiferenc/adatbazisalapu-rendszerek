using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DatabaseModels;

namespace WebShop.Services.DatabaseServices
{
    public class ProductDatabaseService : DatabaseServiceBase
    {
        public List<ProductModel> GetProducts()
        {
            string[] columns = new string[]{
                "Id",
                "Name",
                "Price",
                "Image"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Product";
            Func<SqlDataReader, ProductModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<ProductModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }
        public List<ProductModel> GetProductsWithProperties()
        {
            var products = GetProducts();
            AddVisitedProductsToProducts(products);
            AddPurhasedProductsToProducts(products);
            AddCommentsToProducts(products);
            AddLikesToProducts(products);
            return products;
        }
        public void AddVisitedProductsToProducts(List<ProductModel> products)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "TimesOfVisit"
            };
            string[] customers = new string[]
            {
                "Email",
                "Pass",
                "FirstName",
                "LastName",
                "Balance",
                "Phone",
                "IsRegularCustomer",
                "City",
                "Street",
                "HouseNumber"
            };
            foreach (var product in products)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", customers)} FROM VisitedProducts " +
                    "JOIN Product " +
                    "ON Product.Id = VisitedProducts.ProductId " +
                    "JOIN Customer " +
                    "ON Customer.Email = VisitedProducts.UserId " +
                    $"WHERE Product.Id = '{product.Id}';";
                Func<SqlDataReader, VisitModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[columns.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        prop[i] = sqlreader.GetValue(i);
                    }
                    object[] prop2 = new object[customers.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i);
                    }
                    var visited = CreateInstance<VisitModel>(prop);
                    visited.Customer = CreateInstance<CustomerModel>(prop2);
                    return visited;
                };
                product.VisitedProducts = QueryDatabase(query, queryFunction);
            }
        }
        public void AddPurhasedProductsToProducts(List<ProductModel> products)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "Status.Id",
                "Status.Name"
            };
            string[] customers = new string[]
            {
                "Email",
                "Pass",
                "FirstName",
                "LastName",
                "Balance",
                "Phone",
                "IsRegularCustomer",
                "City",
                "Street",
                "HouseNumber"
            };
            foreach (var product in products)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", customers)} FROM PurchasedProducts, Status, Customer " +
                    $"WHERE PurchasedProducts.ProductId = '{product.Id}' " + 
                    "AND PurchasedProducts.StatusId = Status.Id " +
                    "AND PurchasedProducts.UserId = Customer.Email;"; ;
                Func<SqlDataReader, PurchaseModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[3];

                    for (int i = 0; i < prop.Length; i++)
                    {
                        if (i == 2)
                        {
                            object[] statusProp = new object[2]
                            {
                                sqlreader.GetValue(2),
                                sqlreader.GetValue(3)
                            };
                            prop[i] = CreateInstance<StatusModel>(statusProp);
                            continue;
                        }
                        prop[i] = sqlreader.GetValue(i);

                    }
                    object[] prop2 = new object[customers.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i + 1);
                    }
                    var purchased = CreateInstance<PurchaseModel>(prop);
                    purchased.Customer = CreateInstance<CustomerModel>(prop2);
                    return purchased;
                };
                product.PurhasedProducts = QueryDatabase(query, queryFunction);
            }
        }
        public void AddCommentsToProducts(List<ProductModel> products)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "CommentTime",
                "CommentText",
            };
            string[] customers = new string[]
            {
                "Email",
                "Pass",
                "FirstName",
                "LastName",
                "Balance",
                "Phone",
                "IsRegularCustomer",
                "City",
                "Street",
                "HouseNumber"
            };
            foreach (var product in products)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", customers)} FROM Comment " +
                    "JOIN Product " +
                    "ON Product.Id = Comment.ProductId " +
                    "JOIN Customer " +
                    "ON Customer.Email = Comment.UserId " +
                    $"WHERE Product.Id = '{product.Id}';";
                Func<SqlDataReader, CommentModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[columns.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        prop[i] = sqlreader.GetValue(i);
                    }
                    object[] prop2 = new object[customers.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i);
                    }
                    var comments = CreateInstance<CommentModel>(prop);
                    comments.Customer = CreateInstance<CustomerModel>(prop2);
                    return comments;
                };
                product.Comments = QueryDatabase(query, queryFunction);
            }
        }
        public void AddLikesToProducts(List<ProductModel> products)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "IsLiked"
            };
            string[] customers = new string[]
            {
                "Email",
                "Pass",
                "FirstName",
                "LastName",
                "Balance",
                "Phone",
                "IsRegularCustomer",
                "City",
                "Street",
                "HouseNumber"
            };
            foreach (var product in products)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", customers)} FROM Opinion " +
                    "JOIN Product " +
                    "ON Product.Id = Opinion.ProductId " +
                    "JOIN Customer " +
                    "ON Customer.Email = Opinion.UserId " +
                    $"WHERE Product.Id = '{product.Id}';";
                Func<SqlDataReader, LikeModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[columns.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        prop[i] = sqlreader.GetValue(i);
                    }
                    object[] prop2 = new object[customers.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i);
                    }
                    var likes = CreateInstance<LikeModel>(prop);
                    likes.Customer = CreateInstance<CustomerModel>(prop2);
                    return likes;
                };
                product.Likes = QueryDatabase(query, queryFunction);
            }
        }
    }
}
