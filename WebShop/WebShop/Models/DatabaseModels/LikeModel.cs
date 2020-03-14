using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class LikeModel
    {
        public LikeModel(int customerId, int productId, bool? isLiked)
        {
            CustomerId = customerId;
            ProductId = productId;
            this.isLiked = isLiked;
        }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public bool? isLiked { get; set; }
    }
}
