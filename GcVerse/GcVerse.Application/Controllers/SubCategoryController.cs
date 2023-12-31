﻿using GcVerse.Infrastructure.Services.Category;
using GcVerse.Models.Category;
using GcVerse.Models.Request;
using GcVerse.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// SubCategorias
    /// </summary>
    [ApiController]
    [Authorize]
    [Authorize(Roles = Permission.Administrator)]
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPut("{subCategoryId}")]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] int subCategoryId, [FromBody] UpsertSubCategoryRequest upsertSubCategoryRequest)
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [Authorize(Roles = $"{Permission.Administrator},{Permission.Basic}")]
        [HttpGet("{subCategoryId}")]
        public async Task<SubCategory> GetSubCategoryById([FromRoute] int subCategoryId)
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [Authorize(Roles = $"{Permission.Administrator},{Permission.Basic}")]
        [HttpGet("{categoryId}/all")]
        public async Task<List<SubCategory>> GetAllSubCategoriesByCategoryId([FromRoute] int categoryId)
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
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpDelete("{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategoryById([FromRoute] int subCategoryId)
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