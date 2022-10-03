using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    /// <summary>
    /// <c>Set  </c>
    /// </summary>
    public interface ILdrService
    {
        ResponseModel SetThresholdValues(LightThreshold threshold);
        ResponseModel ResetThresholdValues(LightThreshold threshold);
        LDRData GetLDRData();
        ResponseModel SaveThresholdValues(LightThreshold threshold);
        LightThreshold GetThresholdValues();
        LightThreshold GetDefaultThresholdValues();
    }
}
