using GcVerse.Models.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.User
{
    public class BaseUser
    {
        public BaseUser()
        {

        }

        public BaseUser(UpsertBaseUserRequest upsertBaseUserRequest, string password, UserPermission userPermission)
        {
            Name = upsertBaseUserRequest.Name;
            Email = upsertBaseUserRequest.Email;
            Password = password;
            Permission = userPermission;
            PermissionId = (int)userPermission;
        }

        [Description("base_user_id")]
        public int Id { get; set; }

        [Description("base_user_name")]
        public string Name { get; set; }

        [Description("base_user_email")]
        public string Email { get; set; }

        [Description("base_user_password")]
        public string Password { get; set; }

        public UserPermission Permission { get; set; }

        [Description("user_permission_id")]
        public int PermissionId { get; set; }
    }
}