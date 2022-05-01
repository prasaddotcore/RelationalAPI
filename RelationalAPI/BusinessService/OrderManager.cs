using Microsoft.EntityFrameworkCore;
using RelationalAPI.DataService;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.BusinessService
{
    public interface IOrderManager
    {
        //getproducts
        //getcustomers
        //order.create
        //order.editById
        //order.detailsbyId
        //order.


        Task<List<ProductDTO>> GetProducts();
        Task<List<CustomerDTO>> GetCustomers();

    }
    public class OrderManager:IOrderManager
    {
        private readonly RDBContext _RDBContext;

        public OrderManager(RDBContext rDBContext)
        {
            _RDBContext = rDBContext;
        }

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
