using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelationalAPI.BusinessService;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(NewOrderModel newOrder)
        {
            var resp= await _orderManager.CreateOrder(newOrder);

            return Ok(resp);
        }

        [HttpGet("details/id")]
        public async Task<IActionResult> GetOrderDetails(int Id)
        {
            var resp = await _orderManager.GetOrderDetails(Id);

            return Ok(resp);
        }

        [HttpPost("additem")]
        public async Task<IActionResult> AddOrderItem(OrderItemModel order)
        {
            var resp = await _orderManager.AddOrderItem(order);

            return Ok(resp);
        }

        [HttpGet("deleteitem")]
        public async Task<IActionResult> DeleteOrderItem(OrderItemModel order)
        {
            var resp = await _orderManager.DeleteOrderItem(order);

            return Ok(resp);
        }
    }
}
