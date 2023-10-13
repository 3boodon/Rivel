using System;
using Rivel.Services;

namespace Rivel.Models
{
    public class User : Timestamps
    {

        public User(string username, string password, int roleId)
        {
            Username = username;
            Password = password;
            RoleId = roleId;
        }
        public User(int userId, string username, string password, int roleId)
        {
            UserId = userId;
            Username = username;
            Password = password;
            RoleId = roleId;
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
