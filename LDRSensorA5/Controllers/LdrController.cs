using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LdrController : ControllerBase
    {
        public ILdrService _ldrService;
        public LdrController(ILdrService ldrService)
        {
            this._ldrService = ldrService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetLDRData()
        {
            try
            {
                var LdrData = _ldrService.GetLDRData();
                if(LdrData == null)
                {
                    return NotFound();
                }
                return Ok(LdrData);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }



        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveThreshold(LightThreshold threshold)
        {
            try
            {
                var model = _ldrService.SaveThresholdValues(threshold);
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ResetThreshold(String command)
        {
            try
            {
                var model = _ldrService.ResetThresholdValues(command);
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SetThreshold(LightThreshold threshold)
        {
            try
            {
                var model = _ldrService.SetThresholdValues(threshold);
                return Ok(model);

            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
