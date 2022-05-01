using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.DataService.DataModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public bool Status { get; set; }

        public decimal Price { get; set; }

    }
}
