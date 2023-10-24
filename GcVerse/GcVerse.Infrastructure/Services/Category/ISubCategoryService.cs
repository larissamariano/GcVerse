using GcVerse.Models.Category;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Category
{
    public interface ISubCategoryService
    {
        Task<bool> CreateSubCategory(UpsertSubCategoryRequest upsertSubCategoryRequest);
        Task<bool> UpdateSubCategory(Guid subCategoryId, UpsertSubCategoryRequest upsertSubCategoryRequest);
        Task<SubCategory> GetSubCategoryById(Guid subCategoryId);
        Task<List<SubCategory>> GetSubCategoriesListByCategoryId(Guid categoryId);
        Task<bool> DeleteSubCategoryById(Guid subCategoryId);
    }
}
