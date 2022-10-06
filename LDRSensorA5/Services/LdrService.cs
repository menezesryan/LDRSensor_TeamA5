using LDRSensorA5.Models;
using Microsoft.Extensions.Configuration;
using System.IO.Ports;
using System.Text.Json;
using System.Xml;

namespace LDRSensorA5.Services
{

    /// <summary>
    /// <c>LdrService</c> implements the interface <c>ILdrService</c>. It contains functions that
    /// deal with setting and getting the light thresholds, getting the lux values and storing the data in database
    /// <see cref="ILdrService"/>
    /// </summary>
    public class LdrService : ILdrService
    {
        LdrDBContext _dbContext;
        ICommunicationService _communicationService;

        /// <value>
        /// <c>logger</c> object is used to log exceptional cases using serilog package
        /// </value>
        private readonly ILogger<LdrService> logger;

        /// <summary>
        /// This is the constructor for the class <c>LdrService</c>. Here the interface <c>ICommunicationService</c>
        /// ,the logger, and the database context <c>LdrDBContext</c> are injected.
        /// </summary>
        /// <param name="communication">interface object which allows this service to access the functions</param>
        /// <param name="dbContext">object of <c>LdrDBContext</c> allows to do CRUD functionality on database</param>
        public LdrService(ICommunicationService communication, LdrDBContext dbContext, ILogger<LdrService> logger)
        {
            _communicationService = communication;
            _dbContext = dbContext;
            this.logger = logger;
        }



        /// <summary>
        /// This function is called every 1 second by the front end. It will request data from the hardware device,
        /// fetch the data from the port, save data to database and return data to the frontend
        /// </summary>
        /// <returns>Object of type <c>LDRData</c> It will contain the lux value, current and timestamp
        /// of when the lux value was recorded <see cref="LDRData"/></returns>
        public LDRData GetLDRData()
        {
            LDRData data = new LDRData();
            try
            {
                //--------------------
                var ldrData = _communicationService.FirmwareDataExchange((port) =>
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.WriteLine("lux-getLux\r");
                    var command = port.ReadLine();
                   
                    return command;
                });


                Console.WriteLine(ldrData);
                data.Lux = Convert.ToDouble(ldrData);
                Console.WriteLine("Luxxx" + data.Lux);
                data.TimeStamp = DateTime.Now;

                ConfigurationData configuration = new ConfigurationData();
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);
                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                _communicationService.FirmwareDataExchange((port) =>
                {
                    if (data.Lux >= configuration.LowerThreshold && data.Lux <= configuration.UpperThreshold)
                    {
                        data.Current = ((data.Lux - configuration.LowerThreshold) / (configuration.UpperThreshold - configuration.LowerThreshold) * 16) + 4;
                        data.Current = Convert.ToDouble(data.Current.ToString("N2"));
                        port.WriteLine("lux-luxCurrent-" + data.Current.ToString("N2") + "\r");
                    }
                    else if(data.Lux < configuration.LowerThreshold)
                    {
                        data.Current = 4;
                        port.WriteLine("lux-luxCurrent-4\r");
                    }
                    else if(data.Lux > configuration.UpperThreshold)
                    {
                        data.Current = 20;
                        port.WriteLine("lux-luxCurrent-20\r");
                    }
                    return 1;
                });

                //save data to database

                //var first = _dbContext.LDRData.OrderBy(p => p.TimeStamp).FirstOrDefault();

                TimeSpan interval = new TimeSpan(0, 2, 0);
                DateTime initialTime = DateTime.Now;
                initialTime = initialTime.Subtract(interval);

                var oldValues = _dbContext.LDRData.Where(p => p.TimeStamp < initialTime).ToList();

                if(oldValues != null)
                {
                    _dbContext.LDRData.RemoveRange(oldValues);
                }

