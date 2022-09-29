using LDRSensorA5.Models;
using System.IO.Ports;

namespace LDRSensorA5.Services
{
    public class LdrService : ILdrService
    {
        LdrDBContext _dbContext;

        public LdrService(LdrDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LDRData GetLDRData()
        {
            LDRData data = new LDRData();
            try
            {
                //get data from the port  
                SerialPort port = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
                port.Open();
                port.WriteLine("lux-getLux\r");
                var command = port.ReadLine();
                port.Close();

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
                //update data

                SerialPort port = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
                port.Open();
                port.WriteLine("lux-luxSave- + " + threshold.LowerThreshold + "-" + threshold.UpperThreshold + "\r");
                port.Close();

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
