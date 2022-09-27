using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public interface IConnectionService
    {
        ResponseModel Connect(string command);
        ResponseModel Disconnect(string command);
    }
}
