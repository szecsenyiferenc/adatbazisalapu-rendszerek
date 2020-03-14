using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class VisitModel
    {
        public VisitModel(string cusomterId, int productId, int timesOfVisit)
        {
            CusomterId = cusomterId;
            ProductId = productId;
            TimesOfVisit = timesOfVisit;
        }

        public string CusomterId { get; set; }
        public int ProductId { get; set; }
        public int TimesOfVisit { get; set; }
        public ProductModel Product { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
