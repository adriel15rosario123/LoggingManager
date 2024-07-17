using LoggingManagerCore.Dtos;
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
        public IActionResult Get()
        {
            return Ok(_systemService.getAll());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create(CreateSystemDto createSystemDto)
        {
            return Ok(_systemService.Create(createSystemDto));
        }
    }
}
