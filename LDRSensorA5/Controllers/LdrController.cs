using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LdrController : ControllerBase
    {
        public ILdrService _ldrService;
        public LdrController(ILdrService ldrService)
        {
            this._ldrService = ldrService;
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateThreshold(LightThreshold threshold)
        {
            try
            {
                //do something
                return Ok();
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
                //do something
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
