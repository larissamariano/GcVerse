using GcVerse.Models.Category;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Category
{
    public interface ISubCategoryService
    {
        Task<bool> CreateSubCategory(UpsertSubCategoryRequest upsertSubCategoryRequest);
        Task<bool> UpdateSubCategory(int subCategoryId, UpsertSubCategoryRequest upsertSubCategoryRequest);
        Task<SubCategory> GetSubCategoryById(int subCategoryId);
        Task<List<SubCategory>> GetSubCategoriesListByCategoryId(int categoryId);
        Task<bool> DeleteSubCategoryById(int subCategoryId);
    }
}
