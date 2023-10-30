using GcVerse.Infrastructure.Services.Category;
using GcVerse.Infrastructure.Services.User;
using GcVerse.Models.Category;
using GcVerse.Models.Request;
using GcVerse.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Usuários
    /// </summary>
    [ApiController]
    [Authorize]
    [Authorize(Roles = Permission.Administrator)]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,
                              IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Loga um usuário no sistema.
        /// </summary>
        /// <param name="upsertBaseUserRequest"></param>        
        /// <param name="userPermission"> Tipo de Permissão do Usuário</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var result = await _userService.Login(loginRequest);

                if (result.Success)
                    return Ok(result);
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao autenticar usuário." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController.CreateUser)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }


        /// <summary>
        /// Cria uma novo usuario.
        /// </summary>
        /// <param name="upsertBaseUserRequest"></param>        
        /// <param name="userPermission"> Tipo de Permissão do Usuário</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromQuery] UserPermission userPermission, [FromBody] UpsertBaseUserRequest upsertBaseUserRequest)
        {
            try
            {
                var result = await _userService.CreateUser(upsertBaseUserRequest, userPermission);

                if (result)
                    return Ok("Usuário criado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao criar uma novo usuário." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController.CreateUser)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um usuário de acordo com o id informado.
        /// </summary>
        /// <param name="userId">Id do Usuário</param>
        /// <param name="userPermission"> Tipo de Permissão do Usuário</param>
        /// <param name="upsertBaseUserRequest"></param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromQuery] UserPermission userPermission, [FromBody] UpsertBaseUserRequest upsertBaseUserRequest)
        {
            try
            {
                var result = await _userService.UpdateUser(userId, upsertBaseUserRequest, userPermission);

                if (result)
                    return Ok("Usuário atualizado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao atualizar usuário." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController.UpdateUser)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        /// <summary>
        /// Retorna um usuário de acordo com o id informado.
        /// </summary>
        /// <param name="userId"> Id do Usuário</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpGet("{userId}")]
        public async Task<BaseUser> GetUserById([FromRoute] int userId)
        {
            try
            {
                return await _userService.GetUserById(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController.GetUserById)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma lista com todos os usuários de acordo com o nível de permissão.
        /// </summary>
        /// <param name="userPermission">Tipo de Permissão do Usuário.</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpGet("all")]
        public async Task<List<BaseUser>> GetUsers([FromQuery] UserPermission userPermission)
        {
            try
            {
                return await _userService.GetUsersByPermission(userPermission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController.GetUsers)} - Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deleta um usuário de acordo com o id informado.
        /// </summary>
        /// <param name="userId">Id do Usuário</param>
        /// <returns></returns>
        /// <response code="200">Requisição Realizada com Sucesso.</response>
        /// <response code="401">Requisição Não Autorizada. Token Inválido!</response>
        /// <response code="403">Requisição Proibida. Usuário sem permissão para executar essa ação!</response>
        /// <response code="400">Erro ao Realizar Requisição.</response>
        /// <response code="500">Erro na Aplicação.</response>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);

                if (result)
                    return Ok("Usuário deletado com sucesso.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Erro ao deletar usuário." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController.DeleteUserById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }
    }
}