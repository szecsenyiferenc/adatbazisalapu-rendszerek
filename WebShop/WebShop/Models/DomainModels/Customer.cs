using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(string email, string firstName, string lastName, decimal balance, 
            string phone, bool isRegularCustomer, string city, string street, int houseNumber, bool? isAdmin,
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
            IsAdmin = isAdmin;
            VisitedProducts = visitedProducts != null ? visitedProducts : new List<VisitedProduct>();
            PurhasedProducts = purhasedProducts != null ? purhasedProducts : new List<PurchasedProduct>();
            Comments = comments != null ? comments : new List<Comment>();
            Likes = likes != null ? likes : new List<Like>();
        }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("isRegularCustomer")]
        public bool IsRegularCustomer { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("houseNumber")]
        public int HouseNumber { get; set; }
        [JsonProperty("isAdmin")]
        public bool? IsAdmin { get; set; }
        [JsonProperty("visitedProducts")]
        public List<VisitedProduct> VisitedProducts { get; set; }
        [JsonProperty("purchasedProducts")]
        public List<PurchasedProduct> PurhasedProducts { get; set; }
        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }
        [JsonProperty("likes")]
        public List<Like> Likes { get; set; }
    }
}
