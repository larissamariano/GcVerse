using GcVerse.Models.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using GcVerse.Models.Content;
using Dapper;

namespace GcVerse.Infrastructure.Repositories.Content.Implementation
{
    public class QuizzContentRepository : IBaseRepository<QuizzContent>
    {
        private readonly ILogger<QuizzContentRepository> _logger;
        private readonly string _connectionString;
        private readonly IBaseContentRepository _baseContentRepository;

        public QuizzContentRepository(ILogger<QuizzContentRepository> logger,
                                     IBaseContentRepository baseContentRepository,
                                     IConfiguration configuration)
        {
            _logger = logger;
            _baseContentRepository = baseContentRepository;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<int> CreateEntity(QuizzContent content)
        {
            try
            {
                var contentId = await _baseContentRepository.CreateEntity(content);

                if (contentId == 0)
                    return 0;

                content.Questions.ForEach(q => { q.QuizzId = contentId; });
                content.Results.ForEach(r => { r.QuizzId = contentId; });

                var success = await CreateQuestions(contentId, content.Questions);

                if (!success)
                {
                    await _baseContentRepository.DeleteEntity(contentId);
                    return 0;
                }

                success = await CreateResults(contentId, content.Results);

                if (!success)
                    await _baseContentRepository.DeleteEntity(contentId);

                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.CreateEntity)} - Error: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateEntity(int contentId, QuizzContent content)
        {
            try
            {
                await _baseContentRepository.UpdateEntity(contentId, content);

                await DeleteOptions(contentId);

                await DeleteQuestions(contentId);

                await DeleteResults(contentId);

                if (!await CreateQuestions(contentId, content.Questions))
                    return false;

                if (!await CreateResults(contentId, content.Results))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.UpdateEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEntity(int contentId)
        {
            try
            {
                await DeleteOptions(contentId);
                await DeleteQuestions(contentId);
                await DeleteResults(contentId);
                await _baseContentRepository.DeleteEntity(contentId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.DeleteEntity)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> CreateQuestions(int contentId, List<QuizzQuestion> questions)
        {
            try
            {
                foreach (var question in questions)
                {
                    string processQuery = @$"INSERT INTO [dbo].[quizz_question]
                                          (title,  description,  image_id, base_content_id)
                                          OUTPUT INSERTED.question_id
                                          VALUES ('{question.Title}', '{question.Description}',{question.Image.Id},{contentId})";

                    using IDbConnection dbConnection = new SqlConnection(_connectionString);
                    var questionId = dbConnection.QuerySingle<int>(processQuery);

                    await CreateOptions(contentId, questionId, question.Options);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.CreateQuestions)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteQuestions(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[quizz_question] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.DeleteQuestions)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> CreateOptions(int contentId, int questionId, List<QuestionOption> options)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[question_option]
                                       (title,  description,  image_id, question_id, base_content_id) VALUES
                                       (@Title, @Description, @ImageId, {questionId}, {contentId})";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, options) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.CreateOptions)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteOptions(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[question_option] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.DeleteOptions)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> CreateResults(int questionId, List<QuizzResult> results)
        {
            try
            {
                string processQuery = @$"INSERT INTO [dbo].[quizz_result]
                                       (title,  description,  image_id, base_content_id) VALUES
                                       (@Title, @Description, @ImageId, {questionId})";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery, results) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.CreateResults)} - Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteResults(int contentId)
        {
            try
            {
                string processQuery = @$"DELETE FROM [dbo].[quizz_result] 
                                         WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                return await dbConnection.ExecuteAsync(processQuery) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.DeleteResults)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<QuizzQuestion>> GetQuestions(int contentId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[quizz_question] as qzz
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = qzz.image_id
                                  WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<QuizzQuestion, BaseImage, QuizzQuestion>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    return content;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.GetQuestions)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<QuestionOption>> GetOptions(int contentId, int questionId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[question_option] as qst
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = qst.image_id
                                  WHERE base_content_id = {contentId}
                                  AND question_id = {questionId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<QuestionOption, BaseImage, QuestionOption>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    return content;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.GetOptions)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<QuizzResult>> GetResults(int contentId)
        {
            try
            {
                string query = @$"SELECT 
                                  *
                                  FROM [dbo].[quizz_result] as rst
                                  INNER JOIN [dbo].[base_image] as img on img.image_id = rst.image_id
                                  WHERE base_content_id = {contentId}";

                using IDbConnection dbConnection = new SqlConnection(_connectionString);

                var result = dbConnection.Query<QuizzResult, BaseImage, QuizzResult>(query, (content, baseImage) =>
                {
                    content.Image = baseImage;
                    return content;
                }, splitOn: "image_id").AsList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.GetResults)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<QuizzContent> GetEntityById(int entityId)
        {
            try
            {
                var baseContent = await _baseContentRepository.GetEntityById(entityId);
                var questions = await GetQuestions(entityId);

                questions.ForEach(async q =>
                {
                    q.Options = await GetOptions(entityId, q.QuestionId);
                });

                var results = await GetResults(entityId);

                return new QuizzContent(baseContent, questions, results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.GetEntityById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<QuizzContent>> GetEntities(int? queryId)
        {
            try
            {
                var contents = new List<QuizzContent>();
                var baseContentList = await _baseContentRepository.GetBaseContentBySubCategoryId(queryId.Value, ContentType.Quizz);

                foreach (var baseContent in baseContentList)
                {
                    var questions = await GetQuestions(baseContent.Id);

                    questions.ForEach(async q =>
                    {
                        q.Options = await GetOptions(baseContent.Id, q.QuestionId);
                    });

                    var results = await GetResults(baseContent.Id);

                    contents.Add(new QuizzContent(baseContent, questions, results));
                }

                return contents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentRepository.GetEntities)} - Error: " + ex.Message);
                return null;
            }
        }
    }
}