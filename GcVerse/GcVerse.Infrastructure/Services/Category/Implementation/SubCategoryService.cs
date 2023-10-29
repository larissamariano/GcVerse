using GcVerse.Models.Request;
using GcVerse.Models.Category;
using Microsoft.Extensions.Logging;
using GcVerse.Infrastructure.Repositories;

namespace GcVerse.Infrastructure.Services.Category.Implementation
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ILogger<SubCategoryService> _logger;
        private readonly IBaseRepository<SubCategory> _subCategoryRepository;

        public SubCategoryService(ILogger<SubCategoryService> logger,
                                  IBaseRepository<SubCategory> subCategoryRepository)
        {
            _logger = logger;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<bool> CreateSubCategory(UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            try
            {
                return await _subCategoryRepository.CreateEntity(new SubCategory(upsertSubCategoryRequest)) != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.CreateSubCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSubCategoryById(int subCategoryId)
        {
            try
            {
                return await _subCategoryRepository.DeleteEntity(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.DeleteSubCategoryById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<SubCategory>> GetSubCategoriesListByCategoryId(int categoryId)
        {
            try
            {
                return await _subCategoryRepository.GetEntities(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.GetSubCategoriesListByCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<SubCategory> GetSubCategoryById(int subCategoryId)
        {
            try
            {
                return await _subCategoryRepository.GetEntityById(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.GetSubCategoryById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateSubCategory(int subCategoryId, UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            try
            {
                return await _subCategoryRepository.UpdateEntity(subCategoryId, new SubCategory(upsertSubCategoryRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ISubCategoryService.UpdateSubCategory)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
