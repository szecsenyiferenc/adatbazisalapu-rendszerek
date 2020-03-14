using System;
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
                customerModel.City, customerModel.Street, customerModel.HouseNumber);
        }

        public Product CreateProduct(ProductModel productModel)
        {
            return new Product(productModel.Id, productModel.Name, productModel.Price);
        }

        public VisitedProduct CreateVisitedProducts(ProductModel productModel, int timesOfVisit)
        {
            return new VisitedProduct(productModel.Id, productModel.Name, productModel.Price, timesOfVisit);
        }

        public PurchasedProduct CreateProduct(ProductModel productModel, string category)
        {
            return new PurchasedProduct(productModel.Id, productModel.Name, productModel.Price, category);
        }

        public Comment CreateComment(CommentModel commentModel)
        {
            return new Comment(commentModel.Time, commentModel.Text);
        }

    }
}
