using LoggingManagerCore.Ports.Primary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingManagerAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{username}")]
        [Authorize(Roles = "admin,client")]
        public IActionResult Get(string username) { 
        
            return Ok(_userService.getByUsername(username));
        }
    }
}
