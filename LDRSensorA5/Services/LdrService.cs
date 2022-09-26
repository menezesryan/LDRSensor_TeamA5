using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public class LdrService : ILdrService
    {
        public ResponseModel ResetThresholdValues(string command)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //get default data from JSON file or MCU
                //set the threshold value here

                model.IsSucess = true;
                model.Message = "Threshold Value reset successful";
            }

            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }

            return model;
        }

        public ResponseModel SetThresholdValues(LightThreshold threshold)
        {
            ResponseModel model = new ResponseModel();

            try
            {
                //send data to the MCU
                //update data in the json file

                model.IsSucess = true;
                model.Message = "Threshold values updated";
            }
            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }

            return model;
        }
    }
}
