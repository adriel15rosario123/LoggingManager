using LoggingManagerCore.Ports.Primary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoggingManagerAPI.Controllers
{
    [Route("systems")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Get()
        {
            return Ok(_systemService.getAll());
        }
    }
}
