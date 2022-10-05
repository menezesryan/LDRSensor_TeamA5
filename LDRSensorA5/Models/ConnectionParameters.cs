namespace LDRSensorA5.Models
{
    /// <summary>
    /// This model is used as a template for the connection parameters object. Currently the object will store static/dummy values.
    /// </summary>
   
    public class ConnectionParameters
    {
        /// <value>
        /// Property <c>PortName</c> stores the port with which the device is connected to
        /// </value>
        public string PortName { get; set; }

        /// <value> Property <c>BaudRate</c> stores the Baud Rate for communication with Hardware device</value>
        public int BaudRate { get; set; }
        /// <value> Property <c>DataBit</c> stores the Data Bit for communication with Hardware device</value>
        public int DataBit { get; set; }
        /// <value> Property <c>StartBit</c> stores the Start Bit for communication with Hardware device</value>
        public int StartBit { get; set; }
        /// <value> Property <c>StopBit</c> stores the Stop Bit for communication with Hardware device</value>
        public int StopBit { get; set; }
        /// <value> Property <c>ParityBit</c> stores the Parity Bit for communication with Hardware device</value>
        public int ParityBit { get; set; }
    }
}
