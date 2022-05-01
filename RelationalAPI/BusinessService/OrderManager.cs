using Microsoft.EntityFrameworkCore;
using RelationalAPI.DataService;
using RelationalAPI.DataService.DataModels;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace RelationalAPI.BusinessService
{
    public interface IOrderManager
    {
        //getproducts
        //getcustomers
        //order.create      
        //order.detailsbyId
        //order.


        //Master
        Task<List<ProductDTO>> GetProducts();
        Task<List<CustomerDTO>> GetCustomers();

        //Order
        Task<ResponseModel> CreateOrder(NewOrderModel objReq);
        Task<OrderDetailsDTOModel> GetOrderDetails(int Id);

        //OrderItems
        Task<ResponseModel> DeleteOrderItem(OrderItemModel objReq);
        Task<ResponseModel> UpdateOrderItem(OrderItemModel objReq);
        Task<ResponseModel> AddOrderItem(OrderItemModel objReq);

        //Attachments
        Task<ResponseModel> AddOrderAttachment(OrderNewAttachmentModel objReq);
        Task<ResponseModel> DeleteOrderAttachment(OrderAttachmentModel objReq);

   
    }
    public class OrderManager:IOrderManager
    {
        private readonly RDBContext _RDBContext;

        public OrderManager(RDBContext rDBContext)
        {
            _RDBContext = rDBContext;
        }


        #region Orders
        public async Task<ResponseModel> CreateOrder(NewOrderModel objReq)
        {
            //Get Order
            var order_id = await _RDBContext.GetID();
           
            var objNew = new Order {
                CustomerId = objReq.CustomerId,
                CustomerName = objReq.CustomerName,
                Status = "Open",
                Remarks=objReq.Remarks
            };
            objNew.Id = order_id;
            objNew.Items = new List<OrderItem>();
            objReq.Items.ForEach(x => {
                objNew.Items.Add(new OrderItem {OrderId= order_id ,ProductId=x.ProductId,ProductName=x.ProductName,Price=x.Price,Quantity=x.Quantity,Amount=x.Price*x.Quantity});
            });

            await _RDBContext.Orders.AddAsync(objNew);
            await _RDBContext.SaveChangesAsync();

            return new ResponseModel{ status=true};  

        }
        public async Task<OrderDetailsDTOModel> GetOrderDetails(int Id)
        {
            var objDetails = new OrderDetailsDTOModel { };
            var objD = await _RDBContext.Orders.Where(x => x.Id == Id).Include(i => i.Items).Include(j => j.History).Include(a => a.Attachments).FirstOrDefaultAsync();
            if (objD != null)
            {
                objDetails.CustomerId = objD.CustomerId;
                objDetails.CustomerName = objD.CustomerName;
                objDetails.Remarks = objD.Remarks;
                objDetails.Id = objD.Id;

                objDetails.Items = objD.Items.Select(x => new OrderItemModel {
                Id=x.Id,
                ProductId=x.ProductId,
                ProductName=x.ProductName,
                Quantity=x.Quantity,
                Price=x.Price,
                Amount=x.Amount,
                OrderId=x.OrderId
                }).ToList();

                if (objD.Attachments != null && objD.Attachments.Count > 0)
                    objDetails.Attachments = objD.Attachments.Select(x => new OrderAttachmentModel {
                    Id=x.Id,
                    OrderId=x.OrderId,
                    AttachmentName=x.AttachmentName,
                    AttachmentUrl=x.AttachmentUrl
                    }).ToList();


            }

            return objDetails;
        }
        #endregion

        #region Order Items
        public async Task<ResponseModel> DeleteOrderItem(OrderItemModel objReq)
        {
            var obj = _RDBContext.OrderItems.Where(x => x.OrderId == objReq.OrderId && x.Id == objReq.Id).FirstOrDefault();
            if (obj != null)
            {
                _RDBContext.OrderItems.Remove(obj);
               await _RDBContext.SaveChangesAsync();
                return new ResponseModel { status = true };
            }
            else
                return new ResponseModel { status=false, message="Item not found" };
        }
        public async Task<ResponseModel> AddOrderItem(OrderItemModel objReq)
        {
            var obj = new OrderItem { OrderId = objReq.OrderId, ProductId = objReq.ProductId, ProductName = objReq.ProductName, Price = objReq.Price, Quantity = objReq.Quantity, Amount = objReq.Price * objReq.Quantity };

            await _RDBContext.OrderItems.AddAsync(obj);
            await _RDBContext.SaveChangesAsync();

            return new ResponseModel { status = true };

        }

        public async Task<ResponseModel> UpdateOrderItem(OrderItemModel objReq)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Order Attachments
        public async Task<ResponseModel> DeleteOrderAttachment(OrderAttachmentModel objReq)
        {
            throw new NotImplementedException();
        }


        public async Task<ResponseModel> AddOrderAttachment(OrderNewAttachmentModel objReq)
        {
            throw new NotImplementedException();
        }
        #endregion


      

       
     

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            return  await _RDBContext.Customers.Select(x => new CustomerDTO { Id=x.Id,Name=x.Name,Mobile=x.Mobile,Email=x.Email,Address=x.Address }).ToListAsync() ;

        }

     

        public async Task<List<ProductDTO>> GetProducts()
        {
            return await _RDBContext.Products.Select(x => new ProductDTO { Id = x.Id, Name = x.Name, Model = x.Model, Status = x.Status, Price = x.Price }).ToListAsync();
        }

      
    }
}
