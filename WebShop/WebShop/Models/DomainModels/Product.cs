using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Product
    {
        public Product()
        {

        }
        public Product(int id, string name, double price, byte[] image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
            Likes = 0;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("image")]
        public byte[] Image { get; set; }
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
        [JsonProperty("likes")]
        public int Likes { get; set; }
    }
}
