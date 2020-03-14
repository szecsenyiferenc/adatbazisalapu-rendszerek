using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class PurchaseModel
    {
        public PurchaseModel(int cusomterId, int productId, StatusModel status)
        {
            CusomterId = cusomterId;
            ProductId = productId;
            Status = status;
        }

        public int CusomterId { get; set; }
        public int ProductId { get; set; }
        public StatusModel Status { get; set; }
    }
}
