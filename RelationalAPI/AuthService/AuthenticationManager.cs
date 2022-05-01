using Microsoft.EntityFrameworkCore;
using RelationalAPI.AuthService.Models;
using RelationalAPI.DataService;

using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RelationalAPI.AuthService
{
    public interface IAuthenticationManager
    {
        Task<LoginResponseModel> UserAuthentication(LoginRequestModel objReq);
    }
    public class AuthenticationManager: IAuthenticationManager
    {
        private readonly IJWTManager _jwtManager;
        private readonly RDBContext _RDBContext;

        public AuthenticationManager(IJWTManager jwtManager, RDBContext context)
        {
            _jwtManager = jwtManager;
            _RDBContext = context;
        }

        public async Task<LoginResponseModel> UserAuthentication(LoginRequestModel objReq)
        {

           var userDetails= await CheckAuthenticate(objReq);
           
            if (userDetails != null)
            {
                var token = await GenerateAuthorizeToken(userDetails);
                return new LoginResponseModel() { status = true, token = token };
            }         

            return new LoginResponseModel() { status=false,message="Invalid User"};
        }

        private async Task<UserModel> CheckAuthenticate(LoginRequestModel objReq)
        {
            var userObj = new UserModel();

            var objUser=  _RDBContext.Users.Where(x => x.Email == objReq.user && x.Password == objReq.password).Include(x => x.UserClaims).ThenInclude(clm=>clm.Claim).Include(y => y.UserRoles).ThenInclude(rl => rl.Role).FirstOrDefault();
            if (objUser != null)
            {
                userObj.Id = objUser.Id;
                userObj.UserName = objUser.Name;
                userObj.Email = objUser.Email;
                userObj.RoleId = objUser.UserRoles.FirstOrDefault().Role.Id;
                userObj.Role = objUser.UserRoles.FirstOrDefault().Role.Name;
                userObj.Permissions = new List<string>();
                objUser.UserClaims.ToList().ForEach(x => {
                    userObj.Permissions.Add(x.Claim.ClaimValue);
                });

                return userObj;
            }

            return null;
        }

        private async Task<string> GenerateAuthorizeToken(UserModel userDetails)
        {
            List<Claim> lsClaims = new List<Claim>() { new Claim("user", userDetails.Email), new Claim(ClaimTypes.Name, userDetails.UserName), new Claim(ClaimTypes.Role, userDetails.Role) };
            //if (objDefaultUIRole != null)
            //{
            //    lsClaims.Add(new Claim("DefalutView", objDefaultUIRole.PermissionName));
            //}

            userDetails.Permissions.ForEach(x =>
            {
                lsClaims.Add(new Claim("permission", x));
            });

            // UserIdentity = new ClaimsPrincipal(new ClaimsIdentity(lsClaims))
            var jwtResp = await _jwtManager.GenerateToken(lsClaims);

            if (jwtResp.Status)
            {
                // var jwt=await _jwtManager.ValidateTokenAndGetClaims(jwtResp.Token);

                // return new LoginResponseModel { status = true, token = jwtResp.Token };

                return jwtResp.Token;
            }
            return "";
        }
    }
}
