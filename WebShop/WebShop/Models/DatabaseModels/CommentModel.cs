using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class CommentModel
    {
        public CommentModel(string customerId, int productId, DateTime time, string text)
        {
            CustomerId = customerId;
            ProductId = productId;
            Time = time;
            Text = text;
        }

        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public ProductModel Product { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
