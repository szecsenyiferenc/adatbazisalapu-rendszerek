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
    [ApiController]
    public class StorageController : ControllerBase
    {

        ProductProvider productProvider = new ProductProvider();

        // GET: api/Storage
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Storage/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [Route("api/storage/{id}")]
        [HttpGet]
        public int Get(int id)
        {
            return productProvider.GetStockFromDatabase(id);
        }

       // POST: api/Storage
       //[Route("api/storage/{id}")]
       //[HttpPost]
       // public bool Post([FromBody] CartItem cartItem)
       // {
       //     return productProvider.ReduceStockFromDatabase(cartItem);
       // }

        //// PUT: api/Storage/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
