using GcVerse.Models.Category;

namespace GcVerse.Infrastructure.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategory(BaseCategory baseCategory);
        Task<bool> UpdateCategory(BaseCategory baseCategory);
        Task<BaseCategory> GetCategoryById(Guid categoryId);
        Task<List<BaseCategory>> GetAllCategories();
        Task<bool> DeleteCategoryById(Guid categoryId);
    }
}
