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

        public bool AddProductToDatabase(Product product)
        {
            var productModel = factory.CreateProductModel(product);
            return db.Products.AddProductToDatabase(productModel);
        }

        public bool AddCommentToDatabase(Comment comment)
        {
            return db.Products.AddCommentsToDatabase(comment);
        }

        public List<Comment> GetCommentsFromDatabase(int productId)
        {
            var commentModels = db.Products.GetCommentsFromProductWithProperties(productId);
            var comment = new List<Comment>();

            foreach (var commentModel in commentModels)
            {
                comment.Add(factory.CreateComment(commentModel));
            }

            return comment;
        }
    }
}
