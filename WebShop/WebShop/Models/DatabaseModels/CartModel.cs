using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class CartModel
    {
        public CartModel(int id, string userId, DateTime purchaseDate, int statusId)
        {
            Id = id;
            UserId = userId;
            PurchaseDate = purchaseDate;
            StatusId = statusId;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int StatusId { get; set; }
        public StatusModel Status { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}
