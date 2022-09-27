﻿using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public class ConnectionService : IConnectionService
    {
        public ResponseModel Connect(string command)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //do something

                model.IsSucess = true;
                model.Message = "Threshold Value reset successful";
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }

            return model;

        }

        public ResponseModel Disconnect(string command)
        {
            ResponseModel model = new ResponseModel();

            try
            {
                //do something

                model.IsSucess = true;
                model.Message = "Threshold Value reset successful";
            }

            catch (Exception ex)
            {
                model.IsSucess = false;
                model.Message = "Error: " + ex.Message;
            }

            return model;
        }
    }
}
