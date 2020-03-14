using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Customer
    {
        public Customer(string email, string firstName, string lastName, decimal balance, 
            string phone, bool isRegularCustomer, string city, string street, int houseNumber, 
            List<VisitedProduct> visitedProducts = null, 
            List<PurchasedProduct> purhasedProducts = null, 
            List<Comment> comments = null, 
            List<Like> likes = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
            Phone = phone;
            IsRegularCustomer = isRegularCustomer;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            VisitedProducts = visitedProducts != null ? visitedProducts : new List<VisitedProduct>();
            PurhasedProducts = purhasedProducts != null ? purhasedProducts : new List<PurchasedProduct>();
            Comments = comments != null ? comments : new List<Comment>();
            Likes = likes != null ? likes : new List<Like>();
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        public bool IsRegularCustomer { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public List<VisitedProduct> VisitedProducts { get; set; }
        public List<PurchasedProduct> PurhasedProducts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
