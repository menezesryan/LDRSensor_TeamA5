namespace LDRSensorA5.Models
{
    /// <summary>
    /// <c>LightThreshold</c> is used to model the upper and lower threshold of light
    /// </summary>
    public class LightThreshold
    {
        ///<value><c>LowerThreshold</c> stores the lower threshold of light</value>
        public double LowerThreshold { get; set; }

        ///<value><c>UpperThreshold</c> stores the upper threshold of light</value>
        public double UpperThreshold { get; set; }
    }
}
