﻿using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Button;
using MvAssistant.Mac.v1_0.Hal.Component.Door;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;
using MvAssistant.Mac.v1_0.Hal.Component.PresenceDetector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{    
    [GuidAttribute("D4D85EB6-C9DF-4B9D-A0A5-000B0ACEC252")]
    public interface IHalDrawer:IHalAssembly
    {

        //IHalLaser DirectionDetector { get; set; }
#region 20190909 mht 改掉 舊版
        //IHalFiberOptic InPlachDetector { get; set; }
        //IHalDoor SlotDoor { get; set; }

        //void MonitorButtonStatusPause();

        //bool SetDockCompleteButtonAddress(string varName);
        //bool SetUndockCompleteButtonAddress(string varName);
        //void StartMonitorButtonStatus();
#endregion
        IHalInfraredPhotointerrupter Interupter_PeopleSideLimit { get; set; }
        IHalInfraredPhotointerrupter Interupter_RobotSideLimit { get; set; }
        IHalInfraredPhotointerrupter Interupter_PeopleSide { get; set; }
        IHalInfraredPhotointerrupter Interupter_RobotSide { get; set; }
        IHalInfraredPhotointerrupter Interupter_Home { get; set; }
        IHalPresenceDetector BoxPresentDetector { get; set; }
        IHalButton Button_DrawerLoadControl { get; set; }
    }
}