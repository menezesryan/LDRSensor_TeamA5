using LDRSensorA5.Models;
using System.IO.Ports;

namespace LDRSensorA5.Services
{
    public class LdrService : ILdrService
    {
        LdrDBContext _dbContext;
        ICommunicationService _communicationService;

        public LdrService(ICommunicationService communication, LdrDBContext dbContext)
        {
            _communicationService = communication;
            _dbContext = dbContext;
        }

        public LDRData GetLDRData()
        {
            LDRData data = new LDRData();
            try
            {
                //get data from the port   
                _communicationService.serialPort.WriteLine("lux-getLux\r");
                var command = _communicationService.serialPort.ReadLine();
                
                //Random random = new Random();
                //var x = random.Next(1, 20);
                data.Lux = Int32.Parse(command);
                data.Current = (float)(Int32.Parse(command) * 1.75);
                data.TimeStamp = DateTime.Now;

                //save data to database

                _dbContext.Add<LDRData>(data);
                _dbContext.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
            return data;          
        }

        public ResponseModel ResetThresholdValues(LightThreshold threshold)
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
                //update data
                //SerialPort port = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
                //port.Open();
                _communicationService.serialPort?.WriteLine("lux-luxSave-" + threshold.LowerThreshold + "-" + threshold.UpperThreshold + "\r");
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

        public ResponseModel SaveThresholdValues(LightThreshold threshold)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                _communicationService.serialPort.WriteLine("lux-luxSave-" + threshold.LowerThreshold + "-" + threshold.UpperThreshold + "\r");
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

        public LightThreshold GetThresholdValues()
        {
            LightThreshold threshold = new LightThreshold();
            try
            {
                _communicationService.serialPort.WriteLine("lux-luxRetrieve\r");
                var command = _communicationService.serialPort.ReadLine();
                var values = command.Split("-");
                threshold.LowerThreshold = Convert.ToDouble(values[0]);
                threshold.UpperThreshold = Convert.ToDouble(values[1]);
            }
            catch(Exception)
            {
                throw;
            }
            return threshold;
        }
    }
}
