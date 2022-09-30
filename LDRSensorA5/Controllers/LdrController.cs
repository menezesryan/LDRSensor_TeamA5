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
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetThreshold()
        {
            try
            {
                LightThreshold threshold = _ldrService.GetThresholdValues();
                if (threshold == null)
                {
                    return NotFound();
                }
                return Ok(threshold);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetDefaultThreshold()
        {
            try
            {
                LightThreshold threshold = _ldrService.GetDefaultThresholdValues();
                if (threshold == null)
                {
                    return NotFound();
                }
                return Ok(threshold);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult ResetThreshold(LightThreshold threshold)
        {
            try
            {
                var model = _ldrService.ResetThresholdValues(threshold);
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

        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveThreshold(LightThreshold threshold)
        {
            try
            {
                var model = _ldrService.SaveThresholdValues(threshold);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
