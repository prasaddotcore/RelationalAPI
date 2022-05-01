using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Models
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleDTOListModel
    {
        public List<RoleDTO> roles { get; set; }
    }

}
