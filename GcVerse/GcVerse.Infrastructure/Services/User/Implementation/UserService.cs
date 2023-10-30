using GcVerse.Infrastructure.Repositories;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.User;
using GcVerse.Infrastructure.Services.Security;
using GcVerse.Infrastructure.Repositories.User;

namespace GcVerse.Infrastructure.Services.User.Implementation
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;

        public UserService(ILogger<UserService> logger,
                           IUserRepository userRepository,
                           ISecurityService securityService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _securityService = securityService;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var password = await _securityService.GetHashHmacMD5(loginRequest.Password);

                var user = await _userRepository.IsLoginValid(loginRequest.Email, password);

                if(user == null)
                    return new LoginResponse(false, null);

                var token = await _securityService.GenerateToken(user);

                return new LoginResponse(true, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserService.Login)} - Error: " + ex.Message);
                return new LoginResponse(false, null);
            }
        }

        public async Task<bool> CreateUser(UpsertBaseUserRequest upsertBaseUserRequest, UserPermission userPermission)
        {
            try
            {
                var password = await _securityService.GetHashHmacMD5(upsertBaseUserRequest.Password);
                return await _userRepository.CreateEntity(new BaseUser(upsertBaseUserRequest, password, userPermission)) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserService.CreateUser)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                return await _userRepository.DeleteEntity(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserService.DeleteUser)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<BaseUser>> GetUsersByPermission(UserPermission permission)
        {
            try
            {
                return await _userRepository.GetEntities((int)permission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserService.GetUsersByPermission)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<BaseUser> GetUserById(int userId)
        {
            try
            {
                return await _userRepository.GetEntityById(userId);
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex, $"{nameof(UserService.GetUserById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUser(int categoryId, UpsertBaseUserRequest upsertBaseUserRequest, UserPermission userPermission)
        {
            try
            {
                var password = await _securityService.GetHashHmacMD5(upsertBaseUserRequest.Password);
                return await _userRepository.UpdateEntity(categoryId, new BaseUser(upsertBaseUserRequest, password, userPermission));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserService.UpdateUser)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
