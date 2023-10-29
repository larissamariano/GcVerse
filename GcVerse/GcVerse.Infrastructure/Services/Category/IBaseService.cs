using GcVerse.Models.Category;
using GcVerse.Models.Request;
using GcVerse.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Services.Category
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> CreateCategory(UpsertCategoryRequest upsertCategoryRequest);
        Task<bool> UpdateCategory(int categoryId, UpsertCategoryRequest upsertCategoryRequest);
        Task<BaseCategory> GetCategoryById(int categoryId);
        Task<List<BaseCategory>> GetAllCategories();
        Task<bool> DeleteCategoryById(int categoryId);
    }
}
