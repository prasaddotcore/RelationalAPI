using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RelationalAPI.AuthService;
using RelationalAPI.BusinessService;
using RelationalAPI.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Configuration
{
    public static class AppConfiguration
    {
        public static void AddSqlServerConfig(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<RDBContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerDefaultConnection"));
            });

           
        }
        public static void AddIdentityManager(this IServiceCollection service)
        {
            service.AddScoped<IJWTManager, JWTManager>();

            service.AddScoped<IAuthenticationManager, AuthenticationManager>();


            service.AddScoped<IUserManager, UserManager>();
            service.AddScoped<IUserClaimManager, UserClaimManager>();

            service.AddScoped<IOrderManager, OrderManager>();
        }
    }
}
