using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Models
{

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
    }
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class NewOrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string  CustomerName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int? AssignedUserId { get; set; }
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public string AssignedToName { get; set; }
        public List<OrderItemModel> Items { get; set; }        
    }
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }

    public class OrderAttachmentModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string AttachmentName { get; set; }       
        public string AttachmentUrl { get; set; }
    }

    public class OrderNewAttachmentModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentBase64 { get; set; }
        public string AttachmentUrl { get; set; }
    }

    public class OrderDetailsDTOModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int? AssignedUserId { get; set; }
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public string AssignedToName { get; set; }

        public List<OrderItemModel> Items { get; set; }
        public List<OrderAttachmentModel> Attachments { get; set; }
    }
}
