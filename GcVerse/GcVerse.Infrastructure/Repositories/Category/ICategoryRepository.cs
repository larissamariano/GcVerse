using GcVerse.Models.Category;

namespace GcVerse.Infrastructure.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategory(BaseCategory baseCategory);
        Task<bool> UpdateCategory(int categoryId, BaseCategory baseCategory);
        Task<BaseCategory> GetCategoryById(int categoryId);
        Task<List<BaseCategory>> GetAllCategories();
        Task<bool> DeleteCategoryById(int categoryId);
    }
}
