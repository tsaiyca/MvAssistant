﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{

    [TestClass]
    public class UtHalBoxTransfer
    {
        /// <summary>路徑測試</summary>
        /// <remarks>King, 2020/05/25</remarks>
        [TestMethod]
        public void TestPathMove()
        {
           int boxStartIndex = default(int);
            int boxEndIndex = default(int);
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();


                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    
                    if (bt.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }


                    bt.GotoStage1();
                    // [V] 回到 Cabinet 1 Home
                    bt.BackCabinet1Home();

                    // [ ] 前進到 Cabinet 1 的某個盒子
                    boxStartIndex = 1; boxEndIndex = 1;// boxEndIndex :最多 20
                    for (var boxIndex = boxStartIndex; boxIndex <= boxEndIndex; boxIndex++)
                    {
                        bt.ForwardToCabinet1(boxIndex);
                        System.Threading.Thread.Sleep(2000);
                    }


                  

                    // [ ] 從 Cabinet 1 回到 Cabinet1 Home
                    bt.BackwardFromCabinet1();

                    // [ ] 轉到 Cabinet 2 方向
                    bt.ChangeDirectionToFaceCabinet2();

                    // [ ] 前到 Cabinet 2 某個盒子
                    boxStartIndex = 1; boxEndIndex = 1;// boxEndIndex :最多 15
                    for (var boxIndex = boxStartIndex; boxIndex <= boxEndIndex; boxIndex++)
                    {
                        bt.ForwardToCabinet2(boxIndex);
                        System.Threading.Thread.Sleep(2000);
                    }

                    // [ ] 從 Cabnet 2 回到 Cabinet2 Home
                    bt.BackwardFromCabinet2();

                    // [ ] 轉向面對Open Stage 方向
                    bt.ChangeDirectionToFaceOpenStage();

                    // [ ] 前進到 Open Stage
                    bt.ForwardToOpenStage();

                    // [ ] 從 Open Stage  回到 Open Stage
                    bt.BackwardFromOpenStage();

                    // [ ] 轉向 Cbinet 方向
                    bt.ChangeDirectionToFaceCabinet1();
  
                }
            }
            catch (Exception ex) { throw ex; }

        }
    

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.SetSpeed(20);
                bt.SetHandSpaceLimit(10, 20);
                bt.SetClampToCabinetSpaceLimit(50);
                bt.SetLevelSensorLimit(5, 6);
                bt.SetSixAxisSensorLimit(1, 2, 3, 4, 5, 6);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.ReadSpeedSetting();
                bt.ReadHandSpaceLimitSetting();
                bt.ReadClampToCabinetSpaceLimitSetting();
                bt.ReadLevelSensorLimitSetting();
                bt.ReadSixAxisSensorLimitSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.ReadHandPos();
                bt.ReadBoxDetect();
                bt.ReadHandPosByLSR();
                bt.ReadClampDistance();
                bt.ReadLevelSensor();
                bt.ReadSixAxisSensor();
                bt.ReadHandVacuum();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.Clamp(1);
                bt.Unclamp();
                bt.LevelReset();
                bt.ReadBTRobotStatus();
                bt.RobotMoving(false);
                bt.Initial();
            }
        }
    }
}