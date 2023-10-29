using GcVerse.Models.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using GcVerse.Models.Content;
using Dapper;
using GcVerse.Infrastructure.Services.Content;
using GcVerse.Models.Category;

namespace GcVerse.Infrastructure.Repositories.Content.Implementation
{
    public class ListContentRepository : IBaseRepository<ListContent>
    {
        private readonly ILogger<ListContentRepository> _logger;
        private readonly string _connectionString;
        private readonly IBaseContentRepository _baseContentRepository;

        public ListContentRepository(ILogger<ListContentRepository> logger,
                                     IBaseContentRepository baseContentRepository,
                                     IConfiguration configuration)
        {
            _logger = logger;
            _baseContentRepository = baseContentRepository;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(ListContent content)
        {
            try
            {
                var contentId = await _baseContentRepository.CreateEntity(content);

                if (contentId == 0)
                    return 0;

                content.Topics.ForEach(t => { t.BaseContentId = contentId; });
                var success = await CreateListTopics(content.Topics);

                if (!success)
                    await _baseContentRepository.DeleteEntity(contentId);

                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int contentId, ListContent content)
        {
            try
            {
                await _baseContentRepository.UpdateEntity(contentId, content);
                content.Topics.ForEach(t => { t.BaseContentId = contentId; });

                await DeleteTopics(contentId);

                if (!await CreateListTopics(content.Topics))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEntity(int contentId)
        {
            try
            {
                if (!await DeleteTopics(contentId))
                    return false;

                if (!await _baseContentRepository.DeleteEntity(contentId))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> CreateListTopics(List<ListTopic> topics)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[list_content_topic]
                                       (title,  description,  image_id, base_content_id) VALUES
                                       (@Title, @Description, @ImageId, @BaseContentId)";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, topics) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.CreateListTopics)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteTopics(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[list_content_topic] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.DeleteTopics)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<List<ListTopic>> GetListTopics(int contentId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[list_content_topic] as tpc
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = tpc.image_id
                                  WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<ListTopic, BaseImage, ListTopic>(query, (topic, baseImage) =>
                {
                    topic.Image = baseImage;
                    return topic;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.GetListTopics)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<ListContent> GetEntityById(int entityId)
        {
            try
            {
                var baseContent = await _baseContentRepository.GetEntityById(entityId);
                var topics = await GetListTopics(entityId);

                return new ListContent(baseContent, topics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<ListContent>> GetEntities(int? queryId)
        {
            try
            {
                var contents = new List<ListContent>();
                var baseContentList = await _baseContentRepository.GetBaseContentBySubCategoryId(queryId.Value, ContentType.List);

                foreach (var baseContent in baseContentList)
                {
                    var topics = await GetListTopics(baseContent.Id);

                    contents.Add(new ListContent(baseContent, topics));
                }

                return contents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.GetEntities)} - Error: " + ex.Message);
                return null;
            }
        }
    }
}