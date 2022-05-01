using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelationalAPI.BusinessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet("testId")]
        public IActionResult GetID()
        {
           return Ok( );
        }
    }
}
