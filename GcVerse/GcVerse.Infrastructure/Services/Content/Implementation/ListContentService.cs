using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;
using GcVerse.Models.Content;
using GcVerse.Infrastructure.Repositories;

namespace GcVerse.Infrastructure.Services.Content.Implementation
{
    public class ListContentService : IListContentService
    {
        private readonly ILogger<ListContentService> _logger;
        private readonly IBaseRepository<ListContent> _listContentRepository;
        private readonly IBaseContentRepository _baseContentRepository;

        public ListContentService(ILogger<ListContentService> logger,
                                  IBaseRepository<ListContent> listContentRepository,
                                  IBaseContentRepository baseContentRepository)
        {
            _logger = logger;
            _listContentRepository = listContentRepository;
            _baseContentRepository = baseContentRepository;
        }

        public async Task<bool> CreateListContent(UpsertListContentRequest upsertListContentRequest)
        {
            try
            {
                return await _listContentRepository.CreateEntity(new ListContent(upsertListContentRequest)) !=0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.CreateListContent)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteListContentById(int listContentId)
        {
            try
            {
                return await _listContentRepository.DeleteEntity(listContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.DeleteListContentById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<ListContent>> GetListContentsBySubCategoryId(int subCategoryId)
        {
            try
            {
                return await _listContentRepository.GetEntities(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.GetListContentsBySubCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<ListContent> GetListContentById(int listContentId)
        {
            try
            {
                return await _listContentRepository.GetEntityById(listContentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.GetListContentById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateListContent(int listContentId, UpsertListContentRequest upsertListContentRequest)
        {
            try
            {
                return await _listContentRepository.UpdateEntity(listContentId, new ListContent(upsertListContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.UpdateListContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
