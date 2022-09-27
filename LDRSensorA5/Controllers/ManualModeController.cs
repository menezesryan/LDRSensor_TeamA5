using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManualModeController : ControllerBase
    {
        public IManualModeService _manualModeService;
        public ManualModeController(IManualModeService manualModeService)
        {
            _manualModeService = manualModeService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SetCurrentValue(float current)
        {
            try
            {
                //do something
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SetRelayState(bool relayState)
        {
            try
            {
                //do something
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
