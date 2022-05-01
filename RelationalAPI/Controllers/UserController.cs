using Microsoft.AspNetCore.Authorization;
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
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(NewUserModel newUser)
        {
            var resp = await _userManager.CrateUser(newUser);

            return Ok(resp);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(EditUserModel objUser)
        {
            var resp = await _userManager.UpdateUser(objUser);

            return Ok(resp);
        }

        [HttpGet("delete/Id")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var resp = await _userManager.DeleteUserById(Id);

            return Ok(resp);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(int Id)
        {
            var resp = await _userManager.GetRoles(Id);

            return Ok(resp);
        }

        [HttpGet("details/Id")]
        public async Task<IActionResult> GetUserBy(int Id)
        {
            var resp = await _userManager.GetUserById(Id);

            return Ok(resp);
        }

        [HttpGet("details/email")]
        public async Task<IActionResult> GetUserEmail(string email)
        {
            var resp = await _userManager.GetUserByEmail(email);

            return Ok(resp);
        }

    }
}
