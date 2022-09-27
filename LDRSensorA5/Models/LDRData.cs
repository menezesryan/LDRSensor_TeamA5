using System.ComponentModel.DataAnnotations;

namespace LDRSensorA5.Models
{
    public class LDRData
    {
        //public int Id { get; set; }
        public float Lux { get; set; }  
        public float Current { get; set; }
        [Key]
        public DateTime TimeStamp { get; set; }
    }
}
