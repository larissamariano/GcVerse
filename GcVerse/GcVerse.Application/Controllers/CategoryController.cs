using GcVerse.Infrastructure.Services.Category;
using GcVerse.Models.Category;
using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Categorias
    /// </summary>
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger,
                                  ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="upsertCategoryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] UpsertCategoryRequest upsertCategoryRequest)
        {
            try
            {
                var result = await _categoryService.CreateCategory(upsertCategoryRequest);

                if (result)
                    return Ok("Categoria criada com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao criar uma nova categoria." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.CreateCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma categoria de acordo com o id informado.
        /// </summary>
        /// <param name="categoryId"> Id da Categoria </param>
        /// <param name="upsertCategoryRequest"></param>
        /// <returns></returns>
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromBody] UpsertCategoryRequest upsertCategoryRequest)
        {
            try
            {
                var result = await _categoryService.UpdateCategory(categoryId, upsertCategoryRequest);

                if (result)
                    return Ok("Categoria atualizada com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao atualizar categoria." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.UpdateCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma categoria de acordo com o id informado.
        /// </summary>
        /// <param name="categoryId"> Id da Categoria</param>
        /// <returns></returns>
        [HttpGet("{categoryId}")]
        public async Task<BaseCategory> GetCategoryById([FromRoute] int categoryId)
        {
            try
            {
                return await _categoryService.GetCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.GetCategoryById)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma lista com todas as categorias cadastradas no sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<List<BaseCategory>> GetAllCategories()
        {
            try
            {
                return await _categoryService.GetAllCategories();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.GetAllCategories)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deleta uma categoria de acordo com o id informado.
        /// </summary>
        /// <param name="categoryId">Id da Categoria</param>
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] int categoryId)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryById(categoryId);

                if (result)
                    return Ok("Categoria deletada com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao deletar categoria." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.DeleteCategoryById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }
    }
}