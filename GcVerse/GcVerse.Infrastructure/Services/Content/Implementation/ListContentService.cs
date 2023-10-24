using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.Content;

namespace GcVerse.Infrastructure.Services.Content.Implementation
{
    public class ListContentService : IListContentService
    {
        private readonly ILogger<ListContentService> _logger;
        private readonly IListContentRepository _listContentRepository;

        public ListContentService(ILogger<ListContentService> logger,
                                  IListContentRepository listContentRepository)
        {
            _logger = logger;
            _listContentRepository = listContentRepository;
        }

        public async Task<bool> CreateListContent(UpsertListContentRequest upsertListContentRequest)
        {
            try
            {
                return await _listContentRepository.CreateListContent(new ListContent(upsertListContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.CreateListContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteListContentById(Guid listContentId)
        {
            try
            {
                return await _listContentRepository.DeleteListContent(listContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.DeleteListContentById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<ListContent>> GetListContentsBySubCategoryId(Guid subCategoryId)
        {
            try
            {
                return await _listContentRepository.GetListContentsBySubCategoryId(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.GetListContentsBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<ListContent> GetListContentById(Guid ListContentId)
        {
            try
            {
                return await _listContentRepository.GetListContentById(ListContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.GetListContentById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateListContent(Guid ListContentId, UpsertListContentRequest upsertListContentRequest)
        {
            try
            {
                return await _listContentRepository.UpdateListContent(new ListContent(upsertListContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.UpdateListContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
