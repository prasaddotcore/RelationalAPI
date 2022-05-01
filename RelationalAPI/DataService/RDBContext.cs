using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RelationalAPI.DataService.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.DataService
{
    public class RDBContext: DbContext
    {
        public RDBContext([NotNullAttribute]DbContextOptions options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }

        public DbSet<OrderAttachment> OrderAttachments { get; set; }



        public async Task<int> GetID()
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };

            var result = this.Database
                .ExecuteSqlRaw("EXEC @returnValue = [dbo].[GenerateID]", parameterReturn);

            int returnValue = (int)parameterReturn.Value;

            return returnValue;
        }
    }
}
