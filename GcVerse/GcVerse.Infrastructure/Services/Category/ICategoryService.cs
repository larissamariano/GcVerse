using GcVerse.Models.Category;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Category
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(UpsertCategoryRequest upsertCategoryRequest);
        Task<bool> UpdateCategory(int categoryId, UpsertCategoryRequest upsertCategoryRequest);
        Task<BaseCategory> GetCategoryById(int categoryId);
        Task<List<BaseCategory>> GetAllCategories();
        Task<bool> DeleteCategoryById(int categoryId);
    }
}
