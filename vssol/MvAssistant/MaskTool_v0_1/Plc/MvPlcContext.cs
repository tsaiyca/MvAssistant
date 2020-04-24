﻿using MvAssistant.DeviceDrive.OmronPlc;
using MvAssistant.Tasking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcContext : IDisposable
    {

        public MvOmronPlcLdd PlcLdd;
        public string PlcIp;
        public int PlcPort;
        bool m_isConnected = false;

        MvCancelTask m_keepConnection;


        public bool IsConnected { get { return m_isConnected; } }

        public MvPlcInspCh InspCh;
        public MvPlcBoxRobot BoxRobot;
        public MvPlcMaskRobot MaskRobot;
        public MvPlcOpenStage OpenStage;
        public MvPlcCabinet Cabinet;
        public MvPlcCleanCh CleanCh;
        public MvPlcLoadPort LoadPort;

        public MvPlcContext()
        {
            this.InspCh = new MvPlcInspCh(this);
            this.BoxRobot = new MvPlcBoxRobot(this);
            this.MaskRobot = new MvPlcMaskRobot(this);
            this.OpenStage = new MvPlcOpenStage(this);
            this.Cabinet = new MvPlcCabinet(this);
            this.CleanCh = new MvPlcCleanCh(this);
            this.LoadPort = new MvPlcLoadPort(this);
        }
        ~MvPlcContext() { this.Dispose(false); }



        public void Connect(string ip = null, int? port = null)
        {
            if (ip != null) this.PlcIp = ip;
            if (port != null) this.PlcPort = port.Value;

            this.PlcLdd = new MvOmronPlcLdd();
            this.PlcLdd.NLPLC_Initial(this.PlcIp, this.PlcPort);
        }


        public T Read<T>(MvEnumPlcVariable plcvar)
        {
            var obj = this.PlcLdd.Read(plcvar.ToString());

            return (T)obj;
        }
        public void Write(MvEnumPlcVariable plcvar, object data)
        {
            this.PlcLdd.Write(plcvar.ToString(), data);
        }


        void LockAssign<T>(ref T prop, T val)
        {
            lock (this)
            {
                prop = val;
            }
        }

        public int StartAsyn()
        {


            this.m_keepConnection = MvCancelTask.RunLoop(() =>
            {

                this.Write(MvEnumPlcVariable.PC_TO_PLC_CheckClock, false);
                if (!SpinWait.SpinUntil(() => !this.Read<bool>(MvEnumPlcVariable.PC_TO_PLC_CheckClock_Reply), 2500))
                {
                    this.LockAssign(ref this.m_isConnected, false);
                    return false;
                }
                this.LockAssign(ref this.m_isConnected, true);

                this.Write(MvEnumPlcVariable.PC_TO_PLC_CheckClock, true);
                if (!SpinWait.SpinUntil(() => this.Read<bool>(MvEnumPlcVariable.PC_TO_PLC_CheckClock_Reply), 2500))
                {

                    this.LockAssign(ref this.m_isConnected, false);
                    //throw new MvException("PLC connection T1 timeout");
                    return false;
                }
                else this.LockAssign(ref this.m_isConnected, true);

                return true;
            }, 1000);



            return 0;
        }

        public void Close()
        {
            using (var obj = this.PlcLdd)
            {
                
            }

            if (this.m_keepConnection != null)
            {
                using (var obj = this.m_keepConnection)
                {
                    obj.Cancel();
                    SpinWait.SpinUntil(() => obj.IsEnd(), 1000);
                }
                this.m_keepConnection = null;
            }
        }

        public void ClosePort()
        {
            this.PlcLdd.NLPLC_ClosePort();
        }

        //信號燈
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        {
            this.Write(MvEnumPlcVariable.PC_TO_DR_Red, Red);
            this.Write(MvEnumPlcVariable.PC_TO_DR_Orange, Orange);
            this.Write(MvEnumPlcVariable.PC_TO_DR_Blue, Blue);
        }

        //蜂鳴器
        public void SetBuzzer(uint BuzzerType)
        {
            this.Write(MvEnumPlcVariable.PC_TO_DR_Buzzer, BuzzerType);
        }

        //A08外罩風扇開關、風速控制
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        {
            string Result = "";
            try
            {
                this.Write(MvEnumPlcVariable.PC_TO_FFU_SetSpeed, WindSpeed);
                this.Write(MvEnumPlcVariable.PC_TO_FFU_Address, FanID);
                this.Write(MvEnumPlcVariable.PC_TO_FFU_Write, false);
                Thread.Sleep(100);
                this.Write(MvEnumPlcVariable.PC_TO_FFU_Write, true);

                if (!SpinWait.SpinUntil(() => this.Read<bool>(MvEnumPlcVariable.FFU_TO_PC_Write_Reply), 1000))
                    throw new MvException("Outer Cover Fan Control T0 timeout");
                else if (!SpinWait.SpinUntil(() => this.Read<bool>(MvEnumPlcVariable.FFU_TO_PC_Write_Complete), 5000))
                    throw new MvException("Outer Cover Fan Control T2 timeout");

                switch (this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_Write_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "Failed";
                        break;
                }

                this.Write(MvEnumPlcVariable.PC_TO_FFU_Write, false);

                if (!SpinWait.SpinUntil(() => !this.Read<bool>(MvEnumPlcVariable.FFU_TO_PC_Write_Complete), 1000))
                    throw new MvException("Outer Cover Fan Control T4 timeout");
            }
            catch (Exception ex)
            {
                this.Write(MvEnumPlcVariable.PC_TO_FFU_Write, false);
                throw ex;
            }
            return Result;
        }

        public List<int> ReadCoverFanSpeed()
        {
            List<int> FanSpeedList = new List<int>();
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_1));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_2));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_3));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_4));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_5));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_6));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_7));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_8));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_9));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_10));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_11));
            FanSpeedList.Add(this.Read<int>(MvEnumPlcVariable.FFU_TO_PC_FFUCurrentSpeed_12));
            return FanSpeedList;

        }

        public void ResetAll()
        {
            string Result = "";
            try
            {
                this.Write(MvEnumPlcVariable.Reset_ALL, false);
                Thread.Sleep(100);
                this.Write(MvEnumPlcVariable.Reset_ALL, true);

                if (!SpinWait.SpinUntil(() => this.Read<bool>(MvEnumPlcVariable.Reset_ALL_Complete), 2000))
                    throw new MvException("Reset All T0 timeout");

                this.Write(MvEnumPlcVariable.Reset_ALL, false);
            }
            catch (Exception ex)
            {
                this.Write(MvEnumPlcVariable.Reset_ALL, false);
                throw ex;
            }
        }

        public void EMSAlarm(bool BT_EMS,bool RT_EMS,bool OS_EMS,bool IC_EMS)
        {
            this.Write(MvEnumPlcVariable.PC_TO_BT_EMS, BT_EMS);
            this.Write(MvEnumPlcVariable.PC_TO_MT_EMS, RT_EMS);
            this.Write(MvEnumPlcVariable.PC_TO_OS_EMS, OS_EMS);
            this.Write(MvEnumPlcVariable.PC_TO_IC_EMS, IC_EMS);
        }

        #region IDisposable
        // Flag: Has Dispose already been called?
        protected bool disposed = false;


        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }


        protected virtual void DisposeSelf()
        {
            this.Close();
        }



        #endregion


    }
}
