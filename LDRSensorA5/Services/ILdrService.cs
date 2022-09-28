﻿using LDRSensorA5.Models;

namespace LDRSensorA5.Services
{
    public interface ILdrService
    {
        ResponseModel SetThresholdValues(LightThreshold threshold);
        ResponseModel ResetThresholdValues(string command);

        LDRData GetLDRData();

    }
}
