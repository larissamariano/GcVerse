using GcVerse.Models.Content;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Content
{
    public interface INewsContentService
    {
        Task<bool> CreateNewsContent(UpsertNewsContentRequest upsertNewsContentRequest);
        Task<bool> UpdateNewsContent(Guid contentId, UpsertNewsContentRequest upsertNewsContentRequest);
        Task<NewsContent> GetNewsContentById(Guid contentId);
        Task<List<NewsContent>> GetNewsContentsBySubCategoryId(Guid subCategoryId);
        Task<bool> DeleteNewsContentById(Guid contentId);
    }
}
