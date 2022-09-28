using LDRSensorA5.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Globalization;

namespace LDRSensorA5.Services
{
    public interface ICommunicationService
    {
        ResponseModel Connect(string command);
        ResponseModel Disconnect(string command);
    }
}
