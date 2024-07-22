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

        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] UpdateSystemDto updateSystemDto)
        {
            updateSystemDto.SystemId = id;
            return Ok(_systemService.Update(updateSystemDto));
        }

        //[HttpGet("{id}/errors")]
        //[Authorize(Roles = "admin")]
        //public IActionResult GetErrors(int id) 
        //{
        //    return Ok(_systemService.GetErrorLogs(id));
        //}

        [HttpGet("{id}/errors")]
        [Authorize(Roles = "admin")]
        public IActionResult GetPaginatedErrors(int id, [FromQuery]int pageSize,[FromQuery]int pageNumber)
        {
            return Ok(_systemService.GetErrorLogs(new GetLogDto(id,pageSize,pageNumber)));
        }

        [HttpGet("{id}/trackings")]
        [Authorize(Roles = "admin")]
        public IActionResult GetPaginatedTrackings(int id, [FromQuery]int pageSize, [FromQuery]int pageNumber)
        {
            return Ok(_systemService.GetTrackingLogs(new GetLogDto(id, pageSize, pageNumber)));
        }
    }
}
