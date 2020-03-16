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

        public string Password { get; set; }

    }
}
