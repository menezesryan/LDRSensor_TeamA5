using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace LDRSensorA5.Models
{
    public class ManualModeData
    {
        public double Current { get; set; }  
        public bool RelayState { get; set; }
    }
}
