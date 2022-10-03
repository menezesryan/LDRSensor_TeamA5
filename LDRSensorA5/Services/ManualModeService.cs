using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public class ManualModeService : IManualModeService
    {
        ICommunicationService _communicationService;
        public ManualModeService(ICommunicationService communication)
        {
            _communicationService = communication;
        }

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
            }
            finally
            {


            }
            return model;
        }
    }
}
