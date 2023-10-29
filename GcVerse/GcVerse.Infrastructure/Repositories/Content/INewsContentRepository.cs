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
        Task<bool> UpdateNewsContent(int contentId, NewsContent content);
        Task<NewsContent> GetNewsContentById(int contentId);
        Task<List<NewsContent>> GetNewsContentsBySubCategoryId(int subCategoryId);
        Task<bool> DeleteNewsContent(int contentId);
    }
}
