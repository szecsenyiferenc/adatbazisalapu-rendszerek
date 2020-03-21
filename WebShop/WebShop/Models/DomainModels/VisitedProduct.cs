using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class VisitedProduct : Product
    {
        public VisitedProduct(int id, string name, double price, byte[] image, int timesOfVisit)
            : base(id, name, price, image)
        {
            TimesOfVisit = timesOfVisit;
        }

        public int TimesOfVisit { get; set; }
    }
}
