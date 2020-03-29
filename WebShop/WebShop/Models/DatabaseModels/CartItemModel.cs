using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class CartItemModel
    {
        public CartItemModel()
        {

        }

        public CartItemModel(int cartId, int productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }

        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }
    }
}
