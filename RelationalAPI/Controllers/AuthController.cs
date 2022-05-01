using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelationalAPI.AuthService;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        

        public AuthController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequestModel objReq)
        {
           var resp= await _authenticationManager.UserAuthentication(objReq);

            return Ok(resp);
        }

      
    }
}
