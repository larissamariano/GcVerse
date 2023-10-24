using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.Content;

namespace GcVerse.Infrastructure.Services.Content.Implementation
{
    public class QuizzContentService : IQuizzContentService
    {
        private readonly ILogger<QuizzContentService> _logger;
        private readonly IQuizzContentRepository _QuizzContentRepository;

        public QuizzContentService(ILogger<QuizzContentService> logger,
                                  IQuizzContentRepository QuizzContentRepository)
        {
            _logger = logger;
            _QuizzContentRepository = QuizzContentRepository;
        }

        public async Task<bool> CreateQuizzContent(UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            try
            {
                return await _QuizzContentRepository.CreateQuizzContent(new QuizzContent(upsertQuizzContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.CreateQuizzContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteQuizzContentById(Guid quizzContentId)
        {
            try
            {
                return await _QuizzContentRepository.DeleteQuizzContent(quizzContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.DeleteQuizzContentById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<QuizzContent>> GetQuizzContentsBySubCategoryId(Guid subCategoryId)
        {
            try
            {
                return await _QuizzContentRepository.GetQuizzContentsBySubCategoryId(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.GetQuizzContentsBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<QuizzContent> GetQuizzContentById(Guid quizzContentId)
        {
            try
            {
                return await _QuizzContentRepository.GetQuizzContentById(quizzContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.GetQuizzContentById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateQuizzContent(Guid quizzContentId, UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            try
            {
                return await _QuizzContentRepository.UpdateQuizzContent(new QuizzContent(upsertQuizzContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IQuizzContentService.UpdateQuizzContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
