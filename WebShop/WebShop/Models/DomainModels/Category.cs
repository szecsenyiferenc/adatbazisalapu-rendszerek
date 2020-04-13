using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Category
    {
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        //public List<ProductCategoryModel> Products { get; set; }
    }
}
