using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.AuthService.Models
{
    public class JWTResponse
    {
        public string Token { get; set; }
        public bool Status { get; set; }
    }
}
