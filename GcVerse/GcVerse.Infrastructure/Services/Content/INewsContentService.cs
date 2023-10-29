using GcVerse.Models.Content;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Content
{
    public interface INewsContentService
    {
        Task<bool> CreateNewsContent(UpsertNewsContentRequest upsertNewsContentRequest);
        Task<bool> UpdateNewsContent(int contentId, UpsertNewsContentRequest upsertNewsContentRequest);
        Task<NewsContent> GetNewsContentById(int contentId);
        Task<List<NewsContent>> GetNewsContentsBySubCategoryId(int   subCategoryId);
        Task<bool> DeleteNewsContentById(int contentId);
    }
}
