using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using GcVerse.Models.Content;
using Dapper;
using GcVerse.Models.Shared;

namespace GcVerse.Infrastructure.Repositories.Content.Implementation
{
    public class BaseContentRepository : IBaseContentRepository
    {
        private readonly ILogger<BaseContentRepository> _logger;
        private readonly string _connectionString;

        public BaseContentRepository(ILogger<BaseContentRepository> logger,
                                  IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(BaseContent content)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[base_content]
                                       (title,  description,  image_id, sub_category_id, content_type_id)
                                        OUTPUT INSERTED.base_content_id
                                        VALUES (@Title, @Description,{content.Image.Id},@SubCategoryId,@TypeId)";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                
                var id = dbConnection.QuerySingle<int>(processQuery, content);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseContentRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int contentId, BaseContent content)
        {
            try
            {
                string processQuery = @$"UPDATE [dbo].[base_content] 
                                         SET title = @Title,
                                             description = @Description,
                                             image_id = {content.Image.Id},
                                             sub_category_id = @SubCategoryId
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, content) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseContentRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEntity(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[base_content] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseContentRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<BaseContent> GetEntityById(int contentId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[base_content] as cnt
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cnt.image_id
                                  WHERE base_content_id = {contentId} ";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseContent, BaseImage, BaseContent>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    content.Type = (ContentType)content.TypeId;
                    return content;
                }, splitOn: "image_id").AsList();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseContentRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<BaseContent>> GetBaseContentBySubCategoryId(int subCategoryId, ContentType contentType)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[base_content] as cnt
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cnt.image_id
                                  WHERE sub_category_id = {subCategoryId}
                                  AND content_type_id = {(int)contentType}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseContent, BaseImage, BaseContent>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    content.Type = (ContentType)content.TypeId;
                    return content;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseContentRepository.GetBaseContentBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<BaseContent>> GetEntities(int? queryId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[base_content] as cnt
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cnt.image_id
                                  WHERE sub_category_id = {queryId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<BaseContent, BaseImage, BaseContent>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    content.Type = (ContentType)content.TypeId;
                    return content;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseContentRepository.GetBaseContentBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }
    }
}