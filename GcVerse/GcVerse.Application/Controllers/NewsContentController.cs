using GcVerse.Infrastructure.Services.Content;
using GcVerse.Models.Content;
using GcVerse.Models.Request;
using GcVerse.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Conteúdo - Notícias 
    /// </summary>
    [ApiController]
    [Authorize]
    [Authorize(Roles = Permission.Administrator)]
    [Route("content/news")]
    public class NewsContentController : ControllerBase
    {
        private readonly ILogger<NewsContentController> _logger;
        private readonly INewsContentService _newsContentService;

        public NewsContentController(ILogger<NewsContentController> logger, 
                                     INewsContentService newsContentService)
        {
            _logger = logger;
            _newsContentService = newsContentService;
        }

        /// <summary>
        /// Cria um novo conteúdo do tipo notícia.
        /// </summary>
        /// <param name="upsertNewsContentRequest"></param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPost]
        public async Task<IActionResult> CreateNewsContent([FromBody] UpsertNewsContentRequest upsertNewsContentRequest)
        {
            try
            {
                var result = await _newsContentService.CreateNewsContent(upsertNewsContentRequest);

                if (result)
                    return Ok("Conteúdo criado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao criar uma novo conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentController.CreateNewsContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId"> Id do Conteúdo </param>
        /// <param name="upsertNewsContentRequest"></param>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateNewsContent([FromRoute] int contentId, [FromBody] UpsertNewsContentRequest upsertNewsContentRequest)
        {
            try
            {
                var result = await _newsContentService.UpdateNewsContent(contentId, upsertNewsContentRequest);

                if (result)
                    return Ok("Conteúdo atualizado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao atualizar conteúdo."});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentController.UpdateNewsContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Retorna um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId"> Id do Conteúdo</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [Authorize(Roles = $"{Permission.Administrator},{Permission.Basic}")]
        [HttpGet("{contentId}")]
        public async Task<NewsContent> GetNewsById([FromRoute] int contentId)
        {
            try
            {
               return await _newsContentService.GetNewsContentById(contentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentController.GetNewsById)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma lista de conteúdos de acordo com o id da subCategoria informada.
        /// </summary>
        /// <param name="subCategoryId">Id da SubCategoria</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [Authorize(Roles = $"{Permission.Administrator},{Permission.Basic}")]
        [HttpGet("subCategory/{subCategoryId}")]
        public async Task<List<NewsContent>> GetNewsBySubcategoryId([FromRoute] int subCategoryId)
        {
            try
            {
                return await _newsContentService.GetNewsContentsBySubCategoryId(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentController.GetNewsBySubcategoryId)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deleta um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId">Id do Conteúdo</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteNewsById([FromRoute] int contentId)
        {
            try
            {
                var result = await _newsContentService.DeleteNewsContentById(contentId);

                if (result)
                    return Ok("Conteúdo deletado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao deletar conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(NewsContentController.DeleteNewsById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }
    }
}