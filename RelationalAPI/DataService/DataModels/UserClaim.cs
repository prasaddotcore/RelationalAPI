using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.DataService.DataModels
{
    public class UserClaim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
