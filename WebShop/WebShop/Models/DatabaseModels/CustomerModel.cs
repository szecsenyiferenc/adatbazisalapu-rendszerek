using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class CustomerModel
    {
        public CustomerModel(string email, string password, string firstName, string lastName, decimal balance, string phone, bool isRegularCustomer, string city, string street, int houseNumber)
        {
            Email = email;
            Pass = password;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
            Phone = phone;
            IsRegularCustomer = isRegularCustomer;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
        }

        public string Email { get; set; }
        public string Pass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        public bool IsRegularCustomer { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public List<VisitModel> VisitedProducts { get; set; }
        public List<PurchaseModel> PurhasedProducts { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<LikeModel> Like { get; set; }
    }
}
