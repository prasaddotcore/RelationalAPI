using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Models
{
    public class LoginRequestModel
    {
        public string user { get; set; }
        public string password { get; set; }
    }

    public class LoginResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string token { get; set; }      
    }
}
