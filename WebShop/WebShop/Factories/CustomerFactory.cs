﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DatabaseModels;
using WebShop.Models.DomainModels;

namespace WebShop.Factories
{
    public class CustomerFactory
    {
        public Customer CreateCustomer(CustomerModel customerModel)
        {
            return new Customer(customerModel.Email, customerModel.FirstName, customerModel.LastName,
                customerModel.Balance, customerModel.Phone, customerModel.IsRegularCustomer,
                customerModel.City, customerModel.Street, customerModel.HouseNumber, customerModel.IsAdmin);
        }

        public CustomerModel CreateCustomerModel(RegistrationCustomer customerModel)
        {
            return new CustomerModel(customerModel.Email, customerModel.Password ,customerModel.FirstName, customerModel.LastName,
                customerModel.Balance, customerModel.Phone, customerModel.IsRegularCustomer,
                customerModel.City, customerModel.Street, customerModel.HouseNumber, customerModel.IsAdmin);
        }

        public Product CreateProduct(ProductModel productModel)
        {
            if(productModel == null)
            {
                return null;
            }
            var product = new Product(productModel.Id, productModel.Name, productModel.Price, productModel.Image);

            if(productModel.Likes != null && productModel.Likes.Any())
            {
                int likeValue = 0;
                foreach (var like in productModel.Likes)
                {
                    likeValue += like.IsLiked.HasValue ? (like.IsLiked.Value == true ? 1 : -1) : 0;
                }
                product.Likes = likeValue;
            }


            return product;
        }

        public ProductModel CreateProductModel(Product product)
        {
            return new ProductModel(product.Id, product.Name, product.Price, product.Image);
        }

        public VisitedProduct CreateVisitedProducts(ProductModel productModel, int timesOfVisit)
        {
            return new VisitedProduct(productModel.Id, productModel.Name, productModel.Price, productModel.Image, timesOfVisit);
        }

        public PurchasedProduct CreateProduct(ProductModel productModel, string category)
        {
            return new PurchasedProduct(productModel.Id, productModel.Name, productModel.Price, productModel.Image,category);
        }

        public Comment CreateComment(CommentModel commentModel)
        {
            return new Comment(CreateCustomer(commentModel.Customer), CreateProduct(commentModel.Product),commentModel.Time, commentModel.Text);
        }

        public Like CreateComment(LikeModel likeModel)
        {
            return new Like(likeModel.CustomerId, likeModel.ProductId, likeModel.IsLiked);
        }

        public Cart CreateCart(CartModel cartModel)
        {
            var cartItems = new List<CartItem>();

            foreach (var item in cartModel.CartItems)
            {
                cartItems.Add(CreateCart(item));
            }

            return new Cart(null, cartItems);
        }

        public CartItem CreateCart(CartItemModel cartItemModel)
        {
            return new CartItem(CreateProduct(cartItemModel.Product), cartItemModel.Quantity);
        }

        public Category CreateCategory(CategoryModel categoryModel)
        {
            return new Category(categoryModel.Id, categoryModel.Name);
        }

        public Product CreateProductWithCategory(ProductModel productModel)
        {
            List<Category> categoriesList = new List<Category>();
            foreach (CategoryModel item in productModel.Categories)
            {
                categoriesList.Add(CreateCategory(item));
            }
            var product = CreateProduct(productModel);
            product.Categories = categoriesList;
            return product;
        }

        public VisitedProduct CreateProductWithVisits(ProductModel productModel)
        {
            int value = productModel.VisitedProducts.Count != 0 ? productModel.VisitedProducts[0].TimesOfVisit : 0;
            var product = CreateVisitedProducts(productModel, value);
            return product;
        }
    }
}
