using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Facades;
using WebShop.Factories;
using WebShop.Models.DomainModels;

namespace WebShop.Providers
{
    public class CartProvider
    {
        private DatabaseFacade db = new DatabaseFacade();
        private CustomerFactory cf = new CustomerFactory();

        public CartProvider()
        {

        }

        public bool AddCartToDatabase(Cart cart)
        {
            return db.Cart.AddCartToDatabase(cart);
        }

        public List<Cart> GetCartsFromDatabase(string id)
        {
            var cartModels = db.Cart.GetCartsFromDatabaseWithProperties(id);
            var carts = new List<Cart>();

            foreach (var cartModel in cartModels)
            {
                carts.Add(cf.CreateCart(cartModel));
            }

            return carts;
        }
    }
}
