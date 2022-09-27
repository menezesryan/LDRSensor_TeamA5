using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public class ManualModeService : IManualModeService
    {
        public ResponseModel SetCurrentValue(float current)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //do something

                model.IsSucess = true;
                model.Message = "Threshold Value reset successful";
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }
            return model;
        }

        public ResponseModel SetRelayState(bool relayState)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //do something

                model.IsSucess = true;
                model.Message = "Threshold Value reset successful";
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }
            return model;
        }
    }
}
