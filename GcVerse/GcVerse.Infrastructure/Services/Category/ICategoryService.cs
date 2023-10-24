using GcVerse.Models.Category;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Category
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(UpsertCategoryRequest upsertCategoryRequest);
        Task<bool> UpdateCategory(Guid categoryId, UpsertCategoryRequest upsertCategoryRequest);
        Task<BaseCategory> GetCategoryById(Guid categoryId);
        Task<List<BaseCategory>> GetAllCategories();
        Task<bool> DeleteCategoryById(Guid categoryId);
    }
}
