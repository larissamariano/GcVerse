using GcVerse.Infrastructure.Services.Category;
using GcVerse.Models.Category;
using GcVerse.Models.Request;
using GcVerse.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Categorias
    /// </summary>
    [ApiController]
    [Authorize]
    [Authorize(Roles = Permission.Administrator)]
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
        /// <response code="200">Requisição realizada com Sucesso.</response>
        /// <response code="401">Requisição não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação.</response>
        /// <response code="400">Erro ao realizar requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
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
        /// <response code="200">Requisição realizada com Sucesso.</response>
        /// <response code="401">Requisição não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação.</response>
        /// <response code="400">Erro ao realizar requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [Authorize(Roles = $"{Permission.Administrator},{Permission.Basic}")]
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [Authorize(Roles = $"{Permission.Administrator},{Permission.Basic}")]
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
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