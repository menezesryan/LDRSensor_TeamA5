using LDRSensorA5.Models;
using System.IO.Ports;

namespace LDRSensorA5.Services
{
    public delegate T InteractWithFirmware<T>(SerialPort port);
    public class CommunicationService : ICommunicationService
    {
        private SerialPort serialPort { get; set; }
        

        public ResponseModel Connect(ConnectionParameters parameters)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //do something
                //Ryan you really have to do something..!
                // :( 

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

                model.IsSucess = true;
                model.Message = "Port closed successfully";
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }

            return model;
        }

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
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                Monitor.Exit(this.serialPort);
            }
        }
    }

    

}
  