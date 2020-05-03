using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class RegistrationCustomer : Customer
    {

        public RegistrationCustomer() : base()
        {

        }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
