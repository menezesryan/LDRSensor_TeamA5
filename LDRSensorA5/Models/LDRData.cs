using System.ComponentModel.DataAnnotations;

namespace LDRSensorA5.Models
{
    public class LDRData
    {
        [Key]
        public DateTime TimeStamp { get; set; }
        public float Lux { get; set; }  
        public float Current { get; set; }
        
    }
}
