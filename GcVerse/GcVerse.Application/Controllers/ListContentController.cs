using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GcVerse.Application.Controllers
{
    [ApiController]
    [Route("content/list")]
    public class ListContentController : ControllerBase
    {
        private readonly ILogger<ListContentController> _logger;
        // private readonly IUserService _userService;

        public ListContentController(ILogger<ListContentController> logger)
                                  // IUserService userService)
        {
            _logger = logger;
            //_userService = userService;
        }

        [HttpPost]        
        public async Task<IActionResult> CreateListContent([FromBody] UpsertListContentRequest upsertListContentRequest)
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
                _logger.LogError(ex, $"{nameof(ListContentController.CreateListContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateListContent([FromRoute] Guid contentId, [FromBody] UpsertListContentRequest upsertListContentRequest)
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
                _logger.LogError(ex, $"{nameof(ListContentController.UpdateListContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetListById([FromRoute] Guid contentId)
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
                _logger.LogError(ex, $"{nameof(ListContentController.GetListById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("{subCategoryId}")]
        public async Task<IActionResult> GetListBySubcategoryId([FromRoute] Guid subCategoryId)
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
                _logger.LogError(ex, $"{nameof(ListContentController.GetListBySubcategoryId)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }


        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteListById([FromRoute] Guid contentId)
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
                _logger.LogError(ex, $"{nameof(ListContentController.DeleteListById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

    }
}
