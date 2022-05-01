using Microsoft.EntityFrameworkCore;
using RelationalAPI.DataService;
using RelationalAPI.DataService.DataModels;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.AuthService
{
    public interface IUserClaimManager
    {
        //getallclaims
        //userclaim.getallbyuserId
        //userclaim.assing
        //userclaim.unassing/delete

        Task<ClaimDTOListModel> GetAllClaims();
        Task<UserClaimDTOListModel> GetUserClaimsById(int Id);
        Task<ResponseModel> AssignClaims(UserClaimDTOListModel lsClaims);
        Task<ResponseModel> UnAssignClaims(UserClaimDTO lsClaims);
    }
    public class UserClaimManager:IUserClaimManager
    {
        private readonly RDBContext _RDBContext;

        public UserClaimManager(RDBContext rDBContext)
        {
            _RDBContext = rDBContext;
        }

        public async Task<ResponseModel> AssignClaims(UserClaimDTOListModel lsClaims)
        {
            await  _RDBContext.UserClaims.AddRangeAsync(lsClaims.userclaims.Select(x => new UserClaim {ClaimId=x.ClaimId,UserId=x.UserId }));
            await _RDBContext.SaveChangesAsync();

            return new ResponseModel { status=true};
        }

        public async Task<ClaimDTOListModel> GetAllClaims()
        {
            return new ClaimDTOListModel { claims = await _RDBContext.Claims.Select(x => new ClaimDTO { Id = x.Id, ClaimType = x.ClaimType, ClaimValue = x.ClaimValue}).ToListAsync() };

        }

        public async Task<UserClaimDTOListModel> GetUserClaimsById(int Id)
        {
            return new UserClaimDTOListModel { userclaims = await _RDBContext.UserClaims.Where(y=>y.UserId==Id).Include(u=>u.User).Include(z=>z.Claim).Select(x => new UserClaimDTO { Id = x.Id, ClaimType = x.Claim.ClaimType, ClaimId = x.Claim.Id,UserId=x.User.Id,UserName=x.User.Name }).ToListAsync() };

        }

        public async Task<ResponseModel> UnAssignClaims(UserClaimDTO objClaims)
        {
            var obj = await _RDBContext.UserClaims.Where(x => x.Id == objClaims.Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                _RDBContext.UserClaims.Remove(obj);
                await _RDBContext.SaveChangesAsync();
                return new ResponseModel { status = true };
            }
            else
                return new ResponseModel { status = false, message = "user not found" };
        }
    }
}
