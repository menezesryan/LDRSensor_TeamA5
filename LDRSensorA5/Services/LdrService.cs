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


        /// <summary>
        /// This is the constructor for the class <c>LdrService</c>. Here the interface <c>ICommunicationService</c>
        /// and the database context <c>LdrDBContext</c> are injected.
        /// </summary>
        /// <param name="communication">interface object which allows this service to access the functions</param>
        /// <param name="dbContext">object of <c>LdrDBContext</c> allows to do CRUD functionality on database</param>
        public LdrService(ICommunicationService communication, LdrDBContext dbContext)
        {
            _communicationService = communication;
            _dbContext = dbContext;
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
                    port.WriteLine("lux-getLux\r");
                    var command = port.ReadLine();
                    Console.WriteLine(command);
                    return command;
                });

                //string[] value=command.Split('>');
                //string[] newvalue = value[1].Split(' ');
                //Random random = new Random();
                //var x = random.Next(1, 20);


                //Console.WriteLine("after trim "+Double.Parse(newvalue[0]).ToString("N2"));
                //data.Lux = Double.Parse(newvalue[0]);
                //data.Current = (Double.Parse(newvalue[0]) * 1.75);


                data.Lux = Double.Parse(ldrData);
                data.TimeStamp = DateTime.Now;

                //transform light to current
                //data.Current = (Double.Parse(ldrData) * 1.75);

                //send current data back

                ConfigurationData configuration = new ConfigurationData();
                string fileName = "configuration.json";
                string jsonString = File.ReadAllText(fileName);
                configuration = JsonSerializer.Deserialize<ConfigurationData>(jsonString);

                _communicationService.FirmwareDataExchange((port) =>
                {
                    if (data.Lux > configuration.LowerThreshold && data.Lux < configuration.UpperThreshold)
                    {
                        data.Current = ((data.Lux - configuration.LowerThreshold) / (configuration.UpperThreshold - configuration.LowerThreshold) * 16) + 4;
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
                _dbContext.Add<LDRData>(data);
                _dbContext.SaveChanges();  
            }
            catch(Exception)
            {
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
                    port.WriteLine("lux-luxConfig-" + configuration.LowerThreshold + "-" + configuration.UpperThreshold + "\r");
                    port.WriteLine("lux-luxSave\r");
                    return 1;
                });

               

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

                //send data to the MCU
                //update data

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

                _communicationService.FirmwareDataExchange((port) =>
                {
                    port.WriteLine("lux-luxConfig-" + configuration.LowerThreshold + "-" + configuration.UpperThreshold + "\r");
                    return 1;
                });


                model.IsSucess = true;
                model.Message = "Threshold values updated";
            }

            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
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
            }
            catch(Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
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
                    port.WriteLine("lux-luxConfig-" + threshold.LowerThreshold + "-" + threshold.UpperThreshold + "\r");
                    return 1;

                });

                    
                

               
            }
            catch(Exception)
            {
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
            }
            catch (Exception)
            {
                throw;
            }
            return threshold;
        }
    }
}
