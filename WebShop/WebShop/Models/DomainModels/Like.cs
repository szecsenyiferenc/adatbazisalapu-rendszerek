using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Like
    {
        public Like()
        {

        }

        public Like(Customer customer, Product product, bool? value)
        {
            Customer = customer;
            Product = product;
            Value = value;
        }

        public Like(string customerId, int? productId, bool? value)
        {
            Value = value;
            CustomerId = customerId;
            ProductId = productId;
        }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("product")]
        public Product Product { get; set; }
        [JsonProperty("value")]
        public bool? Value { get; set; }
        public string CustomerId { get; set; }
        public int? ProductId { get; set; }
    }
}
