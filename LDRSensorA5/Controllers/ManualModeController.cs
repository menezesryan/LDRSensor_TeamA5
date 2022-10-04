using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{

    /// <summary>
    /// <c>ManualModeController</c> contains all the api endpoints regarding the operation of hardware device in automatic mode.
    ///  Each function has an attribute that specifies which type of request does it handle.
    ///  The frontend will send a request to the appropriate api endpoint. It is derived from class
    /// <c>ControllerBase</c>
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class ManualModeController : ControllerBase
    {
        public IManualModeService _manualModeService;


        /// <summary>
        /// This is the controller for <c>ManualModeController</c>. <see cref="IManualModeService"/> is injected here.
        /// </summary>
        /// <param name="manualModeService">Object of type <c>IManualModeService</c> that allows access to functions
        /// in <c>ManualModeService</c> <see cref="ManualModeService"/></param>
        public ManualModeController(IManualModeService manualModeService)
        {
            _manualModeService = manualModeService;
        }



        /// <summary>
        /// In manual mode, the frontend will send a post request with the current data and relay state to this api end point.
        /// Here <c>SendCurrentAndRelayData</c> function is called which is defined in <see cref="ManualModeService"/>
        /// </summary>
        /// <param name="data">Object of type <see cref="ManualModeData"/> It will contain the current value and relay state</param>
        /// <returns><see cref="IActionResult"/> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>

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
