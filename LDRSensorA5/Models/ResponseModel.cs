namespace LDRSensorA5.Models
{
    /// <summary>
    /// <c>ResponseModel</c> is used to model the response for any http request from the frontend
    /// </summary>
    public class ResponseModel
    {
        /// <value><c>IsSuccess</c> stores a boolean value and indicates whether the request is successful or unsucessful </value>
        public bool IsSucess { get; set; }
        /// <value><c>Message</c> stores a success or failure message based on whether the request is sucessful or unsucessful </value>
        public string Message { get; set; }
    }
}
