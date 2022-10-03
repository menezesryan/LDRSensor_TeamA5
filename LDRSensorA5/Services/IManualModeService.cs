using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    /// <summary>
    /// <c>IManualModeService</c> defines the interface for <c>ManualModeService</c> It is injected in modules which need
    /// to use the functions declared in this interface
    /// <see cref="ManualModeService"/>
    /// </summary>
    public interface IManualModeService
    {
        /// <summary>
        /// It sends the current and relay value to the hardware device via serial communication.
        /// </summary>
        /// <param name="manualModeData"> Parameter is of type <c>ManualModeData</c>. <see cref="ManualModeData"/> </param>
        /// <returns>Object of type ResponseModel. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message</returns>
        ResponseModel SendCurrentAndRelayData(ManualModeData manualModeData);
    }
}
