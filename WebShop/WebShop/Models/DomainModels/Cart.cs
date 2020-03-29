using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Cart
    {
        public Cart(Customer customer, List<CartItem> cartItems)
        {
            Customer = customer;
            CartItems = cartItems;
        }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("cartItems")]
        public List<CartItem> CartItems { get; set; }
    }
}
