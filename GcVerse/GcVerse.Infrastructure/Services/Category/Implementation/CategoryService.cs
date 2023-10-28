using GcVerse.Infrastructure.Repositories.Category;
using GcVerse.Models.Category;
using GcVerse.Models.Request;
using Microsoft.Extensions.Logging;

namespace GcVerse.Infrastructure.Services.Category.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ILogger<CategoryService> logger,
                               ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateCategory(UpsertCategoryRequest upsertCategoryRequest)
        {
            try
            {
                return await _categoryRepository.CreateCategory(new BaseCategory(upsertCategoryRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ICategoryService.CreateCategory)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCategoryById(int categoryId)
        {
            try
            {
                return await _categoryRepository.DeleteCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ICategoryService.DeleteCategoryById)} - Error: " + ex.Message);
                return false;
            }
        }

        public async Task<List<BaseCategory>> GetAllCategories()
        {
            try
            {
                return await _categoryRepository.GetAllCategories();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ICategoryService.GetAllCategories)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<BaseCategory> GetCategoryById(int categoryId)
        {
            try
            {
                return await _categoryRepository.GetCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ICategoryService.GetCategoryById)} - Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateCategory(int categoryId, UpsertCategoryRequest upsertCategoryRequest)
        {
            try
            {
                return await _categoryRepository.UpdateCategory(categoryId, new BaseCategory(upsertCategoryRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ICategoryService.UpdateCategory)} - Error: " + ex.Message);
                return false;
            }
        }
    }
}
