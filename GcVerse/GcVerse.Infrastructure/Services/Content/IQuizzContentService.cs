using GcVerse.Models.Content;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Content
{
    public interface IQuizzContentService
    {
        Task<bool> CreateQuizzContent(UpsertQuizzContentRequest upsertQuizzContentRequest);
        Task<bool> UpdateQuizzContent(int contentId, UpsertQuizzContentRequest upsertQuizzContentRequest);
        Task<QuizzContent> GetQuizzContentById(int contentId);
        Task<List<QuizzContent>> GetQuizzContentsBySubCategoryId(int subCategoryId);
        Task<bool> DeleteQuizzContentById(int contentId);
    }
}
