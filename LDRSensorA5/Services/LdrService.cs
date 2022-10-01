using LDRSensorA5.Models;
using Microsoft.Extensions.Configuration;
using System.IO.Ports;
using System.Text.Json;
using System.Xml;

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

                //transform light to current

                //send current data back

                ConfigurationData configuration = new ConfigurationData();
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);
                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                if (data.Lux > configuration.LowerThreshold && data.Lux<configuration.UpperThreshold)
                {
                    _communicationService.serialPort.WriteLine("lux-luxCurrent-" + data.Current+"\r");
                }
                else
                {
                    _communicationService.serialPort.WriteLine("lux-luxCurrent-4\r");
                }

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

                ConfigurationData configuration = new ConfigurationData();
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);

                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                configuration.UpperThreshold = configuration.DefaultUpperThreshold;
                configuration.LowerThreshold = configuration.DefaultLowerThreshold;

                var options = new JsonSerializerOptions { WriteIndented = true };
                jsonString = JsonSerializer.Serialize(configuration, options);
                File.WriteAllText(fileName, jsonString);
                _communicationService.serialPort.WriteLine("lux-luxConfig-"+ configuration.LowerThreshold + "-" + configuration.UpperThreshold + "\r");
                _communicationService.serialPort.WriteLine("lux-luxSave");

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

                //set the threshold for the graph conversion
                ConfigurationData configuration = new ConfigurationData();
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);

                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                configuration.UpperThreshold = threshold.UpperThreshold;
                configuration.LowerThreshold = threshold.LowerThreshold;

                var options = new JsonSerializerOptions { WriteIndented = true };
                jsonString = JsonSerializer.Serialize(configuration, options);
                File.WriteAllText(fileName, jsonString);

                _communicationService.serialPort.WriteLine("lux-luxConfig-" + configuration.LowerThreshold + "-" + configuration.UpperThreshold + "\r");

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
                _communicationService.serialPort.WriteLine("lux-luxSave");

                string fileName = "configuration.json";

                ConfigurationData configuration = new ConfigurationData();
                string jsonString = File.ReadAllText(fileName);

                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);
        
                configuration.UpperThreshold = threshold.UpperThreshold;
                configuration.LowerThreshold = threshold.LowerThreshold;

                var options = new JsonSerializerOptions { WriteIndented = true };
                jsonString = JsonSerializer.Serialize(configuration, options);
                File.WriteAllText(fileName, jsonString);

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

                //string fileName = "configuration.json";

                //ConfigurationData configuration = new ConfigurationData();
                //configuration.DefaultLowerThreshold = threshold.LowerThreshold;
                //configuration.DefaultUpperThreshold = threshold.UpperThreshold;
                //configuration.UpperThreshold = threshold.UpperThreshold;
                //configuration.LowerThreshold = threshold.LowerThreshold;

                //var options = new JsonSerializerOptions { WriteIndented = true };
                //string jsonString = JsonSerializer.Serialize(configuration,options);
                //File.WriteAllText(fileName, jsonString);

            }
            catch(Exception)
            {
                throw;
            }
            return threshold;
        }

        public LightThreshold GetDefaultThresholdValues()
        {
            LightThreshold threshold = new LightThreshold();
            try
            {
                //get default threshold values from json data
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);
                ConfigurationData configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);
                threshold.UpperThreshold = configuration.DefaultUpperThreshold;
                threshold.LowerThreshold = configuration.DefaultLowerThreshold;
            }
            catch (Exception)
            {
                throw;
            }
            return threshold;
        }
    }
}
