using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class VisitedProduct : Product
    {
        public VisitedProduct(int id, string name, double price, int timesOfVisit)
            : base(id, name, price)
        {
            TimesOfVisit = timesOfVisit;
        }

        public int TimesOfVisit { get; set; }
    }
}
