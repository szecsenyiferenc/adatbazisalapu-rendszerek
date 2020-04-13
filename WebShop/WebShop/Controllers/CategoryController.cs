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
    public class CategoryController : ControllerBase
    {

        CategoryProvider categoryProvider = new CategoryProvider();

        //// GET: api/Category
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Category/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET: api/Category
        [HttpGet]
        public List<Category> Get()
        {
            return categoryProvider.GetCategories();
        }

        // POST: api/Category
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Category/5
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
