using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Cart
    {

        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("cartItems")]
        public List<CartItem> CartItems { get; set; }
    }
}
