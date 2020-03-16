using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Facades;
using WebShop.Factories;
using WebShop.Models;
using WebShop.Models.DomainModels;

namespace WebShop.Providers
{
    public class LoginProvider
    {
        private DatabaseFacade db = new DatabaseFacade();
        private CustomerFactory cf = new CustomerFactory();

        public Customer CheckLogin(LoginData loginData)
        {
            var customerModel = db.Customers.CheckLogin(loginData);
            if(customerModel == null)
            {
                return null;
            }
            var customer = cf.CreateCustomer(customerModel);
            return customer;
        }
    }
}
