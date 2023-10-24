using GcVerse.Models.Category;

namespace GcVerse.Infrastructure.Repositories.Category
{
    public interface ISubCategoryRepository
    {
        Task<bool> CreateSubCategory(SubCategory baseCategory);
        Task<bool> UpdateSubCategory(SubCategory baseCategory);
        Task<SubCategory> GetSubCategoryById(Guid subCategoryId);
        Task<List<SubCategory>> GetSubCategoriesbyCategoryId(Guid categoryId);
        Task<bool> DeleteSubCategoryById(Guid categoryId);
    }
}
