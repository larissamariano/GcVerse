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
    public class CategoryRepository : IBaseRepository<BaseCategory>
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly string _connectionString;

        public CategoryRepository(ILogger<CategoryRepository> logger,
                                  IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(BaseCategory baseCategory)
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
                _logger.LogError(ex, $"{nameof(CategoryRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int categoryId, BaseCategory baseCategory)
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
                _logger.LogError(ex, $"{nameof(CategoryRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<BaseCategory> GetEntityById(int categoryId)
        {
            try
            {             
                string query = @$"SELECT 
                                      cat.category_id as Id,
								      cat.title as Title,
								      cat.description as Description,
								      img.image_from_url as FromUrl,
								      img.image_id as Id,
								      img.image_description as Description,
								      img.image_related_id as RelatedId,
								      img.image_url as Url
                                  FROM [dbo].[category] as cat
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cat.image_id
                                  WHERE category_id = {categoryId} ";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseCategory, BaseImage, BaseCategory>(query, (category, baseImage) =>
                {
                    category.Image = baseImage;
                    return category;
                }, splitOn: "FromUrl").AsList();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<BaseCategory>> GetEntities(int? queryId)
        {
            try
            {
                
                string query = @$"SELECT 
                                      cat.category_id as Id,
								      cat.title as Title,
								      cat.description as Description,
								      img.image_from_url as FromUrl,
								      img.image_id as Id,
								      img.image_description as Description,
								      img.image_related_id as RelatedId,
								      img.image_url as Url
                                  FROM [dbo].[category] as cat
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cat.image_id";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseCategory, BaseImage, BaseCategory>(query, (category, baseImage) =>
                {
                    category.Image = baseImage;
                    return category;
                }, splitOn: "FromUrl").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryRepository.GetEntities)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteEntity(int categoryId)
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
                _logger.LogError(ex, $"{nameof(CategoryRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
