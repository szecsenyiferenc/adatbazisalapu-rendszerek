using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class PurchaseModel
    {
        public PurchaseModel(string cusomterId, int productId, StatusModel status)
        {
            CusomterId = cusomterId;
            ProductId = productId;
            Status = status;
        }

        public string CusomterId { get; set; }
        public int ProductId { get; set; }
        public StatusModel Status { get; set; }
        public ProductModel Product { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
