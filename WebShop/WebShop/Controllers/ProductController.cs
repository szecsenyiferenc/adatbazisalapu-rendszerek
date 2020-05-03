using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.DomainModels;
using WebShop.Providers;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductProvider productProvider = new ProductProvider();
        
        // GET: api/Product
        [HttpGet]
        public List<Product> Get()
        {
            return productProvider.GetProducts();
        }

        // GET: api/Product/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Product
        [HttpPost]
        public bool Post([FromBody] Product product)
        {
            return productProvider.AddProductToDatabase(product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Product value)
        {
            return productProvider.UpdateProductFromDatabase(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return productProvider.DeleteProductFromDatabase(id);
        }
    }
}
