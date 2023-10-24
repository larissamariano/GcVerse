using GcVerse.Models.Category;
using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories.Content
{
    public interface IListContentRepository
    {
        Task<bool> CreateListContent(ListContent content);
        Task<bool> UpdateListContent(ListContent content);
        Task<ListContent> GetListContentById(Guid contentId);
        Task<List<ListContent>> GetListContentsBySubCategoryId(Guid subCategoryId);
        Task<bool> DeleteListContent(Guid contentId);
    }
}
