using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories.Content
{
    public interface INewsContentRepository
    {
        Task<bool> CreateNewsContent(NewsContent content);
        Task<bool> UpdateNewsContent(NewsContent content);
        Task<NewsContent> GetNewsContentById(Guid contentId);
        Task<List<NewsContent>> GetNewsContentsBySubCategoryId(Guid subCategoryId);
        Task<bool> DeleteNewsContent(Guid contentId);
    }
}
