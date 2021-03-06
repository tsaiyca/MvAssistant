﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using System.Drawing;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalOpenStage
    {
        [TestMethod]
        public void TestCamera()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                os.HalConnect();
                
                os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                os.Camera_FrontNearLP_CapToSave("D:/Image/OS/NearLP", "jpg");
                os.Camera_FrontNearCC_CapToSave("D:/Image/OS/NearCC", "jpg");
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;

                os.SetBoxType(1);
                os.SetSpeed(50);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;

                os.ReadBoxTypeSetting();
                os.ReadSpeedSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();
                    os.HalConnect();

                    os.ReadRobotIntrude(true, false);
                    os.ReadRobotIntrude(false, true);
                    os.ReadRobotIntrude(false, false);
                    os.ReadClampStatus();
                    os.ReadSortClampPosition();
                    os.ReadSliderPosition();
                    os.ReadCoverPos();
                    os.ReadCoverSensor();
                    os.ReadBoxDeform();
                    os.ReadWeightOnStage();
                    os.ReadBoxExist();
                    os.ReadOpenStageStatus();
                    os.ReadBeenIntruded();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                os.HalConnect();

                os.Initial();
                os.SortClamp();
                os.Vacuum(true);
                os.SortUnclamp();
                os.Close();
                os.Clamp();
                os.Open();
                os.Close();
                os.Unclamp();
                os.Lock();
                os.Vacuum(false);
            }
        }
    }
}
