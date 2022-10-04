using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{

    /// <summary>
    /// <c>LdrController</c> contains all the api endpoints regarding the operation of LDR sensor and automatic mode.
    ///  Each function has an attribute that specifies which type of request does it handle.
    ///  The frontend will send a request to the appropriate api endpoint. It is derived from class
    /// <c>ControllerBase</c>
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class LdrController : ControllerBase
    {
        public ILdrService _ldrService;
        public ICommunicationService _communicationService;


        /// <summary>
        /// This is the constructor for <c>LdrController</c>. <see cref="ILdrService"/> is injected here.
        /// <see cref="ILdrService"/>
        /// </summary>
        /// <param name="ldrService">Object of type <see cref="ILdrService"/> that allows access to functions
        /// in <c>LdrService</c> <see cref="LdrService"/></param>
        /// <param name="communication">Object of type <c>ICommunicationService</c> that allows access to functions
        /// in <c>CommunicationService</c> <see cref="CommunicationService"/></param>
        public LdrController(ILdrService ldrService,ICommunicationService communication)
        {
            this._ldrService = ldrService;
            this._communicationService = communication;
        }


        /// <summary>
        /// When frontend requests for <c>LDRData</c>, it will send Get Request to this api endpoint.
        /// Here <c>GetLDRData</c> function is called which is defined in <see cref="LdrService"/>
        /// <see cref="LdrService"/>
        /// </summary>
        /// <returns><see cref="IActionResult"/> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetLDRData()
        {
            try
            {
                Console.WriteLine("Before getting ldr data");
                var LdrData = _ldrService.GetLDRData();
                Console.WriteLine("in controller "+LdrData.Current);
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


        /// <summary>
        /// When frontend requests for threshold data, it will send a Get request to this api endpoint.
        /// Here <c>GetThresholdValues</c> function is called which is defined in <see cref="LdrService"/>
        /// <see cref="LdrService"/>
        /// </summary>
        /// <returns><see cref="IActionResult"/> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
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


        /// <summary>
        /// When frontend requests for default threshold data, it will send a Get request to this api endpoint.
        /// Here <c>GetDefaultThresholdValues</c> function is called which is defined in <see cref="LdrService"/>
        /// 
        /// </summary>
        /// <returns><see cref="IActionResult"/> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
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



        /// <summary>
        /// When frontend sends a request to reset the threshold data, it will send a Post request to this api endpoint.
        /// Here <c>ResetThresholdValues</c> function is called which is defined in <see cref="LdrService"/>
        /// </summary>
        /// <param name="threshold">Object of type <see cref="LightThreshold"/>. It contains the upper 
        /// and lower threshold of light</param>
        /// <returns><see cref="IActionResult"/> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
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


        /// <summary>
        /// When frontend sends a request to set the threshold data, it will send a Post request to this api endpoint.
        /// Here <c>SetThresholdValues</c> function is called which is defined in <see cref="LdrService"/>
        /// </summary>
        /// <param name="threshold">>Object of type <see cref="LightThreshold"/>. It contains the upper 
        /// and lower threshold of light</param>
        /// <returns><see cref="IActionResult"/> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad</returns>
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




        /// <summary>
        /// When frontend sends a request to save the threshold data, it will send a Post request to this api endpoint.
        /// Here <c>SetThresholdValues</c> function is called which is defined in <see cref="LdrService"/>
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
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
