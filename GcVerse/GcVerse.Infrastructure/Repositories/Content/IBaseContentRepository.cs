using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories.Content
{
    public interface IBaseContentRepository
    {
        Task<int> CreateBaseContent(BaseContent content);
        Task<BaseContent> GetBaseContentById(int contentId);
        Task<List<BaseContent>> GetBaseContentBySubCategoryId(int subCategoryId, ContentType contentType);
        Task<bool> UpdateBaseContent(int contentId, BaseContent content);   
        Task<bool> DeleteBaseContent(int contentId);
    }
}
