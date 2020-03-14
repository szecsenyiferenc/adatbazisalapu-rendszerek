using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class CommentModel
    {
        public CommentModel(int customerId, int productId, DateTime time, string text)
        {
            CustomerId = customerId;
            ProductId = productId;
            Time = time;
            Text = text;
        }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
