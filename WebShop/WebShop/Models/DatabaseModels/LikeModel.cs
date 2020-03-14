using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class LikeModel
    {
        public LikeModel(string customerId, int productId, bool? isLiked)
        {
            CustomerId = customerId;
            ProductId = productId;
            IsLiked = isLiked;
        }

        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public bool? IsLiked { get; set; }
        public ProductModel Product { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
