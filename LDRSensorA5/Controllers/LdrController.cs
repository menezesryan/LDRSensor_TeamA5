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
        public ICommunicationService _communicationService;
        public LdrController(ILdrService ldrService,ICommunicationService communication)
        {
            this._ldrService = ldrService;
            this._communicationService = communication;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetLDRData()
        {
            try
            {
                var LdrData = _ldrService.GetLDRData();
                var response = _communicationService.Connect("hello");
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
