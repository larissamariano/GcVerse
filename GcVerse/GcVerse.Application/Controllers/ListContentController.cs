using GcVerse.Infrastructure.Services.Category;
using GcVerse.Infrastructure.Services.Content;
using GcVerse.Models.Content;
using GcVerse.Models.Request;
using GcVerse.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Conteúdo - Listas
    /// </summary>
    [ApiController]
    [Authorize]
    [Authorize(Roles = Permission.Administrator)]
    [Route("content/list")]
    public class ListContentController : ControllerBase
    {
        private readonly ILogger<ListContentController> _logger;
        private readonly IListContentService _listContentService;


        public ListContentController(ILogger<ListContentController> logger,
                                     IListContentService listContentService)
        {
            _logger = logger;
            _listContentService = listContentService;
        }

        /// <summary>
        /// Cria um novo conteúdo do tipo lista.
        /// </summary>
        /// <param name="upsertListContentRequest"></param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPost]
        public async Task<IActionResult> CreateListContent([FromBody] UpsertListContentRequest upsertListContentRequest)
        {
            try
            {
                var result = await _listContentService.CreateListContent(upsertListContentRequest);

                if (result)
                    return Ok("Conteúdo criado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao criar uma novo conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentController.CreateListContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um conteúdo de acordo com o id informado.
        /// </summary>
        /// <param name="contentId"> Id do Conteúdo </param>
        /// <param name="upsertListContentRequest"></param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateListContent([FromRoute] int contentId, [FromBody] UpsertListContentRequest upsertListContentRequest)
        {
            try
            {
                var result = await _listContentService.UpdateListContent(contentId, upsertListContentRequest);

                if (result)
                    return Ok("Conteúdo atualizado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao atualizar conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentController.UpdateListContent)} - Error: " + ex.Message);
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
        public async Task<ListContent> GetListById([FromRoute] int contentId)
        {
            try
            {
                return await _listContentService.GetListContentById(contentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentController.GetListById)} - Error: " + ex.Message);
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
        public async Task<List<ListContent>> GetListBySubcategoryId([FromRoute] int subCategoryId)
        {
            try
            {
                return await _listContentService.GetListContentsBySubCategoryId(subCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentController.GetListBySubcategoryId)} - Error: " + ex.Message);
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
        public async Task<IActionResult> DeleteListById([FromRoute] int contentId)
        {
            try
            {
                var result = await _listContentService.DeleteListContentById(contentId);

                if (result)
                    return Ok("Conteúdo deletado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao deletar conteúdo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ListContentController.DeleteListById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }
    }
}