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
    public class ListContentRepository : IListContentRepository
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

        public async Task<bool> CreateListContent(ListContent content)
        {
            try
            {
                var contentId = await _baseContentRepository.CreateBaseContent(content);

                if (contentId == 0)
                    return false;

                content.Topics.ForEach(t => { t.BaseContentId = contentId; });
                var success = await CreateListTopics(content.Topics);

                if (!success)
                    await _baseContentRepository.DeleteBaseContent(contentId);

                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.CreateListContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateListContent(int contentId, ListContent content)
        {
            try
            {
                await _baseContentRepository.UpdateBaseContent(contentId, content);

                if (content.Topics.Any(t => t.Id != 0))
                    await UpdateListTopics(content.Topics.Where(t => t.Id != 0).ToList());

                if (content.Topics.Any(t => t.Id == 0))
                    await CreateListTopics(content.Topics.Where(t => t.Id == 0).ToList());

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.UpdateListContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteListContent(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[list_content_topic] 
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

        private async Task<bool> UpdateListTopics(List<ListTopic> topics)
        {
            try
            {
                topics.ForEach(async t =>
                {
                    string processQuery = @$"UPDATE [dbo].[list_content_topic] 
                                         SET title = @Title,
                                             description = @Description,
                                             image_id = @ImageId,
                                         WHERE topic_id = {t.Id}";

                    using IDbConnection dbConnection = new SqlConnection(_connectionString);
                    await dbConnection.ExecuteAsync(processQuery, t);
                });

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentRepository.CreateListTopics)} - Error: " + ex.Message);
                return false;
            }
        }


        public async Task<List<ListTopic>> GetListTopics(int contentId)
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
    }
}
