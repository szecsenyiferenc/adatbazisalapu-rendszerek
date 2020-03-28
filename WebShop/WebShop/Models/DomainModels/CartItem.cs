using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class CartItem
    {
        public CartItem()
        {

        }

        [JsonProperty("product")]
        public Product Product { get; set; }
        [JsonProperty("quantity")]

        public int Quantity { get; set; }
    }
}
