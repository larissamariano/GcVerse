using GcVerse.Models.Category;
using GcVerse.Models.Request;
using GcVerse.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Services.User
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<bool> CreateUser(UpsertBaseUserRequest upsertBaseUserRequest, UserPermission userPermission);
        Task<bool> UpdateUser(int userId, UpsertBaseUserRequest upsertBaseUserRequest, UserPermission userPermission);
        Task<bool> DeleteUser(int userId);
        Task<BaseUser> GetUserById(int userId);
        Task<List<BaseUser>> GetUsersByPermission(UserPermission userPermission);
    }
}
