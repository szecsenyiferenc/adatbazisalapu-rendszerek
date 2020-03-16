using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Facades;
using WebShop.Factories;
using WebShop.Models.DomainModels;

namespace WebShop.Services
{
    public class RegistrationService
    {
        DatabaseFacade db = new DatabaseFacade();
        CustomerFactory fc = new CustomerFactory();

        public bool RegisterUser(RegistrationCustomer customer)
        {
            var customerModel = fc.CreateCustomerModel(customer);
            return db.Customers.AddCustomerToDb(customerModel);
        }
    }
}
