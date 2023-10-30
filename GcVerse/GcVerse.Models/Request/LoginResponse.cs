using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Models.Request
{
    public class LoginResponse
    {
        public LoginResponse(bool success, string token)
        {
            Success = success;
            Token = token;
        }

        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
