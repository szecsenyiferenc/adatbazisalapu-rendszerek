using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class VisitModel
    {
        public int CusomterId { get; set; }
        public int ProductId { get; set; }
        public int TimesOfVisit { get; set; }
    }
}
