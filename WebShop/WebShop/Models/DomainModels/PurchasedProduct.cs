﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class PurchasedProduct : Product
    {
        public PurchasedProduct(int id, string name, double price, string status)
            : base(id, name, price)
        {
            Status = status;
        }

        public string Status { get; set; }
    }
}