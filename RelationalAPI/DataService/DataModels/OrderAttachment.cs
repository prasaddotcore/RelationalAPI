using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.DataService.DataModels
{
    public class OrderAttachment
    {
        public int Id { get; set; }  
        public string AttachmentName { get; set; }
        public string AttachmentLocation { get; set; }
        public string AttachmentUrl { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
