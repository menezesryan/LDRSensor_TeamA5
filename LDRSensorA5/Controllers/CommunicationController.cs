using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        public ICommunicationService _connectionService;

        public CommunicationController(ICommunicationService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Connect(string command)
        {
            try
            {
                var model = _connectionService.Connect(command);
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Disconnect(string command)
        {
            try
            {
                var model = _connectionService.Disconnect(command);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
