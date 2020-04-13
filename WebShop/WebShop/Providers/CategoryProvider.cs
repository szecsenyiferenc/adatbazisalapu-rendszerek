using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Facades;
using WebShop.Factories;
using WebShop.Models.DomainModels;

namespace WebShop.Providers
{
    public class CategoryProvider
    {

        DatabaseFacade db = new DatabaseFacade();
        CustomerFactory factory = new CustomerFactory();

        public List<Category> GetCategories()
        {
            var categoryModels = db.Categories.GetCategories();
            var categories = new List<Category>();

            foreach (var categorytModel in categoryModels)
            {
                categories.Add(factory.CreateCategory(categorytModel));
            }

            return categories;
        }
    }
}
