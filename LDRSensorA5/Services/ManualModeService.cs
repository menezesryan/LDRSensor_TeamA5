using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    /// <summary>
    /// <c>ManualModeService</c> implements the interface <c>IManualModeService</c>. It contains function that sends
    /// the current and relay data from the applicaton to the hardware device via command in manual mode
    /// <see cref="IManualModeService"/>
    /// </summary>
    public class ManualModeService : IManualModeService
    {
        ICommunicationService _communicationService;
        private readonly ILogger<LdrService> logger;


        /// <summary>
        /// This is the constructor for class <c>ManualModeService</c>. Here the interface <c>ICommunicationService</c>
        /// is injected
        /// </summary>
        /// <param name="communication">interface object which allows this service to access the functions</param>
        public ManualModeService(ICommunicationService communication, ILogger<LdrService> logger)
        {
            _communicationService = communication;
            this.logger = logger;
        }


        /// <summary>
        /// It sends the current and relay value to the hardware device via serial communication using commands.
        /// </summary>
        /// <param name="manualModeData">Parameter is of type <c>ManualModeData</c>. <see cref="ManualModeData"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message</returns>
        public ResponseModel SendCurrentAndRelayData(ManualModeData manualModeData)
        {
            ResponseModel model = new ResponseModel();

            try
            {

                Console.WriteLine(manualModeData.ToString());
                string relaystate = (manualModeData.RelayState ? "on" : "off");
                Console.WriteLine("lux-luxManual-" + relaystate + "-" + manualModeData.Current + "\r");

                _communicationService.FirmwareDataExchange((port) =>
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.WriteLine("lux-luxManual-" + relaystate + "-" + manualModeData.Current + "\r");
                    return 1;
                });


       
                model.IsSucess = true;
                model.Message = "Current and relay data sent successfully";
            }
            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message ="Error" +  ex.Message;
                logger.LogError(ex,"Could not send data to firmware in manual mode");
            }
            finally
            {


            }
            return model;
        }
    }
}
