using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.User
{
    public enum UserPermission
    {
        Administrator = 1,
        Basic = 2
    }

    public static class Permission
    {
        public const string Administrator = "Administrator";
        public const string Basic = "Basic";
    }
}
