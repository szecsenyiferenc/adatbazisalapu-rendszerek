using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Facades;
using WebShop.Factories;
using WebShop.Models.DomainModels;

namespace WebShop.Providers
{
    public class CustomerProvider
    {
        private DatabaseFacade db = new DatabaseFacade();
        private CustomerFactory fc = new CustomerFactory();

        public CustomerProvider()
        {

        }

        public bool UploadBalanceInDatabase(Customer customer)
        {
            return db.Customers.UploadBalanceInDatabase(customer);
        }

        public bool DeleteCustomerFromDatabase(string userId)
        {
            return db.Customers.DeleteCustomerFromDatabase(userId);
        }

        public Customer UpdateCustomerFromDatabase(RegistrationCustomer customer)
        {
            if (db.Customers.UpdateCustomerFromDatabase(customer))
            {
                var customers = db.Customers.GetCustomers(customer.Email);
                var result = customers.FirstOrDefault(c => c.Email == customer.Email);
                if (result != null)
                {
                    return fc.CreateCustomer(result);
                }
            }
            return null;
        }
    }
}