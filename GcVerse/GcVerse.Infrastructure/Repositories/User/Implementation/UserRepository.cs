using GcVerse.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace GcVerse.Infrastructure.Repositories.User.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly string _connectionString;

        public UserRepository(ILogger<UserRepository> logger,
                              IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(BaseUser baseUser)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[base_user]
                                       (base_user_name, base_user_email, base_user_password, user_permission_id) VALUES
                                       (@Name, @Email, @Password, @PermissionId)";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, baseUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int baseUserId, BaseUser baseUser)
        {
            try
            {
                string processQuery = @$"UPDATE [dbo].[base_user] 
                                         SET base_user_name = @Name,
                                             base_user_email = @Email,
                                             base_user_password = @Password,
                                             user_permission_id = @PermissionId
                                         WHERE base_user_id = {baseUserId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, baseUser) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<BaseUser> GetEntityById(int userId)
        {
            try
            {
                string query = @$" SELECT 
                                  base_user_id Id,
								  base_user_name Name,
								  base_user_email Email,
								  user_permission_id PermissionId
                                  FROM [dbo].[base_user]
                                  WHERE base_user_id = {userId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                var result = dbConnection.Query<BaseUser>(query).AsList();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<BaseUser> IsLoginValid(string email, string password)
        {
            try
            {
                string query = @$" SELECT 
                                  base_user_id Id,
								  base_user_name Name,
								  base_user_email Email,
								  user_permission_id PermissionId
                                  FROM [dbo].[base_user]
                                  WHERE base_user_email = '{email}' 
                                  AND base_user_password = '{password}'";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                var result = dbConnection.Query<BaseUser>(query).AsList();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<BaseUser>> GetEntities(int? queryId)
        {
            try
            {
                string query = @$"SELECT 
                                  base_user_id Id,
								  base_user_name Name,
								  base_user_email Email,
								  user_permission_id PermissionId
                                  FROM [dbo].[base_user]
                                  WHERE user_permission_id = {queryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                var result = dbConnection.Query<BaseUser>(query).AsList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.GetEntities)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteEntity(int userId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[base_user] 
                                         WHERE base_user_id = {userId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
