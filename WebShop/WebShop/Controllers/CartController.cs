using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Factories;
using WebShop.Models.DomainModels;
using WebShop.Providers;

namespace WebShop.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        CartProvider cartProvider = new CartProvider();

        [Route("api/cart/{id}")]
        [HttpGet]
        public List<Cart> Get(string id)
        {
            return cartProvider.GetCartsFromDatabase(id);
        }

        // POST: api/Cart
        [Route("api/cart")]
        [HttpPost]
        public bool Post([FromBody] Cart cart)
        {
            return cartProvider.AddCartToDatabase(cart);
        }


    }
}
