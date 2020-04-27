using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class ProductModel
    {
        public ProductModel(int id, string name, double price, byte[] image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public List<VisitModel> VisitedProducts { get; set; }
        public List<PurchaseModel> PurhasedProducts { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<LikeModel> Likes { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
