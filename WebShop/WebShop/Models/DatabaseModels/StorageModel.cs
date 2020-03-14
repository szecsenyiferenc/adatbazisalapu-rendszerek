using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.DatabaseModels
{
    public class StorageModel
    {
        public StorageModel(int id, string location)
        {
            Id = id;
            Location = location;
        }

        public int Id { get; set; }
        public string Location { get; set; }
        public List<StoragedProductModel> StoragedProducts { get; set; }

    }
}
