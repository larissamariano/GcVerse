using Dapper;
using GcVerse.Models.Category;
using GcVerse.Models.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;

namespace GcVerse.Infrastructure.Repositories.Category.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly string _connectionString;

        public CategoryRepository(ILogger<CategoryRepository> logger,
                                  IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<bool> CreateCategory(BaseCategory baseCategory)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[category]
                                       (title, description, image_id) VALUES
                                       (@Title, @Description,{baseCategory.Image.Id})";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, baseCategory) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryRepository.CreateCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateCategory(int categoryId, BaseCategory baseCategory)
        {
            try
            {
                string processQuery = @$"UPDATE [dbo].[category] 
                                         SET title = @Title,
                                             description = @Description,
                                             image_id = {baseCategory.Image.Id}
                                         WHERE category_id = {categoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, baseCategory) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryRepository.UpdateCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<BaseCategory> GetCategoryById(int categoryId)
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
                _logger.LogError(ex, $"{nameof(CategoryRepository.UpdateCategory)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<BaseCategory>> GetAllCategories()
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
                _logger.LogError(ex, $"{nameof(CategoryRepository.GetAllCategories)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteCategoryById(int categoryId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[category] 
                                         WHERE category_id = {categoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryRepository.DeleteCategoryById)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
