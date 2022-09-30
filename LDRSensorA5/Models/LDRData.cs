using System.ComponentModel.DataAnnotations;

namespace LDRSensorA5.Models
{
    public class LDRData
    {
        [Key]
        public DateTime TimeStamp { get; set; }
        public double Lux { get; set; }  
        public double Current { get; set; }
        
    }
}
