using LDRSensorA5.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Globalization;
using System.IO.Ports;

namespace LDRSensorA5.Services
{
    /// <summary>
    /// <c>ICommunicationService</c> defines the interface for <c>CommunicationService</c> It is injected in services or modules
    /// that need to use the functions defined in this interface. It is responsible for establishing the port connection, disconnection
    /// and managing the use of the port.
    /// </summary>
    public interface ICommunicationService
    {
        /// <summary>
        /// This function is used to create a port object. The port will be opened for use by services.
        /// </summary>
        /// <param name="parameters"><c>parameters</c> is of type <c>ConnectionParamters</c> <see cref="ConnectionParameters"/></param>
        /// <returns>Object of type <c>ResponseModel</c> It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        ResponseModel Connect(ConnectionParameters parameters);

        /// <summary>
        /// This function is used to disconnect from the port. The port object reference will be set to Null
        /// </summary>
        /// <param name="parameters"><c>parameters</c> is of type <c>ConnectionParamters</c> <see cref="ConnectionParameters"/></param>
        /// <returns>Object of type <c>ResponseModel</c> It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        ResponseModel Disconnect(ConnectionParameters parameters);

        /// <summary>
        /// This function is used to check whether the connection is established and the port is opened or not
        /// </summary>
        /// <returns>Boolean value. true if port is open and false if port is closed</returns>
        public bool IsConnected();
        /// <summary>
        /// <c>FirmwareDataExchange</c> function ensures that only one method is acessing a port at a time. It uses
        /// the Monitor class to make sure the port object is not being accessed by more than one method at a time.
        /// </summary>
        /// <typeparam name="T">Function takes a delegate type as parameter. Delegate takes Generic parameter type</typeparam>
        /// <param name="function">Function</param>
        /// <returns>generic type data</returns>
        public T FirmwareDataExchange<T>(InteractWithFirmware<T> function);
    }
}
