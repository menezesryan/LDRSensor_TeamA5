using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace LDRSensorA5.Models
{
    /// <summary>
    /// <c>ManualModeData</c> is used to model the manual mode data that is received from the frontend
    /// </summary>
    public class ManualModeData
    {
        /// <value><c>Current</c> Stores the current value received from the front end in manual mode</value>
        public double Current { get; set; }


        /// <value><c>RelayState</c> Stores the relay state received from the front end in manual mode. It stores boolean data</value>
        public bool RelayState { get; set; }
    }
}
