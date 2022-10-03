using LDRSensorA5.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Globalization;
using System.IO.Ports;

namespace LDRSensorA5.Services
{
    public interface ICommunicationService
    {
        ResponseModel Connect(ConnectionParameters parameters);
        ResponseModel Disconnect(ConnectionParameters parameters);
        public bool IsConnected();
        SerialPort serialPort { get; set; }
        public T FirmwareDataExchange<T>(InteractWithFirmware<T> function);
    }
}
