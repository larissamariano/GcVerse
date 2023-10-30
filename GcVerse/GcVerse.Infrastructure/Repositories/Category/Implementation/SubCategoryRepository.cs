using Dapper;
using GcVerse.Infrastructure.Repositories.Category;
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
    public class SubCategoryRepository : IBaseRepository<SubCategory>
    {
        private readonly ILogger<SubCategoryRepository> _logger;
        private readonly string _connectionString;

        public SubCategoryRepository(ILogger<SubCategoryRepository> logger,
                                  IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(SubCategory subCategory)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[sub_category]
                                       (title,  description,  image_id, category_id, related_info) VALUES
                                       (@Title, @Description,{subCategory.Image.Id},@CategoryId,@Related)";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, subCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int subCategoryId, SubCategory subCategory)
        {
            try
            {
                string processQuery = @$"UPDATE [dbo].[sub_category] 
                                         SET title = @Title,
                                             description = @Description,
                                             image_id = {subCategory.Image.Id},
                                             category_id = @CategoryId,
                                             related_info = @Related
                                         WHERE sub_category_id = {subCategoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, subCategory) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<SubCategory> GetEntityById(int subCategoryId)
        {
            try
            {             
                string query = @$"SELECT 
                                      sub.sub_category_id as Id,
									  sub.category_id as CategoryId,
									  sub.related_info as Related,
								      sub.title as Title,
								      sub.description as Description,
								      img.image_from_url as FromUrl,
								      img.image_id as Id,
								      img.image_description as Description,
								      img.image_related_id as RelatedId,
								      img.image_url as Url
                                  FROM [dbo].[sub_category] as sub
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = sub.image_id
                                  WHERE sub_category_id = {subCategoryId} ";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<SubCategory, BaseImage, SubCategory>(query, (SubCategory, baseImage) =>
                {
                    SubCategory.Image = baseImage;
                    return SubCategory;
                }, splitOn: "FromUrl").AsList();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<SubCategory>> GetEntities(int? categoryId)
        {
            try
            {
                string query = @$"SELECT 
                                      sub.sub_category_id as Id,
									  sub.category_id as CategoryId,
									  sub.related_info as Related,
								      sub.title as Title,
								      sub.description as Description,
								      img.image_from_url as FromUrl,
								      img.image_id as Id,
								      img.image_description as Description,
								      img.image_related_id as RelatedId,
								      img.image_url as Url
                                  FROM [dbo].[sub_category] as sub
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = sub.image_id
                                  WHERE category_id = {categoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = await dbConnection.QueryAsync<SubCategory, BaseImage, SubCategory>(query, (SubCategory, baseImage) =>
                {
                    SubCategory.Image = baseImage;
                    return SubCategory;
                }, splitOn: "FromUrl");

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.GetEntities)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteEntity(int subCategoryId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[sub_category] 
                                         WHERE sub_category_id = {subCategoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
