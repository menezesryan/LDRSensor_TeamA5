using LDRSensorA5.Models;
using System.IO.Ports;

namespace LDRSensorA5.Services
{

    public delegate T InteractWithFirmware<T>(SerialPort port);
    public class CommunicationService : ICommunicationService
    {
        /// <value><c>SerialPort</c> object stores data like port name, baudrate, parity bit, start bit, stop bit </value>
        private SerialPort serialPort { get; set; }


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

                if (serialPort == null || !serialPort.IsOpen)
                {

                    serialPort = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
                    serialPort.Open();
                    model.IsSucess = true;
                    model.Message = "Port opened";
            }
                else
                {
                    model.IsSucess = false;
                    model.Message = "Port already open";
                }
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
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
                }
                else
                {
                    model.IsSucess = false;
                    model.Message = "Port already closed";
                }
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
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
            catch(Exception)
            {
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
            catch(Exception)
            {
                throw;
            }
            finally
            {
                Monitor.Exit(this.serialPort);
            }
        }

        public string[] GetPortNamesList()
        {
            string[] ports;
            try
            {
                ports = SerialPort.GetPortNames();
            }
            catch(Exception)
            {
                throw;
            }
            return ports;
        }
    }

    

}
  