using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Models
{

    public class UserClaimDTOListModel
    {
        public List<UserClaimDTO> userclaims { get; set; }
    }
    public class UserClaimDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ClaimId { get; set; }
        public string ClaimType { get; set; }
    }

    public class ClaimDTOListModel
    {
        public List<ClaimDTO> claims { get; set; }
    }
    public class ClaimDTO
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
