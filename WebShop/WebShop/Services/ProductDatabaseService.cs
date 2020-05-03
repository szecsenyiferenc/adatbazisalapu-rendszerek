using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Models.DatabaseModels;
using WebShop.Models.DomainModels;

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
            //AddVisitedProductsToProducts(products);
            //AddPurhasedProductsToProducts(products);
            AddCommentsToProducts(products);
            AddLikesToProducts(products);
            AddCategoriesToProducts(products);
            return products;
        }
        public void AddCategoriesToProducts(List<ProductModel> products)
        {
            string[] categories = new string[]
            {
                "Category.Id",
                "Category.Name"
            };

            foreach (var product in products)
            {
                string query = $"SELECT {String.Join(",", categories)} FROM Category, Product, ProductCategory " +
                    "WHERE Product.Id = ProductCategory.ProductId " + 
                    "AND Category.Id = ProductCategory.CategoryId " +
                    $"AND Product.Id = '{product.Id}';";

                Func<SqlDataReader, CategoryModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[categories.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        prop[i] = sqlreader.GetValue(i);
                    }
                   
                    var prodCat = CreateInstance<CategoryModel>(prop);
                    return prodCat;
                };
                product.Categories = QueryDatabase(query, queryFunction);
            }
        }

        public void AddVisitedProductsToProducts(List<ProductModel> products, string userId)
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
                "HouseNumber",
                "IsAdmin"
            };
            foreach (var product in products)
            {
                string query = $"SELECT {String.Join(",", columns)},{String.Join(",", customers)} FROM VisitedProducts " +
                    "JOIN Product " +
                    "ON Product.Id = VisitedProducts.ProductId " +
                    "JOIN Customer " +
                    "ON Customer.Email = VisitedProducts.UserId " +
                    $"WHERE Product.Id = '{product.Id}' AND Customer.Email = '{userId}';";
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
                        var value = sqlreader.GetValue(i);
                        value = value.ToString() != "" ? value : null;
                        prop2[i - prop.Length] = value;
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
                        var value = sqlreader.GetValue(i + 1);
                        value = value.ToString() != "" ? value : null;
                        prop2[i - prop.Length] = value;
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
                "HouseNumber",
                "IsAdmin"
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
                        var value = sqlreader.GetValue(i);
                        value = value.ToString() != "" ? value : null;
                        prop2[i - prop.Length] = value;
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
                "HouseNumber",
                "IsAdmin"
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
                        var value = sqlreader.GetValue(i);
                        value = value.ToString() != "" ? value : null;
                        prop[i] = value;
                    }
                    object[] prop2 = new object[customers.Length];
                    for (int i = prop.Length; i < (prop.Length + prop2.Length); i++)
                    {
                        var value = sqlreader.GetValue(i);
                        value = value.ToString() != "" ? value : null;
                        prop2[i - prop.Length] = value;
                    }
                    var likes = CreateInstance<LikeModel>(prop);
                    likes.Customer = CreateInstance<CustomerModel>(prop2);
                    return likes;
                };
                product.Likes = QueryDatabase(query, queryFunction);
            }
        }
        public bool AddProductToDatabase(ProductModel productModel)
        {
            string[] columns = new string[]{
                "Name",
                "Price",
                "Image"
            };
            string query = $"INSERT INTO Product ({String.Join(",", columns)}) VALUES('{productModel.Name}',{productModel.Price}, @imagebinary)";

            SqlQueryParam param = new SqlQueryParam("@imagebinary", SqlDbType.VarBinary, productModel.Image);
            var paramList = new List<SqlQueryParam>() { param };

            return ExecuteQuery(query, paramList);
        }

        public bool AddCommentsToDatabase(Comment comment)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "CommentTime",
                "CommentText"
            };
            string query = $"INSERT INTO Comment ({String.Join(",", columns)}) VALUES('{comment.Customer.Email}', {comment.Product.Id}," +
                $" '{comment.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{comment.Text}')";


            return ExecuteQuery(query);
        }

        public List<CommentModel> GetCommentsFromProduct(int productId)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "CommentTime",
                "CommentText"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Comment WHERE ProductId = {productId}";
            Func<SqlDataReader, CommentModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<CommentModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }

        public List<CommentModel> GetCommentsFromProductWithProperties(int productId)
        {
            var comment = GetCommentsFromProduct(productId);
            AddCustomersToComments(comment);
            return comment;
        }

        public void AddCustomersToComments(List<CommentModel> comments)
        {
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
                "HouseNumber",
                "IsAdmin"
            };
            foreach (var comment in comments)
            {
                string query = $"SELECT {String.Join(",", customers)} FROM Customer " +
                    "JOIN Comment " +
                    "ON Customer.Email = Comment.UserId " +
                    $"WHERE Customer.Email = '{comment.CustomerId}';";
                Func<SqlDataReader, CustomerModel> queryFunction = sqlreader =>
                {
                    object[] prop = new object[customers.Length];
                    for (int i = 0; i < prop.Length; i++)
                    {
                        var value = sqlreader.GetValue(i);
                        value = value.ToString() != "" ? value : null;
                        prop[i] = value;
                    }
                    return CreateInstance<CustomerModel>(prop);
                };
                comment.Customer = QueryDatabase(query, queryFunction)[0];
            }
        }

        public bool AddLikeToDatabase(Like like)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "IsLiked"
            };

            var previouslike = GetLikeModelsFromDatabaseById(like);

            string query = null;

            if (previouslike == null || previouslike.Count == 0)
            {
                query = $"INSERT INTO Opinion ({String.Join(",", columns)}) VALUES('{like.Customer.Email}', {like.Product.Id}, NULL)";

                if (like.Value.HasValue)
                {
                    int likeValue = like.Value.Value ? 1 : 0;
                    query = $"INSERT INTO Opinion ({String.Join(",", columns)}) VALUES('{like.Customer.Email}', {like.Product.Id}, {likeValue})";
                }
            }
            else
            {
                query = $"UPDATE Opinion SET IsLiked = NULL WHERE ProductId = {like.Product.Id} AND UserId = '{like.Customer.Email}';";

                if (like.Value.HasValue)
                {
                    int likeValue = like.Value.Value ? 1 : 0;
                    query = $"UPDATE Opinion SET IsLiked = {likeValue} WHERE ProductId = {like.Product.Id} AND UserId = '{like.Customer.Email}';";
                }
            }

            return ExecuteQuery(query);
        }

        public bool AddVisitedProductToDatabase(string userId, int productId)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "TimesOfVisit"
            };

            var previousValue = GetVisitedProductFromDatabase(userId, productId);

            string query = null;

            int count = previousValue.Any() ? previousValue[0].TimesOfVisit + 1 : 1;

            if(count == 1)
            {
                query = $"INSERT INTO VisitedProducts({String.Join(",", columns)}) VALUES('{userId}', {productId}, {count})";
            }
            else
            {
                query = $"UPDATE VisitedProducts SET TimesOfVisit = {count} WHERE ProductId = {productId} AND UserId = '{userId}';";
            }

            return ExecuteQuery(query);
        }


        public List<LikeModel> GetLikeModelsFromDatabaseById(Like like)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "IsLiked"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Opinion WHERE ProductId = {like.Product.Id} AND UserId = '{like.Customer.Email}';";
            Func<SqlDataReader, LikeModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<LikeModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }

        public List<LikeModel> GetLikeModelsFromProduct(string userId)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "IsLiked"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Opinion WHERE UserId = '{userId}'";
            Func<SqlDataReader, LikeModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<LikeModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }

        public List<VisitModel> GetVisitedProductFromDatabase(string userId, int productId)
        {
            string[] columns = new string[]{
                "UserId",
                "ProductId",
                "TimesOfVisit"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM VisitedProducts WHERE UserId = '{userId}' AND ProductId = {productId}";
            Func<SqlDataReader, VisitModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<VisitModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }

        public bool DeleteProductFromDatabase(int productId)
        {
            string query = $"DELETE FROM Product WHERE Id = {productId}";
            return ExecuteQuery(query);
        }

        public bool UpdateProductFromDatabase(Product product)
        {
            string[] columns = new string[]{
                $"Name = '{product.Name}'",
                $"Price = {product.Price}"
            };
            string query = $"UPDATE Product SET {String.Join(",", columns)}, Image = @imagebinary WHERE Id = {product.Id}";

            SqlQueryParam param = new SqlQueryParam("@imagebinary", SqlDbType.VarBinary, product.Image);
            var paramList = new List<SqlQueryParam>() { param };

            return ExecuteQuery(query, paramList);
        }

    }

   
}
