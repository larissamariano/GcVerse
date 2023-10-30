using GcVerse.Infrastructure.Services.User;
using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace GcVerse.Application.Controllers
{
    /// <summary>
    /// Usuários
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger,
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
        /// <response code="200">Requisição realizada com Sucesso.</response>
        /// <response code="400">Erro ao realizar requisição.</response>
        /// <response code="500">Erro na aplicação.</response>
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
    }
}
