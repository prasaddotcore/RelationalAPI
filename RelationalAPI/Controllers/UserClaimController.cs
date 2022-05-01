using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelationalAPI.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelationalAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace RelationalAPI.Controllers
{
    [Route("api/userclaim")]
    [ApiController]
    [Authorize]
    public class UserClaimController : ControllerBase
    {
        private readonly IUserClaimManager _userClaimsManager;

        public UserClaimController(IUserClaimManager userClaimsManager)
        {
            _userClaimsManager = userClaimsManager;
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetAllClaims()
        {
            var resp = await _userClaimsManager.GetAllClaims();

            return Ok(resp);
        }

        [HttpGet("getlist/Id")]
        public async Task<IActionResult> GetUserClaimsById(int Id)
        {
            var resp = await _userClaimsManager.GetUserClaimsById(Id);

            return Ok(resp);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> CreateUser(UserClaimDTOListModel claimList)
        {
            var resp = await _userClaimsManager.AssignClaims(claimList);

            return Ok(resp);
        }

        [HttpPost("unassign")]
        public async Task<IActionResult> DeleteUser(UserClaimDTO claimDTO)
        {
            var resp = await _userClaimsManager.UnAssignClaims(claimDTO);

            return Ok(resp);
        }
    }
}
