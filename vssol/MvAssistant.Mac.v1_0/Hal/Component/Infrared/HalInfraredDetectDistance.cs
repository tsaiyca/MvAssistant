﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.Component;
using System.Runtime.InteropServices;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;


namespace MvAssistant.Mac.v1_0.Hal.Component.Infrared
{

    [GuidAttribute("381F012E-54B7-4B97-8AAB-BA123091A849")]
    public class HalInfraredDetectDistance : HalComponentBase, IHalInfraredDetectDistance
    {
        public bool SetIrAddress(string varName)
        {
            throw new NotImplementedException();
        }

        public float GetValue()
        {
            throw new NotImplementedException();
        }

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }
    }
}