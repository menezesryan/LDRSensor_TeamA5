using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public class ManualModeService : IManualModeService
    {
        public ResponseModel SetCurrentValue(float current)
        {
            ResponseModel model = new ResponseModel();
            return model;
        }

        public ResponseModel SetRelayState(bool relayState)
        {
            ResponseModel model = new ResponseModel();
            return model;
        }
    }
}
