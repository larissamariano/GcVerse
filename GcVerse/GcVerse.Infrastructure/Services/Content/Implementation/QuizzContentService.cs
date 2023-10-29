using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.Content;
using GcVerse.Infrastructure.Repositories.Content.Implementation;
using GcVerse.Infrastructure.Repositories;

namespace GcVerse.Infrastructure.Services.Content.Implementation
{
    public class QuizzContentService : IQuizzContentService
    {
        private readonly ILogger<QuizzContentService> _logger;
        private readonly IBaseRepository<QuizzContent> _quizzContentRepository;
        private readonly IBaseContentRepository _baseContentRepository;

        public QuizzContentService(ILogger<QuizzContentService> logger,
                                   IBaseRepository<QuizzContent> quizzContentRepository,
                                   IBaseContentRepository baseContentRepository)
        {
            _logger = logger;
            _quizzContentRepository = quizzContentRepository;
            _baseContentRepository = baseContentRepository;
        }

        public async Task<bool> CreateQuizzContent(UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            try
            {
                return await _quizzContentRepository.CreateEntity(new QuizzContent(upsertQuizzContentRequest)) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.CreateQuizzContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteQuizzContentById(int quizzContentId)
        {
            try
            {
                return await _quizzContentRepository.DeleteEntity(quizzContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.DeleteQuizzContentById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<QuizzContent>> GetQuizzContentsBySubCategoryId(int subCategoryId)
        {
            try
            {
                return await _quizzContentRepository.GetEntities(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.GetQuizzContentsBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<QuizzContent> GetQuizzContentById(int quizzContentId)
        {
            try
            {
                return await _quizzContentRepository.GetEntityById(quizzContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.GetQuizzContentById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateQuizzContent(int quizzContentId, UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            try
            {
                return await _quizzContentRepository.UpdateEntity(quizzContentId, new QuizzContent(upsertQuizzContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.UpdateQuizzContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}