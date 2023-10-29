using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.Content;
using GcVerse.Infrastructure.Repositories;

namespace GcVerse.Infrastructure.Services.Content.Implementation
{
    public class NewsContentService : INewsContentService
    {
        private readonly ILogger<NewsContentService> _logger;
        private readonly IBaseRepository<NewsContent> _newsContentRepository;

        public NewsContentService(ILogger<NewsContentService> logger,
                                  IBaseRepository<NewsContent> newsContentRepository)
        {
            _logger = logger;
            _newsContentRepository = newsContentRepository;
        }

        public async Task<bool> CreateNewsContent(UpsertNewsContentRequest upsertNewsContentRequest)
        {
            try
            {
                return await _newsContentRepository.CreateEntity(new NewsContent(upsertNewsContentRequest)) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(INewsContentService.CreateNewsContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteNewsContentById(int newsContentId)
        {
            try
            {
                return await _newsContentRepository.DeleteEntity(newsContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(INewsContentService.DeleteNewsContentById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<NewsContent>> GetNewsContentsBySubCategoryId(int subCategoryId)
        {
            try
            {
                return await _newsContentRepository.GetEntities(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(INewsContentService.GetNewsContentsBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<NewsContent> GetNewsContentById(int newsContentId)
        {
            try
            {
                return await _newsContentRepository.GetEntityById(newsContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(INewsContentService.GetNewsContentById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateNewsContent(int newsContentId, UpsertNewsContentRequest upsertNewsContentRequest)
        {
            try
            {
                return await _newsContentRepository.UpdateEntity(newsContentId, new NewsContent(upsertNewsContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(INewsContentService.UpdateNewsContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
