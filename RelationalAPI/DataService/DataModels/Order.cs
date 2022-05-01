using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.DataService.DataModels
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int? AssignedUserId { get; set; }
        public int? EngineerId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public ICollection<OrderItem> Items { get; set; }
        public ICollection<OrderHistory> History { get; set; }

        public ICollection<OrderAttachment> Attachments { get; set; }

        
    }
}
