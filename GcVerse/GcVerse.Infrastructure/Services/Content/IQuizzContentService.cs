using GcVerse.Models.Content;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Content
{
    public interface IQuizzContentService
    {
        Task<bool> CreateQuizzContent(UpsertQuizzContentRequest upsertQuizzContentRequest);
        Task<bool> UpdateQuizzContent(Guid contentId, UpsertQuizzContentRequest upsertQuizzContentRequest);
        Task<QuizzContent> GetQuizzContentById(Guid contentId);
        Task<List<QuizzContent>> GetQuizzContentsBySubCategoryId(Guid subCategoryId);
        Task<bool> DeleteQuizzContentById(Guid contentId);
    }
}
