using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public interface IManualModeService
    {
        ResponseModel SetRelayState(bool relayState);
        ResponseModel SetCurrentValue(float current);
    }
}
