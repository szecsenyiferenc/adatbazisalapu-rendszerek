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
    public class CustomerController : ControllerBase
    {

        CustomerProvider customerProvider = new CustomerProvider();

        //// GET: api/Customer
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[Route("api/customer/{id}")]
        //// GET: api/Customer/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [Route("api/customer")]
        // POST: api/Customer
        [HttpPost]
        public bool Post([FromBody] Customer customer)
        {
            return customerProvider.UploadBalanceInDatabase(customer);
        }

        //// PUT: api/Customer/5
        [HttpPut("api/customer/{id}")]
        public Customer Put(int id, [FromBody] RegistrationCustomer value)
        {
            return customerProvider.UpdateCustomerFromDatabase(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("api/customer/{id}")]
        public bool Delete(string id)
        {
            return customerProvider.DeleteCustomerFromDatabase(id);
        }
    }
}
