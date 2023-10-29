using GcVerse.Infrastructure.Repositories.Category.Implementation;
using GcVerse.Models.Category;
using GcVerse.Models.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GcVerse.Models.Content;
using Dapper;
using System.Reflection.Metadata;
using System.Reflection;

namespace GcVerse.Infrastructure.Repositories.Content.Implementation
{
    public class NewsContentRepository : INewsContentRepository
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

        public async Task<bool> CreateNewsContent(NewsContent content)
        {
            try
            {
                var contentId = await _baseContentRepository.CreateBaseContent(content);

                if (contentId == 0)
                    return false;

                string processQuery = @$"INSERT INTO [dbo].[news_content]
                                       (base_content_id, news_content_text) VALUES
                                       ({contentId}, '{content.Text}')";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                var success = await dbConnection.ExecuteAsync(processQuery) != 0;

                if (!success)
                    await _baseContentRepository.DeleteBaseContent(contentId);

                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.CreateNewsContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateNewsContent(int contentId, NewsContent content)
        {
            try
            {
                await _baseContentRepository.UpdateBaseContent(contentId, content);

                string processQuery = @$"UPDATE [dbo].[news_content] 
                                         SET news_content_text = '{content.Text}'
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.UpdateNewsContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteNewsContent(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[news_content] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                await dbConnection.ExecuteAsync(processQuery);

                if (await _baseContentRepository.DeleteBaseContent(contentId))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.UpdateListContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<NewsContent> GetNewsContentById(int contentId)
        {
            try
            {
                string query = @$"SELECT 
                                    cnt.base_content_id,
                                    cnt.title,
                                    cnt.description,
                                    cnt.content_type_id,
                                    cnt.sub_category_id,
                                    news.news_content_text,
                                    img.image_id,
                                    img.image_description,
                                    img.image_from_url,
                                    img.image_related_id,
                                    img.image_url
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
                }, splitOn: "image_id").AsList();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.GetNewsContentById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<NewsContent>> GetNewsContentsBySubCategoryId(int subCategoryId)
        {
            try
            {
                string query = @$"SELECT 
                                    cnt.base_content_id,
                                    cnt.title,
                                    cnt.description,
                                    cnt.content_type_id,
                                    cnt.sub_category_id,
                                    news.news_content_text,
                                    img.image_id,
                                    img.image_description,
                                    img.image_from_url,
                                    img.image_related_id,
                                    img.image_url
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
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentRepository.GetNewsContentsBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }      
    }
}
