using GcVerse.Models.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using GcVerse.Models.Content;
using Dapper;

namespace GcVerse.Infrastructure.Repositories.Content.Implementation
{
    public class NewsContentRepository : IBaseRepository<NewsContent>
    {
        private readonly ILogger<NewsContentRepository> _logger;
        private readonly string _connectionString;
        private readonly IBaseContentRepository _baseContentRepository;

        public NewsContentRepository(ILogger<NewsContentRepository> logger,
                                     IBaseContentRepository baseContentRepository,
                                     IConfiguration configuration)
        {
            _logger = logger;
            _baseContentRepository = baseContentRepository;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(NewsContent content)
        {
            try
            {
                var contentId = await _baseContentRepository.CreateEntity(content);

                if (contentId == 0)
                    return 0;

                string processQuery = @$"INSERT INTO [dbo].[news_content]
                                       (base_content_id, news_content_text) VALUES
                                       ({contentId}, '{content.Text}')";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                var success = await dbConnection.ExecuteAsync(processQuery) != 0;

                if (!success)
                    await _baseContentRepository.DeleteEntity(contentId);

                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int contentId, NewsContent content)
        {
            try
            {
                await _baseContentRepository.UpdateEntity(contentId, content);

                string processQuery = @$"UPDATE [dbo].[news_content] 
                                         SET news_content_text = '{content.Text}'
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEntity(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[news_content] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                await dbConnection.ExecuteAsync(processQuery);

                if (await _baseContentRepository.DeleteEntity(contentId))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<NewsContent> GetEntityById(int contentId)
        {
            try
            {
                string query = @$"SELECT 
                                     cnt.base_content_id as Id,
									 cnt.sub_category_id as SubCategoryId,
									 cnt.content_type_id as TypeId,
								     cnt.title as Title,
								     cnt.description as Description,
                                     news.news_content_text as Text,
                                     img.image_from_url as FromUrl,
								     img.image_id as Id,
								     img.image_description as Description,
								     img.image_related_id as RelatedId,
								     img.image_url as Url
                                  FROM [dbo].[base_content] as cnt
                                  INNER JOIN [dbo].[news_content] as news on news.base_content_id = cnt.base_content_id
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cnt.image_id
                                  WHERE cnt.base_content_id = {contentId} ";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<NewsContent, BaseImage, NewsContent>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    content.Type = (ContentType)content.TypeId;
                    return content;
                }, splitOn: "FromUrl").AsList();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<NewsContent>> GetEntities(int? subCategoryId)
        {
            try
            {
                string query = @$"SELECT 
                                     cnt.base_content_id as Id,
									 cnt.sub_category_id as SubCategoryId,
									 cnt.content_type_id as TypeId,
								     cnt.title as Title,
								     cnt.description as Description,
                                     news.news_content_text as Text,
                                     img.image_from_url as FromUrl,
								     img.image_id as Id,
								     img.image_description as Description,
								     img.image_related_id as RelatedId,
								     img.image_url as Url
                                  FROM [dbo].[base_content] as cnt
                                  INNER JOIN [dbo].[news_content] as news on news.base_content_id = cnt.base_content_id
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = cnt.image_id
                                  WHERE cnt.sub_category_id = {subCategoryId}
                                  AND cnt.content_type_id = {(int)ContentType.News}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<NewsContent, BaseImage, NewsContent>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    content.Type = (ContentType)content.TypeId;
                    return content;
                }, splitOn: "FromUrl").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.GetEntities)} - Error: " + ex.Message);
                return null;
            }
        }      
    }
}