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
    }
}
