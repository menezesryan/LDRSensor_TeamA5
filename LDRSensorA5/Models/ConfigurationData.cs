namespace LDRSensorA5.Models
{
    /// <summary>
    /// This model is used as a template to store the configuration data into the configuration.json file.
    /// </summary>
    public class ConfigurationData
    {
        /// <value> <c>DefaultLowerThreshold</c> stores the default lower threshold for the current in mA</value>
        public double DefaultLowerThreshold { get; set; }
        /// <value> <c>DefaultUpperThreshold</c> stores the default upper threshold for the current in mA</value>
        public double DefaultUpperThreshold { get; set; }
        /// <value> <c>LowerThreshold</c> stores the lower threshold for the current in mA</value>
        public double LowerThreshold { get; set; }
        /// <value> <c>UpperThreshold</c> stores the upper threshold for the current in mA</value>
        public double UpperThreshold { get; set; } 
    }
}
