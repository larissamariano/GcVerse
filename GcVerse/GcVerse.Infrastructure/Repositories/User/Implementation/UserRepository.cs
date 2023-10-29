using GcVerse.Infrastructure.Repositories.Category.Implementation;
using GcVerse.Models.Category;
using GcVerse.Models.Shared;
using GcVerse.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GcVerse.Infrastructure.Repositories.User.Implementation
{
    public class UserRepository : IBaseRepository<BaseUser>
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
                string processQuery = @$"INSERT INTO [dbo].[category]
                                       (title, description, image_id) VALUES
                                       (@Title, @Description,{baseCategory.Image.Id})";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, baseCategory);
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
                string processQuery = @$"UPDATE [dbo].[category] 
                                         SET title = @Title,
                                             description = @Description,
                                             image_id = {baseCategory.Image.Id}
                                         WHERE category_id = {baseUserId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, baseCategory) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<BaseUser> GetEntityById(int categoryId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[category] as cat
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cat.image_id
                                  WHERE category_id = {categoryId} ";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseCategory, BaseImage, BaseCategory>(query, (category, baseImage) =>
                {
                    category.Image = baseImage;
                    return category;
                }, splitOn: "image_id").AsList();

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
                                  *
                                  FROM [dbo].[category] as cat
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cat.image_id";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseCategory, BaseImage, BaseCategory>(query, (category, baseImage) =>
                {
                    category.Image = baseImage;
                    return category;
                }, splitOn: "image_id").AsList();

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
                string processQuery = @$"DELETE FROM [dbo].[category] 
                                         WHERE category_id = {userId}";

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
