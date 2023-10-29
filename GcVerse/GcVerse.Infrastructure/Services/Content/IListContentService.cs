using GcVerse.Models.Content;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Content
{
    public interface IListContentService
    {
        Task<bool> CreateListContent(UpsertListContentRequest upsertListContentRequest);
        Task<bool> UpdateListContent(int contentId, UpsertListContentRequest upsertListContentRequest);
        Task<ListContent> GetListContentById(int contentId);
        Task<List<ListContent>> GetListContentsBySubCategoryId(int subCategoryId);
        Task<bool> DeleteListContentById(int contentId);
    }
}
