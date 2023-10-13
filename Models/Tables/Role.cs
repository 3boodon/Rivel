using System;

namespace Rivel.Models
{
    public class Role : Timestamps
    {
        public Role(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
        public Role(string roleName)
        {
            RoleName = roleName;
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

}
