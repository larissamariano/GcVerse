using GcVerse.Models.Content;
using GcVerse.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Services.Security
{
    public interface ISecurityService
    {
        Task<string> GetHashHmacMD5(string message);
        Task<string> GenerateToken(BaseUser user);

    }
}
