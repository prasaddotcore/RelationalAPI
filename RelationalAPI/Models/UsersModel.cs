using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Models
{

    public class UserListModel
    {
        public List<UserDTO> users { get; set; }
    }
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
    }

    public class NewUserModel
    {
        
        public string Name { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
       
    }

    public class EditUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
