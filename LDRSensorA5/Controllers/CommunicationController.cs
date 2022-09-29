using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        public ICommunicationService _communicationService;

        public CommunicationController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Connect(ConnectionParameters parameters)
        {
            try
            {
                var model = _communicationService.Connect(parameters);
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Disconnect(ConnectionParameters parameters)
        {
            try
            {
                var model = _communicationService.Disconnect(parameters);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
