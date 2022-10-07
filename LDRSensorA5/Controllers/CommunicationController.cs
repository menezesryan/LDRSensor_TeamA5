using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LDRSensorA5.Controllers
{

    /// <summary>
    /// <c>CommunicationController</c> contains all the api endpoints regarding port connection that the
    /// frontend/browser sends a request to.
    /// Each function has an attribute that specifies which type of request does it handle. It is derived from class
    /// <c>ControllerBase</c>
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        public ICommunicationService _communicationService;


        /// <summary>
        /// This is the constructor for <c>CommunicationController</c>. <c>ICommunicationService</c> is injected here.
        /// <see cref="ICommunicationService"/>
        /// </summary>
        /// <param name="communicationService"></param>
        public CommunicationController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }



        /// <summary>
        /// When user wants to establish connection with the hardware device, the frontend sends a post request to this 
        /// api endpoint. Here the <c>Connect</c> function is called that is defined in <c>CommunicationService</c>
        /// <see cref="CommunicationService"/>
        /// </summary>
        /// <param name="parameters">Connection parameters of type <c>ConnectionParameters</c> is received from
        /// the frontend</param>
        /// <returns><c>IActionResult</c> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Connect(ConnectionParameters parameters)
        {
            try
            {
                Console.WriteLine("connect controller");
                var model = _communicationService.Connect(parameters);
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }



        /// <summary>
        /// The frontend will send a post request with the connection parameters when user wants to disconnect from the 
        /// hardware device.  Here the <c>Disconnect</c> function is called that is defined in <c>CommunicationService</c>
        /// </summary>
        /// <param name="parameters">Connection parameters of type <c>ConnectionParameters</c> is received from
        /// the frontend</param>
        /// <returns><c>IActionResult</c> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
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


        /// <summary>
        /// The frontend will send a get request when it wants to know whether connection with hardware device is
        /// established or disconnected
        /// </summary>
        /// <returns><c>IActionResult</c> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>

        [HttpGet]
        [Route("[action]")]
        public IActionResult IsConnected()
        {
            try
            {
                bool isConnected = _communicationService.IsConnected();
                return Ok(isConnected);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// This is the api endpoint for the backend. frontend will send get request to retrieve
        /// the  available port names
        /// </summary>
        /// <returns><c>IActionResult</c> represents the response that this api endpoint will give when a request
        /// is made. will indicate whether the request is successful or bad.</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetPortNames()
        {
            try
            {
                string[] ports = _communicationService.GetPortNamesList();
                return Ok(ports);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

    }
}
