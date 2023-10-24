using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GcVerse.Application.Controllers
{
    [ApiController]
    [Route("subCategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> _logger;
        // private readonly IUserService _userService;

        public SubCategoryController(ILogger<SubCategoryController> logger)
                                  // IUserService userService)
        {
            _logger = logger;
            //_userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] UpsertSubCategoryRequest upsertSubCategoryRequest)
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
                _logger.LogError(ex, $"{nameof(SubCategoryController.CreateSubCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] UpsertSubCategoryRequest upsertSubCategoryRequest)
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
                _logger.LogError(ex, $"{nameof(SubCategoryController.UpdateSubCategory)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("all/{categoryId}")]
        public async Task<IActionResult> GetAllSubCategories([FromRoute] Guid categoryId)
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
                _logger.LogError(ex, $"{nameof(SubCategoryController.GetAllSubCategories)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById([FromRoute] Guid subCategoryId)
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
                _logger.LogError(ex, $"{nameof(SubCategoryController.GetSubCategoryById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryById([FromRoute] Guid categoryId)
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
                _logger.LogError(ex, $"{nameof(SubCategoryController.DeleteSubCategoryById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

    }
}
