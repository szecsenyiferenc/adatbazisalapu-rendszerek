using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Comment
    {
        public Comment()
        {

        }

        public Comment(Customer customer, Product product, DateTime dateTime, string text)
        {
            Customer = customer;
            Product = product;
            DateTime = dateTime;
            Text = text;
        }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("product")]
        public Product Product { get; set; }
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