                _dbContext.Add<LDRData>(data);
                _dbContext.SaveChanges();  
            }
            catch(Exception ex)
            {
                logger.LogError(ex,"Could not get ldr data");
                throw;
            }
            return data;          
        }

        /// <summary>
        /// This function is called when the user presses the reset threshold button. It will reset the threshold
        /// to default threshold values. It will also send a command to hardware device with the changed thresholds.
        /// It will save the thresholds to configuration.json file
        /// </summary>
        /// <param name="threshold">It is of type <c>LightThreshold</c> It contains
        /// the lower and upper threshold of light <see cref="LightThreshold"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
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

                _communicationService.FirmwareDataExchange((port) =>
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.WriteLine("lux-luxConfig-" + configuration.LowerThreshold + "-" + configuration.UpperThreshold + "\r");
                    port.WriteLine("lux-luxSave\r");
                    return 1;
                });

               

                model.IsSucess = true;
                model.Message = "Threshold Value reset successful";

                logger.LogInformation("Threshold values resetted");
            }

            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
                logger.LogError(ex, "Could not reset threshold values");
            }

            return model;
        }


        /// <summary>
        /// This function is called when the user sets the threshold in the application. A command is sent to the
        /// hardware device with the changed thresholds
        /// </summary>
        /// <param name="threshold">It is of type <c>LightThreshold</c> It contains
        /// the lower and upper threshold of light <see cref="LightThreshold"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        public ResponseModel SetThresholdValues(LightThreshold threshold)
        {
            ResponseModel model = new ResponseModel();

            try
            {

                ConfigurationData configuration = new ConfigurationData();
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);

                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                configuration.UpperThreshold = threshold.UpperThreshold;
                configuration.LowerThreshold = threshold.LowerThreshold;

                var options = new JsonSerializerOptions { WriteIndented = true };
                jsonString = JsonSerializer.Serialize(configuration, options);
                File.WriteAllText(fileName, jsonString);

                _communicationService.FirmwareDataExchange((port) =>
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.WriteLine("lux-luxConfig-" + configuration.LowerThreshold + "-" + configuration.UpperThreshold + "\r");
                    return 1;
                });


                model.IsSucess = true;
                model.Message = "Threshold values updated";
                logger.LogInformation("Threshold values set");
            }

            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
                logger.LogError(ex, "Could not set threshold values");
            }
            finally
            {


            }
            return model;
        }


        /// <summary>
        /// This function is called when the user pressses the save threshold button. It will send a command to
        /// the hardware device to save the threshold values in the EEPROM
        /// </summary>
        /// <param name="threshold">It is of type <c>LightThreshold</c> It contains
        /// the lower and upper threshold of light <see cref="LightThreshold"/></param>
        /// <returns>Object of type <c>ResponseModel</c>. It will indicate whether http request is successful or
        /// unsuccessful in boolean form and also contain the success or failure message <see cref="ResponseModel"/></returns>
        public ResponseModel SaveThresholdValues(LightThreshold threshold)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                _communicationService.FirmwareDataExchange((port) =>
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.WriteLine("lux-luxSave\r");
                    return 1;
                });

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

                logger.LogInformation("Threshold values saved in EEPROM");
            }
            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
                logger.LogError(ex, "Could not save threshold values in EEPROM");
            }
            finally
            {
                

            }
            return model;
        }



        /// <summary>
        /// When the application is opened this is the first function that will be called by the frontend. It will
        /// request the hardware device for the threshold values that are saved in the EEPROM
        /// </summary>
        /// <returns>object of type <c>LightThreshold</c> It will contain the lower and upper light
        /// threshold that were stored in the EEPROM</returns>
        public LightThreshold GetThresholdValues()
        {
            LightThreshold threshold = new LightThreshold();
            try
            {
                var thresholdData = _communicationService.FirmwareDataExchange((port) =>
                {
                    port.ReadTimeout = 1000;
                    port.WriteTimeout = 1000;
                    port.WriteLine("lux-luxRetrieve\r");
                    var command = port.ReadLine();
                    return command;
                });

                Console.WriteLine(thresholdData);

                var values = thresholdData.Split("-");
                threshold.LowerThreshold = Convert.ToDouble(values[0]);
                threshold.UpperThreshold = Convert.ToDouble(values[1]);

                string fileName = "configuration.json";

                ConfigurationData configuration = new ConfigurationData();
                string jsonString = File.ReadAllText(fileName);

                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                configuration.UpperThreshold = threshold.UpperThreshold;
                configuration.LowerThreshold = threshold.LowerThreshold;

                var options = new JsonSerializerOptions { WriteIndented = true };
                jsonString = JsonSerializer.Serialize(configuration, options);
                File.WriteAllText(fileName, jsonString);

                _communicationService.FirmwareDataExchange((port) =>
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.WriteLine("lux-luxConfig-" + threshold.LowerThreshold + "-" + threshold.UpperThreshold + "\r");
                    return 1;

                });
                logger.LogInformation("Threshold values retrieved from EEPROM");
            }
            catch(Exception ex)
            {
                logger.LogError(ex,"Could not get threshold from EEPROM");
                throw;
            }
            finally
            {

            }
            return threshold;
        }



        /// <summary>
        /// When the user presses the reset button this function is called. This will get the default threshold values from
        /// configuration.json and send it back to the frontend
        /// </summary>
        /// <returns>object of type <c>LightThreshold</c> It will contain the lower and upper light
        /// threshold that were stored in the configuration.json file</returns>
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
                logger.LogInformation("Default threshold values retrieved");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not get default values from configuration.json");
                throw;
            }
            return threshold;
        }


        public LDRData[] GetDatabaseData()
        {
            LDRData[] data; 
            try
            {
                TimeSpan interval = new TimeSpan(0, 2, 0);
                DateTime initialTime = DateTime.Now;
                initialTime = initialTime.Subtract(interval);

                var oldValues = _dbContext.LDRData.Where(p => p.TimeStamp < initialTime).ToList();

                if (oldValues != null)
                {
                    _dbContext.LDRData.RemoveRange(oldValues);
                }

           
                _dbContext.SaveChanges();
                data = _dbContext.LDRData.ToArray();

                logger.LogInformation("Values retrieved from database");
            }
            catch(Exception ex)
            {
                logger.LogError(ex,"Could not get data from database");
                throw;
            }
            return data;
        }
    }
}
