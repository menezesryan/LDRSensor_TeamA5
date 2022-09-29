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

        SerialPort serialPort { get; set; }
    }
}
