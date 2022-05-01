using Microsoft.IdentityModel.Tokens;
using RelationalAPI.AuthService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RelationalAPI.AuthService
{
    public interface IJWTManager
    {
        Task<JWTResponse> GenerateToken(List<Claim> lsClaims);

        // Task<JWTResponse> GenerateTokenOriginal(string user, List<string> roles, List<string> permissions);

        Task<ClaimsPrincipal> ValidateTokenAndGetClaims(string strtoken);
        //  Task<JWTResponse> RefreshToken(string token);

    }
    public class JWTManager : IJWTManager
    {
        private JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        private string _secret = "";

        public JWTManager(JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _secret = _jwtSettings.Secret;
        }

        public async Task<JWTResponse> GenerateToken(List<Claim> lsClaims)
        {
            var resp = new JWTResponse();
            try
            {
                var secCreds = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_secret)), SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken(
                  issuer: "http://vserveq.voltasworld.com",
                  audience: "http://vserveq.voltasworld.com",
                  expires: DateTime.UtcNow.AddDays(1),
                  claims: lsClaims,
                  signingCredentials: secCreds
                  );

                //generate Token
                resp.Token = (new JwtSecurityTokenHandler()).WriteToken(token);
                resp.Status = true;

               

            }
            catch (System.Exception ex)
            {
                throw new Exception("JWT Manager (Token Generation )-" + ex.Message);
            }
            return resp;
        }

     
        public async Task<ClaimsPrincipal> ValidateTokenAndGetClaims(string strtoken)
        {

            try
            {
                var secCreds = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_secret));
                SecurityToken secToken;
                //Validate Token
                var resClaimPrincipal = (new JwtSecurityTokenHandler()).ValidateToken(strtoken, new TokenValidationParameters { IssuerSigningKey = secCreds, ValidateAudience = false, ValidateIssuer = false }, out secToken); //ValidIssuer = "prasad", ValidAudience = "jkd",

                return resClaimPrincipal;
            }
            catch (System.Exception ex)
            {
                throw new Exception("JWT Manager (Validation) -" + ex.Message);
            }
        }
       


        #region Local

        private ClaimsPrincipal GetClaimPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var securityToken);
                if (!IsJwtWithValidSecurityAlgoithem(securityToken))
                    return null;

                return principal;
            }
            catch (Exception ex)
            {
                throw new Exception("Got error while fetch claims from token", ex);
            }
        }

        private bool IsJwtWithValidSecurityAlgoithem(SecurityToken validationToken)
        {
            return (validationToken is JwtSecurityToken jwtSecurityToken) && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
