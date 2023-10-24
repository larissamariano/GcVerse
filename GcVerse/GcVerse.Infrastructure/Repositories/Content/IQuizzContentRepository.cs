using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories.Content
{
    public interface IQuizzContentRepository
    {
        Task<bool> CreateQuizzContent(QuizzContent content);
        Task<bool> UpdateQuizzContent(QuizzContent content);
        Task<QuizzContent> GetQuizzContentById(Guid contentId);
        Task<List<QuizzContent>> GetQuizzContentsBySubCategoryId(Guid subCategoryId);
        Task<bool> DeleteQuizzContent(Guid contentId);
    }
}
