using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Services.DatabaseServices;

namespace WebShop.Facades
{
    public class DatabaseFacade
    {
        public DatabaseFacade()
        {
            Customers = new CustomerDatabaseService();
            Products = new ProductDatabaseService();
            Categories = new CategoryDatabaseService();
            Storages = new StorageDatabaseService();
            Statuses = new StatusDatabaseService();
        }

        public CustomerDatabaseService Customers { get; }
        public ProductDatabaseService Products { get; }
        public CategoryDatabaseService Categories { get; }
        public StorageDatabaseService Storages { get; }
        public StatusDatabaseService Statuses { get; }
    }
}
