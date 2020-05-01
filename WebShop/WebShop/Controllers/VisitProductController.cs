using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebShop.Models.DomainModels;
using WebShop.Providers;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitProductController : ControllerBase
    {
        ProductProvider productProvider = new ProductProvider();

        // GET: api/VisitProduct
        [HttpGet("{id}", Name = "Asd")]
        public List<VisitedProduct> Asd(string id)
        {
            return productProvider.GetVisitedProductByUserId(id);
        }

        // POST: api/VisitProduct
        [HttpPost]
        public void Post(int id, [FromBody] object value)
        {
            JObject o = JObject.Parse(value.ToString());
            List<JProperty> props = o.Properties().ToList();

            Customer customer = JsonConvert.DeserializeObject<Customer>(props[0].Value.ToString());
            Product product = JsonConvert.DeserializeObject<Product>(props[1].Value.ToString());

            productProvider.AddVisitedProductToDatabase(customer, product);
        }

        // PUT: api/VisitProduct/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
