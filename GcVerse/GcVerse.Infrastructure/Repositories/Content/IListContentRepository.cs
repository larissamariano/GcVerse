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
        Task<bool> UpdateListContent(int contentId, ListContent content);
        Task<bool> DeleteListContent(int contentId);
        Task<List<ListTopic>> GetListTopics(int contentId);

    }
}
