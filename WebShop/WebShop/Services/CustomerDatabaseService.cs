using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Models.DatabaseModels;
using WebShop.Models.DomainModels;

namespace WebShop.Services.DatabaseServices
{
    public class CustomerDatabaseService : DatabaseServiceBase
    {
        public CustomerModel CheckLogin(LoginData loginData)
        {
            CustomerModel customer = GetCustomers(loginData.Email).FirstOrDefault();
            if(customer != null && customer.Pass == loginData.Password)
            {
               return customer;
            }
            return null;
        }
        public List<CustomerModel> GetCustomers(string email = null)
        {
            string[] columns = new string[]{
                "Email",
                "Pass",
                "FirstName",
                "LastName",
                "Balance",
                "Phone",
                "IsRegularCustomer",
                "City",
                "Street",
                "HouseNumber",
                "IsAdmin"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Customer";
            string queryString = email != null ? query + $" WHERE Customer.Email = '{email}';" : query;
            Func<SqlDataReader, CustomerModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<CustomerModel>(prop);
            };
            return QueryDatabase(queryString, queryFunction);
        }
        public List<CustomerModel> GetCustomersWithProperties()
        {
            var customers = GetCustomers();
            AddVisitedProductsToCustomers(customers);
            //AddPurhasedProductsToCustomers(customers);
            AddCommentsToCustomers(customers);
            AddLikesToCustomers(customers);
            return customers;
        }
        public void AddVisitedProductsToCustomers(List<CustomerModel> customers)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "TimesOfVisit"
            };
            string[] products = new string[]
            {
                 "Id",
                "Name",
                "Price"
            };
            foreach (var customer in customers)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", products)} FROM VisitedProducts " +
                    "JOIN Customer " +
                    "ON Customer.Email = VisitedProducts.UserId " +
                    "JOIN Product " +
                    "ON Product.Id = VisitedProducts.ProductId " +
                    $"WHERE Customer.Email = '{customer.Email}';";
                Func<SqlDataReader, VisitModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[columns.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        prop[i] = sqlreader.GetValue(i);
                    }
                    object[] prop2 = new object[products.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i);
                    }
                    var visited = CreateInstance<VisitModel>(prop);
                    visited.Product = CreateInstance<ProductModel>(prop2);
                    return visited;
                };
                customer.VisitedProducts = QueryDatabase(query, queryFunction);
            }
        }
        public void AddPurhasedProductsToCustomers(List<CustomerModel> customers)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "Status.Id",
                "Status.Name"
            };
            string[] products = new string[]
            {
                "Product.Id",
                "Product.Name",
                "Price"
            };
            foreach (var customer in customers)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", products)} " + 
                    "FROM PurchasedProducts, Status, Product " +
                    $"WHERE PurchasedProducts.UserId = '{customer.Email}' " + 
                    "AND PurchasedProducts.StatusId = Status.Id " +
                    "AND PurchasedProducts.ProductId = Product.Id;";
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
                    object[] prop2 = new object[products.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i+1);
                    }
                    var purchased = CreateInstance<PurchaseModel>(prop);
                    purchased.Product = CreateInstance<ProductModel>(prop2);
                    return purchased;
                };
                customer.PurhasedProducts = QueryDatabase(query, queryFunction);
            }
        }
        public void AddCommentsToCustomers(List<CustomerModel> customers)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "CommentTime",
                "CommentText",
            };
            string[] products = new string[]
            {
                "Product.Id",
                "Product.Name",
                "Price"
            };
            foreach (var customer in customers)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", products)} FROM Comment " +
                    "JOIN Customer " +
                    "ON Customer.Email = Comment.UserId " +
                    "JOIN Product " +
                    "ON Product.Id = Comment.ProductId " +
                    $"WHERE Customer.Email = '{customer.Email}';";
                Func<SqlDataReader, CommentModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[columns.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        prop[i] = sqlreader.GetValue(i);
                    }
                    object[] prop2 = new object[products.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i);
                    }
                    var comment = CreateInstance<CommentModel>(prop);
                    comment.Product = CreateInstance<ProductModel>(prop2);
                    return comment;
                };
                customer.Comments = QueryDatabase(query, queryFunction);
            }
        }
        public void AddLikesToCustomers(List<CustomerModel> customers)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "IsLiked"
            };
            string[] products = new string[]
            {
                "Product.Id",
                "Product.Name",
                "Price"
            };
            foreach (var customer in customers)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", products)} FROM Opinion " +
                    "JOIN Customer " +
                    "ON Customer.Email = Opinion.UserId " +
                    "JOIN Product " +
                    "ON Product.Id = Opinion.ProductId " +
                    $"WHERE Customer.Email = '{customer.Email}';";
                Func<SqlDataReader, LikeModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[columns.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        var value = sqlreader.GetValue(i);
                        value = value.ToString() != "" ? value : null;
                        prop[i] = value;
                    }
                    object[] prop2 = new object[products.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        prop2[i - prop.Length] = sqlreader.GetValue(i);
                    }
                    var comment = CreateInstance<LikeModel>(prop);
                    comment.Product = CreateInstance<ProductModel>(prop2);
                    return comment;
                };
                customer.Likes = QueryDatabase(query, queryFunction);
            }
        }
        public bool AddCustomerToDb(CustomerModel customerModel) 
        {
            string[] columns = new string[]{
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
            string query = $"INSERT INTO Customer ({String.Join(",", columns)}) VALUES('{customerModel.Email}', '{customerModel.Pass}'," +
                $"'{customerModel.FirstName}','{customerModel.LastName}',{customerModel.Balance},'{customerModel.Phone}',{ConvertBoolToInt(customerModel.IsRegularCustomer)}," +
                $"'{customerModel.City}','{customerModel.Street}',{customerModel.HouseNumber})";

            ExecuteQuery(query);

            return true;
        }
        public int ConvertBoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public bool UploadBalanceInDatabase(Customer customer)
        {
            string query = $"UPDATE Customer SET Balance = {customer.Balance} WHERE Email = '{customer.Email}'";

            return ExecuteQuery(query);
        }
    }
}
