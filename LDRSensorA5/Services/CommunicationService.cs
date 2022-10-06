using LDRSensorA5.Models;
using System.IO.Ports;

namespace LDRSensorA5.Services
{

    public delegate T InteractWithFirmware<T>(SerialPort port);


    /// <summary>
    /// This service deals with the communication between software and hardware. Functions related
    /// to port are present
    /// </summary>
    public class CommunicationService : ICommunicationService
    {
        /// <value><c>SerialPort</c> object stores data like port name, baudrate, parity bit, start bit, stop bit </value>
        private SerialPort serialPort { get; set; }

        /// <value>
        /// <c>logger</c> object is used to log exceptional cases using serilog package
        /// </value>
        private readonly ILogger<CommunicationService> logger;

        public CommunicationService(ILogger<CommunicationService> logger)
        {
            this.serialPort = null;
            this.logger = logger;
        }


        /// <summary>
        /// It establishes connection with the hardware device. The SerialPort object is created with the required
        /// data and the port is opened.
        /// </summary>
        /// <param name="parameters"><c>parameters</c> is of type <see cref="ConnectionParameters"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message</returns>
        public ResponseModel Connect(ConnectionParameters parameters)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Console.WriteLine("portName is " + parameters.PortName);
                if (serialPort == null || !serialPort.IsOpen)
                {
                    
                    serialPort = new SerialPort(parameters.PortName, 9600, Parity.None, 8, StopBits.One);
                    serialPort.Open();
                    model.IsSucess = true;
                    model.Message = "Port opened";
                    logger.LogInformation("Connection established");
            }
                else
                {
                    model.IsSucess = false;
                    model.Message = "Port already open";
                    logger.LogInformation("Connection could not be established");  
                }
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
                logger.LogError(ex, "Connection could not be established");
            }

            return model;
        }


        /// <summary>
        /// This function is used to disconnect from the port. The port will be closed and the port object 
        /// reference will be set to Null.
        /// </summary>
        /// <param name="parameters"><c>parameters</c> is of type  <see cref="ConnectionParameters"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        public ResponseModel Disconnect(ConnectionParameters parameters)
        {
            ResponseModel model = new ResponseModel();

            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    model.IsSucess = true;
                    model.Message = "Port closed successfully";
                    logger.LogInformation("Port closed successfully");
                }
                else
                {
                    model.IsSucess = false;
                    model.Message = "Port already closed";
                    logger.LogInformation("Port already closed");
                }
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
                logger.LogError(ex, "Port could not be closed");
            }

            return model;
        }


        /// <summary>
        /// This function is used to check whether the connection is established and the port is opened or not
        /// </summary>
        /// <returns>Boolean value. true if port is open and false if port is closed</returns>
        public bool IsConnected()
        {
            bool isConnected = false;
            try
            {
                if(serialPort==null || !serialPort.IsOpen)
                {
                    isConnected = false;
                }
                else
                {
                    isConnected = true;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception occured while checking connection");
                throw;
            }

            return isConnected;
        }



        /// <summary>
        /// <c>FirmwareDataExchange</c> function ensures that only one method is acessing a port at a time. It uses
        /// the Monitor class to make sure the port object is not being accessed by more than one method at a time.
        /// </summary>
        /// <typeparam name="T">Function takes a delegate type as parameter. Delegate takes Generic parameter type</typeparam>
        /// <param name="function">Function</param>
        /// <returns>generic type data</returns>
        public T FirmwareDataExchange<T>(InteractWithFirmware<T> function)
        {
            try
            {
                Monitor.Enter(this.serialPort);
                try
                {
                    
                    var x = function(this.serialPort);
                    return x;
                }
                catch(Exception e)
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Firmware interaction failed");
                throw;
            }
            finally
            {
                Monitor.Exit(this.serialPort);
            }
        }


        /// <summary>
        /// This function is used to retrieve the ports available for connection
        /// </summary>
        /// <returns>Array of port names</returns>
        public string[] GetPortNamesList()
        {
            string[] ports;
            try
            {
                ports = SerialPort.GetPortNames();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Could not get port names");
                throw;
            }
            return ports;
        }
    }

    

}
  