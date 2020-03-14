using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class StoragedProductModel
    {
        public StoragedProductModel(int storageId, int productId, int quantity)
        {
            StorageId = storageId;
            ProductId = productId;
            Quantity = quantity;
        }

        public int StorageId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
