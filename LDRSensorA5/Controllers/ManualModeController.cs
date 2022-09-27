using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{
    [Route("[controller]")]
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
                var model = _manualModeService.SetCurrentValue(current);
                return Ok(model);
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
                var model = _manualModeService.SetRelayState(relayState);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
