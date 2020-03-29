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

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        [JsonProperty("product")]
        public Product Product { get; set; }
        [JsonProperty("quantity")]

        public int Quantity { get; set; }
    }
}
