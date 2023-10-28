using GcVerse.Models.Category;

namespace GcVerse.Infrastructure.Repositories.Category
{
    public interface ISubCategoryRepository
    {
        Task<bool> CreateSubCategory(SubCategory baseCategory);
        Task<bool> UpdateSubCategory(int subCategoryId, SubCategory baseCategory);
        Task<SubCategory> GetSubCategoryById(int subCategoryId);
        Task<List<SubCategory>> GetSubCategoriesbyCategoryId(int categoryId);
        Task<bool> DeleteSubCategoryById(int categoryId);
    }
}
