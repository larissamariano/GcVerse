using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories.Content
{
    public interface IBaseContentRepository : IBaseRepository<BaseContent>
    {
        Task<List<BaseContent>> GetBaseContentBySubCategoryId(int subCategoryId, ContentType contentType);
    }
}
