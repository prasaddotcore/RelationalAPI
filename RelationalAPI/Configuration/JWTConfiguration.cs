using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RelationalAPI.AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Configuration
{
    public static class JWTConfiguration
    {
        public static void AddJWTConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            var objJwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), objJwtSettings);
            service.AddSingleton(objJwtSettings);

            var tokenValidationParams = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(objJwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false

            };

            service.AddSingleton(tokenValidationParams);

            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = tokenValidationParams;
            });

        }
    }
}
