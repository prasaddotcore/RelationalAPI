using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.DataService.DataModels
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }

        public Order order { get; set; }
    }
}
