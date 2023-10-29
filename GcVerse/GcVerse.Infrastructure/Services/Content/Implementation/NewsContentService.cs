using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.Content;

namespace GcVerse.Infrastructure.Services.Content.Implementation
{
    public class NewsContentService : INewsContentService
    {
        private readonly ILogger<NewsContentService> _logger;
        private readonly INewsContentRepository _NewsContentRepository;

        public NewsContentService(ILogger<NewsContentService> logger,
                                  INewsContentRepository NewsContentRepository)
        {
            _logger = logger;
            _NewsContentRepository = NewsContentRepository;
        }

        public async Task<bool> CreateNewsContent(UpsertNewsContentRequest upsertNewsContentRequest)
        {
            try
            {
                return await _NewsContentRepository.CreateNewsContent(new NewsContent(upsertNewsContentRequest));
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
                return await _NewsContentRepository.DeleteNewsContent(newsContentId);
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
                return await _NewsContentRepository.GetNewsContentsBySubCategoryId(subCategoryId);
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
                return await _NewsContentRepository.GetNewsContentById(newsContentId);
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
                return await _NewsContentRepository.UpdateNewsContent(newsContentId, new NewsContent(upsertNewsContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(INewsContentService.UpdateNewsContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
