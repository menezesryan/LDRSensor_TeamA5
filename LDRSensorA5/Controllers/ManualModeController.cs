using LDRSensorA5.Models;
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
        public IActionResult SendCurrentAndRelayData(ManualModeData data)
        {
            try
            {
                var model = _manualModeService.SendCurrentAndRelayData(data);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
