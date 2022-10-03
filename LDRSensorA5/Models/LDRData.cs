using System.ComponentModel.DataAnnotations;

namespace LDRSensorA5.Models
{
    /// <summary>
    /// <c>LDRData</c> model is used to store LDR data into database. It is also used to send LDR data from backend to the front end.
    /// Primarily used to send data to the graph component in front end.
    /// </summary>
    public class LDRData
    {
        /// <value><c>TimeStamp</c>: Contains time at which lux was measured. It is the primary key for the database</value>
        [Key]
        public DateTime TimeStamp { get; set; }
        /// <value><c>Lux</c>: Stores incoming lux values from firmware</value>
        public double Lux { get; set; }
        /// <value> <c>Current</c>: Stores the current that was converted from Lux </value>
        public double Current { get; set; }
        
    }
}
