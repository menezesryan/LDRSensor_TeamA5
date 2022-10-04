using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    /// <summary>
    /// <c>ILdrSerivce</c> defines the interface for <c>LdrService</c>. It is injected in modules which need
    /// to use the functions declared in this interface. The basic LDR functionality is declared in this interface
    /// <see cref="LdrService"/>
    /// </summary>
    public interface ILdrService
    {
        /// <summary>
        /// This function is called when the user sets the threshold in the application. A command is sent to the
        /// hardware device with the changed thresholds
        /// </summary>
        /// <param name="threshold">It is of type <c>LightThreshold</c> It contains
        /// the lower and upper threshold of light <see cref="LightThreshold"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        ResponseModel SetThresholdValues(LightThreshold threshold);


        /// <summary>
        /// This function is called when the user presses the reset threshold button. It will reset the threshold
        /// to default threshold values. It will also send a command to hardware device with the changed thresholds.
        /// It will save the thresholds to configuration.json file
        /// </summary>
        /// <param name="threshold">It is of type <c>LightThreshold</c> It contains
        /// the lower and upper threshold of light <see cref="LightThreshold"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        ResponseModel ResetThresholdValues(LightThreshold threshold);


        /// <summary>
        /// THis function is called every 1 second by the front end. It will request data from the hardware device,
        /// fetch the data from the port, save data to database and return data to the frontend
        /// </summary>
        /// <returns>Object of type <c>LDRData</c> It will contain the lux value, current and timestamp
        /// of when the lux value was recorded <see cref="LDRData"/></returns>
        LDRData GetLDRData();


        /// <summary>
        /// This function is called when the user pressses the save threshold button. It will send a command to
        /// the hardware device to save the threshold values in the EEPROM
        /// </summary>
        /// <param name="threshold">It is of type <c>LightThreshold</c> It contains
        /// the lower and upper threshold of light <see cref="LightThreshold"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        ResponseModel SaveThresholdValues(LightThreshold threshold);


        /// <summary>
        /// When the application is opened this is the first function that will be called by the frontend. It will
        /// request the hardware device for the threshold values that are saved in the EEPROM
        /// </summary>
        /// <returns>object of type <c>LightThreshold</c> It will contain the lower and upper light
        /// threshold that were stored in the EEPROM</returns>
        LightThreshold GetThresholdValues();


        /// <summary>
        /// When the user presses the reset button this function is called. This will get the default threshold values from
        /// configuration.json and send it back to the frontend
        /// </summary>
        /// <returns>object of type <c>LightThreshold</c> It will contain the lower and upper light
        /// threshold that were stored in the configuration.json file</returns>
        LightThreshold GetDefaultThresholdValues();
    }
}
