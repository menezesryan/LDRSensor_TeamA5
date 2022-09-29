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
                _communicationService.serialPort.WriteLine("lux-manualModeData-" + manualModeData.Current+ "-" + manualModeData.RelayState);
                model.IsSucess = true;
                model.Message = "Current and relay data sent successfully";
            }
            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message ="Error" +  ex.Message;
            }
            return model;
        }

        //public ResponseModel SetCurrentValue(float current)
        //{
        //    ResponseModel model = new ResponseModel();
        //    try
        //    {
        //        //do something
        //        Console.WriteLine("Current: " + current);
        //        _communicationService.serialPort.WriteLine("lux-setCurrent-"+current);
        //        model.IsSucess = true;
        //        model.Message = "Current set successful";
        //    }

        //    catch (Exception ex)
        //    {
        //        model.IsSucess = false;
        //        model.Message = "Error: " + ex.Message;
        //    }
        //    return model;
        //}

        //public ResponseModel SetRelayState(bool relayState)
        //{
        //    ResponseModel model = new ResponseModel();
        //    try
        //    {
        //        //do something
        //        Console.WriteLine("relay: " + relayState);
        //        _communicationService.serialPort.WriteLine("lux-setRelayState-" + relayState);
        //        model.IsSucess = true;
        //        model.Message = "Relay state changed sucessfully";
        //    }

        //    catch (Exception ex)
        //    {
        //        model.IsSucess = false;
        //        model.Message = "Error: " + ex.Message;
        //    }
        //    return model;
        //}
    }
}
