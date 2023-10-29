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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ILogger<SubCategoryRepository> _logger;
        private readonly string _connectionString;

        public SubCategoryRepository(ILogger<SubCategoryRepository> logger,
                                  IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<bool> CreateSubCategory(SubCategory subCategory)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[sub_category]
                                       (title,  description,  image_id, category_id, related_info) VALUES
                                       (@Title, @Description,{subCategory.Image.Id},@CategoryId,@Related)";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, subCategory) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.CreateSubCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateSubCategory(int subCategoryId, SubCategory subCategory)
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
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.UpdateSubCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<SubCategory> GetSubCategoryById(int subCategoryId)
        {
            try
            {             
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[sub_category] as sub
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = sub.image_id
                                  WHERE sub_category_id = {subCategoryId} ";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<SubCategory, BaseImage, SubCategory>(query, (SubCategory, baseImage) =>
                {
                    SubCategory.Image = baseImage;
                    return SubCategory;
                }, splitOn: "image_id").AsList();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.GetSubCategoryById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<SubCategory>> GetSubCategoriesbyCategoryId(int categoryId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[sub_category] as sub
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = sub.image_id
                                  WHERE category_id = {categoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<SubCategory, BaseImage, SubCategory>(query, (SubCategory, baseImage) =>
                {
                    SubCategory.Image = baseImage;
                    return SubCategory;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.GetSubCategoriesbyCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteSubCategoryById(int subCategoryId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[sub_category] 
                                         WHERE sub_Category_id = {subCategoryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryRepository.DeleteSubCategoryById)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
