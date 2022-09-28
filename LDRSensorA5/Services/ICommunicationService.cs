using LDRSensorA5.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace LDRSensorA5.Services
{
    public interface ICommunicationService
    {
        ResponseModel Connect(string command);
        ResponseModel Disconnect(string command);
    }
}
