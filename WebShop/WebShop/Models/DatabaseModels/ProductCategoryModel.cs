using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class ProductCategoryModel
    {
        public ProductCategoryModel(int categoryId, int productId)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }

        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
