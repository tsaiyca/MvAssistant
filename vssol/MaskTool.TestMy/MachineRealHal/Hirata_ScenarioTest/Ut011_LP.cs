﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut011_LP
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfInit();
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var lpa = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var lpb = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    lpb.HalConnect();
                    lpa.HalConnect();

                    //1.POD內有光罩, 且放置於Load Port A stage上

                    //2~5  Load Poat A Dock
                    lpa.Dock();

                    //6. POD內有光罩, 且放置於Load Port B stage上

                    //7~10  Load Poat B Dock
                    lpb.Dock();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}