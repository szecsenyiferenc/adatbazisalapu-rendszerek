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

        public CustomerProvider()
        {

        }

        public bool UploadBalanceInDatabase(Customer customer)
        {
            return db.Customers.UploadBalanceInDatabase(customer);
        }
    }
}