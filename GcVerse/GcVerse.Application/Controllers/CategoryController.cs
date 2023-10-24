using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GcVerse.Application.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        // private readonly IUserService _userService;

        public CategoryController(ILogger<CategoryController> logger)
                                  // IUserService userService)
        {
            _logger = logger;
            //_userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] UpsertCategoryRequest upsertCategoryRequest)
        {
            try
            {
                  //  if (result)
                        return Ok("Success adding a new user");
                 //   else
                        return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Error adding a new user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.CreateCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpsertCategoryRequest upsertCategoryRequest)
        {
            try
            {
                //  if (result)
                return Ok("Success adding a new user");
                //   else
                return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Error adding a new user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.UpdateCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                //  if (result)
                return Ok("Success adding a new user");
                //   else
                return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Error adding a new user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.GetAllCategories)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
        {
            try
            {
                //  if (result)
                return Ok("Success adding a new user");
                //   else
                return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Error adding a new user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.GetCategoryById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid categoryId)
        {
            try
            {
                //  if (result)
                return Ok("Success adding a new user");
                //   else
                return StatusCode(StatusCodes.Status400BadRequest, new { ResultError = "Error adding a new user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CategoryController.DeleteCategoryById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

    }
}
