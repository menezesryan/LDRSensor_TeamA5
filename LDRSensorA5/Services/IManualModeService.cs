using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public interface IManualModeService
    {
        ResponseModel SendCurrentAndRelayData(ManualModeData manualModeData);
    }
}
