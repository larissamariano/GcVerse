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
        private readonly IBaseContentRepository _baseContentRepository;

        public ListContentService(ILogger<ListContentService> logger,
                                  IListContentRepository listContentRepository,
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
                return await _listContentRepository.CreateListContent(new ListContent(upsertListContentRequest));
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
                return await _listContentRepository.DeleteListContent(listContentId);
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
                var contents = new List<ListContent>();
                var baseContentList = await _baseContentRepository.GetBaseContentBySubCategoryId(subCategoryId, ContentType.List);
               
                foreach (var baseContent in baseContentList)
                {
                    var topics = await _listContentRepository.GetListTopics(baseContent.Id);

                    contents.Add(new ListContent(baseContent, topics));
                }

                return contents;
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
                var baseContent = await _baseContentRepository.GetBaseContentById(listContentId);
                var topics = await _listContentRepository.GetListTopics(listContentId);

                return new ListContent(baseContent, topics);
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
                return await _listContentRepository.UpdateListContent(listContentId, new ListContent(upsertListContentRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(IListContentService.UpdateListContent)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
