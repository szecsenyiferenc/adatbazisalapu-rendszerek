using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DatabaseModels;
using WebShop.Models.DomainModels;
using WebShop.Services.DatabaseServices;

namespace WebShop.Services
{
    public class CartDatabaseService : DatabaseServiceBase
    {
        public bool AddCartToDatabase(Cart cart) {
            try
            {
                foreach (var cartItem in cart.CartItems)
                {
                    string query2 = $"UPDATE ProductOnStorage SET Quantity = Quantity - {cartItem.Quantity}" +
                    $"WHERE ProductId = {cartItem.Product.Id} AND " +
                    $"StorageId = (SELECT TOP (1) StorageId FROM ProductOnStorage WHERE ProductId = '{cartItem.Product.Id}' ORDER BY Quantity DESC)";
                    ExecuteQuery(query2);
                }

                string[] columns = new string[]{
                "UserId",
                "PurchaseDate",
                "StatusId",
                };
                string query = $"INSERT INTO Cart ({String.Join(",", columns)}) VALUES('{cart.Customer.Email}', "+
                    $"'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 1); SELECT SCOPE_IDENTITY()";

                int cartId = InsertQuery(query);

                if(cartId == -1)
                {
                    return false;
                }

                return AddCartItemToDatabase(cart, cartId);
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<CartModel> GetCartsFromDatabase(string id)
        {
            string[] columns = new string[]{
                "CartId",
                "UserId",
                "PurchaseDate",
                "StatusId"
            };
            string query = $"SELECT {String.Join(",", columns)} FROM Cart WHERE UserId = '{id}'";
            Func<SqlDataReader, CartModel> queryFunction = sqlreader =>
            {
                object[] prop = new object[columns.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    var value = sqlreader.GetValue(i);
                    value = value.ToString() != "" ? value : null;
                    prop[i] = value;
                }
                return CreateInstance<CartModel>(prop);
            };
            return QueryDatabase(query, queryFunction);
        }

        private bool AddCartItemToDatabase(Cart cart, int cartId)
        {
            try
            {
                foreach (var cartItem in cart.CartItems)
                {

                    string[] columns = new string[]{
                        "CartId",
                        "ProductId",
                        "Quantity"
                    };
                    string query = $"INSERT INTO PurchasedProducts ({String.Join(",", columns)}) VALUES({cartId}, " + 
                        $"{cartItem.Product.Id}, {cartItem.Quantity})";

                    ExecuteQuery(query);
                }
              
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<CartModel> GetCartsFromDatabaseWithProperties(string id)
        {
            var cartModels = GetCartsFromDatabase(id);
            AddCartItemsToCart(cartModels);
            return cartModels;
        }

        public void AddCartItemsToCart(List<CartModel> cartModels)
        {

                string[] columns = new string[]{
                    "CartId",
                    "ProductId",
                    "Quantity"
                };
                string[] products = new string[]
                {
                    "Id",
                    "Name",
                    "Price",
                    "Image"
                };
                foreach (var cartModel in cartModels)
                {
                    string query = $"SELECT {String.Join(",", columns)},{String.Join(",", products)} FROM PurchasedProducts " +
                        "JOIN Product " +
                        "ON Product.Id = PurchasedProducts.ProductId " +
                        $"WHERE CartId = '{cartModel.Id}';";
                    Func<SqlDataReader, CartItemModel> queryFunction = sqlreader =>
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
                        var cartItemModel = CreateInstance<CartItemModel>(prop);
                        cartItemModel.Product = CreateInstance<ProductModel>(prop2);
                        return cartItemModel;
                    };
                    cartModel.CartItems = QueryDatabase(query, queryFunction);
                }
            





           
        }


    }
}
