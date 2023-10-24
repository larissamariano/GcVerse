using GcVerse.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GcVerse.Application.Controllers
{
    [ApiController]
    [Route("content/news")]
    public class NewsContentController : ControllerBase
    {
        private readonly ILogger<NewsContentController> _logger;
        // private readonly IUserService _userService;

        public NewsContentController(ILogger<NewsContentController> logger)
                                  // IUserService userService)
        {
            _logger = logger;
            //_userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsContent([FromBody] UpsertNewsContentRequest upsertNewsContentRequest)
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
                _logger.LogError(ex, $"{nameof(NewsContentController.CreateNewsContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateNewsContent([FromRoute] Guid contentId, [FromBody] UpsertNewsContentRequest upsertNewsContentRequest)
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
                _logger.LogError(ex, $"{nameof(NewsContentController.UpdateNewsContent)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetNewsById([FromRoute] Guid contentId)
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
                _logger.LogError(ex, $"{nameof(NewsContentController.GetNewsById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

        [HttpGet("{subCategoryId}")]
        public async Task<IActionResult> GetNewsBySubcategoryId([FromRoute] Guid subCategoryId)
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
                _logger.LogError(ex, $"{nameof(NewsContentController.GetNewsBySubcategoryId)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }


        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteNewsById([FromRoute] Guid contentId)
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
                _logger.LogError(ex, $"{nameof(NewsContentController.DeleteNewsById)} - Error: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ResultError = ex.Message });
            }
        }

    }
}
