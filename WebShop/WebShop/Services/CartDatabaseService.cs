using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;
using WebShop.Services.DatabaseServices;

namespace WebShop.Services
{
    public class CartDatabaseService : DatabaseServiceBase
    {
        public bool AddCartToDatabase(Cart cart) {
            try
            {
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
    }
}
