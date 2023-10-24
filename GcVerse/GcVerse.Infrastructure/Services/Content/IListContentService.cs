using GcVerse.Models.Content;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Content
{
    public interface IListContentService
    {
        Task<bool> CreateListContent(UpsertListContentRequest upsertListContentRequest);
        Task<bool> UpdateListContent(Guid contentId, UpsertListContentRequest upsertListContentRequest);
        Task<ListContent> GetListContentById(Guid contentId);
        Task<List<ListContent>> GetListContentsBySubCategoryId(Guid subCategoryId);
        Task<bool> DeleteListContentById(Guid contentId);
    }
}
