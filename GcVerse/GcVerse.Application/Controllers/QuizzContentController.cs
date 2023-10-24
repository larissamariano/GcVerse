using GcVerse.Infrastructure.Services.Content;
using GcVerse.Models.Content;
using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Conteúdo - Quizz
    /// </summary>
    [ApiController]
    [Route("content/quizz")]
    public class QuizzContentController : ControllerBase
    {
        private readonly ILogger<QuizzContentController> _logger;
        private readonly IQuizzContentService _quizzContentService;


        public QuizzContentController(ILogger<QuizzContentController> logger,
                                      IQuizzContentService quizzContentService)
        {
            _logger = logger;
            _quizzContentService = quizzContentService;
        }

        /// <summary>
        /// Cria um novo conteúdo do tipo quizz.
        /// </summary>
        /// <param name="upsertQuizzContentRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateQuizzContent([FromBody] UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            try
            {
                var result = await _quizzContentService.CreateQuizzContent(upsertQuizzContentRequest);

                if (result)
                    return Ok("Conteúdo criado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao criar uma novo conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentController.CreateQuizzContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId"> Id do Conteúdo </param>
        /// <param name="upsertQuizzContentRequest"></param>
        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateQuizzContent([FromRoute] Guid contentId, [FromBody] UpsertQuizzContentRequest upsertQuizzContentRequest)
        {
            try
            {
                var result = await _quizzContentService.UpdateQuizzContent(contentId, upsertQuizzContentRequest);

                if (result)
                    return Ok("Conteúdo atualizado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao atualizar conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentController.UpdateQuizzContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Retorna um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId"> Id do Conteúdo</param>
        /// <returns></returns>
        [HttpGet("{contentId}")]
        public async Task<QuizzContent> GetQuizzById([FromRoute] Guid contentId)
        {
            try
            {
                return await _quizzContentService.GetQuizzContentById(contentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentController.GetQuizzById)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma lista de conteúdos de acordo com o id da subCategoria informada.
        /// </summary>
        /// <param name="subCategoryId">Id da SubCategoria</param>
        /// <retuns></retuns>
        [HttpGet("{subCategoryId}")]
        public async Task<List<QuizzContent>> GetQuizzBySubcategoryId([FromRoute] Guid subCategoryId)
        {
            try
            {
                return await _quizzContentService.GetQuizzContentsBySubCategoryId(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentController.GetQuizzBySubcategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deleta um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId">Id do Conteúdo</param>
        /// <returns></returns>
        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteQuizzById([FromRoute] Guid contentId)
        {
            try
            {
                var result = await _quizzContentService.DeleteQuizzContentById(contentId);

                if (result)
                    return Ok("Conteúdo deletado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao deletar conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(QuizzContentController.DeleteQuizzById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }
    }
}