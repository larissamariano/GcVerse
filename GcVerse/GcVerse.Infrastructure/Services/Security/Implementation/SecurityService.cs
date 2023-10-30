using GcVerse.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GcVerse.Infrastructure.Services.Security.Implementation
{
    public class SecurityService : ISecurityService
    {
        private readonly ILogger<SecurityService> _logger;
        private readonly string _hashSecret;
        private readonly string _authSecret;

        public SecurityService(ILogger<SecurityService> logger,
                               IConfiguration configuration)
        {
            _logger = logger;
            _hashSecret = configuration.GetValue<string>("HashSecret");
            _authSecret = configuration.GetValue<string>("AuthSecret");
        }

        public async Task<string> GetHashHmacMD5(string message)
        {
            Encoding encoding = Encoding.UTF8;
            using (HMACMD5 hmac = new HMACMD5(encoding.GetBytes(_hashSecret)))
            {
                var msg = encoding.GetBytes(message);
                var hash = hmac.ComputeHash(msg);
                return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }

        public async Task<string> GenerateToken(BaseUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSecret);
            var role = Enum.GetName(typeof(UserPermission), user.PermissionId);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
