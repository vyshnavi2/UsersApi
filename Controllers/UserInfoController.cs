using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _infoService;
        private readonly ILogger<UserInfoController> _logger;

        public UserInfoController(IUserInfoService infoService, ILogger<UserInfoController> logger)
        {
            _infoService = infoService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveActivities([FromBody] List<UserInfo> info)
        {
            if (info == null || info.Count == 0)
                return BadRequest("No user data received.");

            try
            {
                await _infoService.SaveToFileAsync(info);
                return Ok("user info saved successfully.");
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Handled exception occurred in controller.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
