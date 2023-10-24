using GcVerse.Infrastructure.Repositories.Category;
using GcVerse.Infrastructure.Services.Category;
using GcVerse.Models.Request;
using models = GcVerse.Models.Category;
using Microsoft.Extensions.Logging;

namespace GcVerse.Infrastructure.Services.SubCategory.Implementation
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ILogger<SubCategoryService> _logger;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(ILogger<SubCategoryService> logger,
                                  ISubCategoryRepository subCategoryRepository)
        {
            _logger = logger;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<bool> CreateSubCategory(UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            try
            {
                return await _subCategoryRepository.CreateSubCategory(new models.SubCategory(upsertSubCategoryRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.CreateSubCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSubCategoryById(Guid subCategoryId)
        {
            try
            {
                return await _subCategoryRepository.DeleteSubCategoryById(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.DeleteSubCategoryById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<models.SubCategory>> GetSubCategoriesListByCategoryId(Guid categoryId)
        {
            try
            {
                return await _subCategoryRepository.GetSubCategoriesbyCategoryId(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.GetSubCategoriesListByCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<models.SubCategory> GetSubCategoryById(Guid subCategoryId)
        {
            try
            {
                return await _subCategoryRepository.GetSubCategoryById(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.GetSubCategoryById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateSubCategory(Guid SubCategoryId, UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            try
            {
                return await _subCategoryRepository.UpdateSubCategory(new models.SubCategory(upsertSubCategoryRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.UpdateSubCategory)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
