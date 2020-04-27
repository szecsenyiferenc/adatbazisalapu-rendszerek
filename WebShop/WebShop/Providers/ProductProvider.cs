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
            db.Products.AddCategoriesToProducts(productModels);
            var products = new List<Product>();

            foreach (var productModel in productModels)
            {
                products.Add(factory.CreateProductWithCategory(productModel));
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

        public bool AddLikeToDatabase(Like like)
        {
            return db.Products.AddLikeToDatabase(like);
        }

        public List<Comment> GetCommentsFromDatabase(int productId)
        {
            var commentModels = db.Products.GetCommentsFromProductWithProperties(productId);
            var comments = new List<Comment>();

            foreach (var commentModel in commentModels)
            {
                comments.Add(factory.CreateComment(commentModel));
            }

            return comments;
        }

        public List<Like> GetLikesFromDatabase(string userId)
        {
            var likeModels = db.Products.GetLikeModelsFromProduct(userId);
            var likes = new List<Like>();

            foreach (var likeModel in likeModels)
            {
                likes.Add(factory.CreateComment(likeModel));
            }

            return likes;
        }
    }
}
