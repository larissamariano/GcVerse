using GcVerse.Infrastructure.Services.Category;
using GcVerse.Models.Category;
using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// SubCategorias
    /// </summary>
    [ApiController]
    [Route("subCategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> _logger;
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ILogger<SubCategoryController> logger, 
                                     ISubCategoryService subCategoryService)
                                  
        {
            _logger = logger;
            _subCategoryService = subCategoryService;
        }

        /// <summary>
        /// Cria uma nova subCategoria.
        /// </summary>
        /// <param name="upsertCategoryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            try
            {
                var result = await _subCategoryService.CreateSubCategory(upsertSubCategoryRequest);

                if (result)
                    return Ok("SubCategoria criada com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao criar uma nova subCategoria." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryController.CreateSubCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma subCategoria de acordo com o id informado.
        /// </summary>
        /// <param name="subCategoryId"> Id da Categoria </param>
        /// <param name="upsertSubCategoryRequest"></param>
        /// <returns></returns>
        [HttpPut("{subCategoryId}")]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] Guid subCategoryId, [FromBody] UpsertSubCategoryRequest upsertSubCategoryRequest)
        {
            try
            {
                var result = await _subCategoryService.UpdateSubCategory(subCategoryId, upsertSubCategoryRequest);

                if (result)
                    return Ok("SubCategoria atualizada com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao atualizar subCategoria." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryController.UpdateSubCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma subCategoria de acordo com o id informado.
        /// </summary>
        /// <param name="subCategoryId"> Id da SubCategoria</param>
        /// <returns></returns>
        [HttpGet("{subCategoryId}")]
        public async Task<SubCategory> GetSubCategoryById([FromRoute] Guid subCategoryId)
        {
            try
            {
               return await _subCategoryService.GetSubCategoryById(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryController.GetSubCategoryById)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma lista com todas as subCategorias de acordo com o id da categoria informada.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{categoryId}/all")]
        public async Task<List<SubCategory>> GetAllSubCategoriesByCategoryId([FromRoute] Guid categoryId)
        {
            try
            {
               return await _subCategoryService.GetSubCategoriesListByCategoryId(categoryId);   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryController.GetAllSubCategoriesByCategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deleta uma subCategoria de acordo com o id informado.
        /// </summary>
        /// <param name="subCategoryId">Id da SubCategoria</param>
        /// <returns></returns>
        [HttpDelete("{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategoryById([FromRoute] Guid subCategoryId)
        {
            try
            {
                var result = await _subCategoryService.DeleteSubCategoryById(subCategoryId);

                if (result)
                    return Ok("SubCategoria deletada com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao deletar subCategoria." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(SubCategoryController.DeleteSubCategoryById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

    }
}