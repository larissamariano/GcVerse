using models = GcVerse.Models.Category;
using GcVerse.Models.Request;

namespace GcVerse.Infrastructure.Services.Category
{
    public interface ISubCategoryService
    {
        Task<bool> CreateSubCategory(UpsertSubCategoryRequest upsertSubCategoryRequest);
        Task<bool> UpdateSubCategory(Guid subCategoryId, UpsertSubCategoryRequest upsertSubCategoryRequest);
        Task<models.SubCategory> GetSubCategoryById(Guid subCategoryId);
        Task<List<models.SubCategory>> GetSubCategoriesListByCategoryId(Guid categoryId);
        Task<bool> DeleteSubCategoryById(Guid subCategoryId);
    }
}
