using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Facades;
using WebShop.Factories;
using WebShop.Models.DomainModels;

namespace WebShop.Providers
{
    public class ProductProvider
    {
        DatabaseFacade db = new DatabaseFacade();
        CustomerFactory factory = new CustomerFactory();


        public List<Product> GetProducts()
        {
            var productModels = db.Products.GetProductsWithProperties();
            var products = new List<Product>();

            foreach (var productModel in productModels)
            {
                products.Add(factory.CreateProduct(productModel));
            }

            return products;
        }
    }
}
