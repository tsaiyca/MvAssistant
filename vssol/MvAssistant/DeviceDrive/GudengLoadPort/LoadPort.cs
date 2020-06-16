﻿using MvAssistant.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort
{
    public class LoadPort
    {
        /**
        public  delegate void OriginalInvokeMethod() ;
      
        OriginalInvokeMethod DelgateOriginalMethod;
        private bool CommandCascadeMode = false;
        private bool IsCommandCascade
        {
            get
            {
                return CommandCascadeMode;
            }
        }
        public void SetCommandCascade()
        {
            CommandCascadeMode = true;
        }
        public void ReleaseCommandCasadeMode()
        {
            CommandCascadeMode = false;
        }
        public void ClearOriginalMethod()
        {
            DelgateOriginalMethod = null;
        }
        public void SetOriginalMethod(OriginalInvokeMethod delegateMethod)
        {
            if (IsCommandCascade)
            {
                DelgateOriginalMethod = delegateMethod;
            }
            else
            {
                DelgateOriginalMethod = null;
            }
        }
        private bool HasInvokeOriginalMethod
        {
            get
            {
                if (DelgateOriginalMethod == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
     */
        /*private string OriginalCommandText{get;set;}
        /// <summary>是否有最初指定的 Command</summary>
        private bool HasOriginalCommand
        {
            get
            {
                if (string.IsNullOrWhiteSpace(OriginalCommandText))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        */
        /// <summary>Load Port 編號</summary>
        public int LoadPortNo { get; private set; }
        /// <summary>Server 端 End point</summary>
        public IPEndPoint ServerEndPoint { get; private set; }
        /// <summary>本地端要送資料到 Server 端的 Client 物件</summary>
        private Socket ClientSocket = null;
        /// <summary>是否已監聽 Server </summary>
        public bool IsListenServer { get; private set; }
        /// <summary>Client(本地端) 端監 Server</summary>
        public Thread ThreadClientListen = null;
        /// <summary>收到 Server 端傳回資料時的事件處理程序</summary>
        private event EventHandler OnReceviceRtnFromServerHandler = null;

        /// <summary>建構式</summary>
        public LoadPort()
        {
           
           OnReceviceRtnFromServerHandler += ReceiveMessageFromServer;
        }

        /// <summary>建構式</summary>
        /// <param name="serverEndpoint">Server 端 Endpoint</param>
        /// <param name="loadportNo">Load Port 序號</param>
        public LoadPort(IPEndPoint serverEndpoint, int loadportNo) : this()
        {
            ServerEndPoint = serverEndpoint;
            LoadPortNo = loadportNo;
        }

        /// <summary>建構式</summary>
        /// <param name="serverIP">Server 端IP</param>
        /// <param name="serverPort">Server Port</param>
        /// <param name="loadportNo">Load port序號</param>
        public LoadPort(string serverIP, int serverPort, int loadportNo) : this(new IPEndPoint(IPAddress.Parse(serverIP), serverPort), loadportNo)
        {

        }

        /// <summary>Client(本地)端 Listen 收到 Server 端回傳資料後引發的事件程序</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ReceiveMessageFromServer(object sender, EventArgs args)
        {
            var eventArgs = (OnReceviceRtnFromServerEventArgs)args;
            ReturnFromServer rtnContent = new ReturnFromServer(eventArgs.RtnContent);
            var methodName = rtnContent.StringContent.Replace(" ", "_");//.Replace("\0", "");
            try
            {
                var method = typeof(LoadPort).GetMethod(methodName);
                method.Invoke(this, new object[] { rtnContent });
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>啟動監聽 Server 端的 Thread</summary>
        public void StartListenServerThread()
        {
            try
            {
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(ServerEndPoint);
                ThreadClientListen = new Thread(ListenFromServer);
                ThreadClientListen.Start();
                IsListenServer = true;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>監聽 Server 的Method</summary>
        private void ListenFromServer()
        {
            while (true)
            {
                byte[] B = new byte[1023];
                int inLine = ClientSocket.Receive(B);//從Server端回復
                string rtn = Encoding.Default.GetString(B, 0, inLine);

                //rtn = "~001,Placement,0@\0\0\0\0";

                Debug.WriteLine("[RETURN] " + rtn);
                if (OnReceviceRtnFromServerHandler != null)
                {
                    // 可能一次會有多個結果
                    var rtnAry = rtn.Split(new string[] { BaseHostToLoadPortCommand.CommandPostfixText }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var element in rtnAry)
                    {
                        var eventArgs = new OnReceviceRtnFromServerEventArgs(element);
                        OnReceviceRtnFromServerHandler.Invoke(this, eventArgs);
                    }
                }
            }
        }

        /// <summary>送出 指令</summary>
        /// <param name="commandText">指令</param>
        private void Send(string commandText)
        {
            Debug.WriteLine("[COMMAND] " + commandText);
            byte[] B = Encoding.Default.GetBytes(commandText);
            ClientSocket.Send(B, 0, B.Length, SocketFlags.None);
        }
        #region Command
       
        /// <summary>Command DockRequest(100)</summary>
        /// <remarks>Main Event: DockPODStart</remarks>
        public void CommandDockRequest()
        {
         
            if (IsListenServer)
            {
              
                var command = new DockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                 Send(command);
            }
        }

        /// <summary>Command UndockRequest(101)</summary>
        /// <remarks></remarks>
        public void CommandUndockRequest()
        {
            
            if (IsListenServer)
            {
               
                var command = new UndockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskPlacementStatus(102)</summary>
        /// <remarks>Main Event: Placement</remarks>
        public void CommandAskPlacementStatus()
        {
           
            if (IsListenServer)
            {
             
                var command = new AskPlacementStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskPresentStatus(103)</summary>
        /// <remarks>Main Event: Present</remarks>
        public void CommandAskPresentStatus()
        {
             
            if (IsListenServer)
            {
                
                var command = new AskPresentStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskClamperStatus(104)</summary>
        /// <remarks>Main Event: Clamper</remarks>
        public void CommandAskClamperStatus()
        {
          
            if (IsListenServer)
            {
               
              
                var command = new AskClamperStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskRFIDStatus(105)</summary>
        /// <remarks>Main Event: RFID</remarks>
        public void CommandAskRFIDStatus()
        {
         
            if (IsListenServer)
            {
             
                var command = new AskRFIDStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskBarcodeStatus(106)</summary>
        /// <remarks>Main Event: Barcode ID(Invoke: Barcode_ID)</remarks>
        public void CommandAskBarcodeStatus()
        {
          
            if (IsListenServer)
            {
              
                var command = new AskBarcodeStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskVacuumStatus(107)</summary>
        /// <remarks>Main Event: VacuumComplete</remarks>
        public void CommandAskVacuumStatus()
        {

            
            if (IsListenServer)
            {
              
                var command = new AskVacuumStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskReticleExistStatus(108)</summary>
        /// <remarks>
        /// Main Event: 
        /// <para>DockPODComplete_HasReticle(009)</para><para>.OR.</para>
        /// <para>DockPODComplete_Empty(010)</para>
        /// </remarks>
        public void CommandAskReticleExistStatus()
        {
           
            if (IsListenServer)
            {
              
                var command = new AskReticleExistStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>AlarmReset(109)</summary>
        /// <remarks>
        /// Main Event: 
        /// <para>AlarmResetSuccess</para>
        /// <para>.OR.</para>
        /// <para>AlarmResetFail</para>
        /// </remarks>
        public void CommandAlarmReset()
        {
            if (IsListenServer)
            {
              
                var command = new AlarmReset().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskStagePosition(110)</summary>
        /// <remarks>Main Event: StagePosition</remarks>
        public void CommandAskStagePosition()
        {
         
            if (IsListenServer)
            {

              
                var command = new AskStagePosition().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command AskLoadportStatus(111)</summary>
        /// <remarks>Main Event: LoadportStatus</remarks>
        public void CommandAskLoadportStatus()
        {
           
            if (IsListenServer)
            {
              
                var command = new AskLoadportStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }
        /// <summary>Command Initilial Request(112)</summary>
        public void CommandInitialRequest()
        {
            if (IsListenServer)
            {
               
                var command = new InitialRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        /// <summary>Command ManualClamperLock</summary>
        /// <remarks>Main Event: Clamper</remarks>
        public void CommandManualClamperLock()
        {
         
            if (IsListenServer)
            {
               
                var command = new ManualClamperLock().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualClamperUnlock()
        {
            if (IsListenServer)
            {
                var command = new ManualClamperUnlock().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualClamperOPR()
        {
            if (IsListenServer)
            {
                var command = new ManualClamperOPR().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageUp()
        {
            if (IsListenServer)
            {
                var command = new ManualStageUp().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageInspection()
        {
            if (IsListenServer)
            {
                var command = new ManualStageInspection().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageDown()
        {
            if (IsListenServer)
            {
                var command = new ManualStageDown().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageOPR()
        {
            if (IsListenServer)
            {
                var command = new ManualStageOPR().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualVacuumOn()
        {
            if (IsListenServer)
            {
                var command = new ManualVacuumOn().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualVacuumOff()
        {
            if (IsListenServer)
            {
                var command = new ManualVacuumOff().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }
     

       
        /// <summary>Event Placement (001)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. placement 狀態改變</para>
        /// <para>2. 收到 AskPlacement </para>
        /// </remarks>
        private void Placement(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 有無Placement 信號
            var rtnCode = (EventPlacementCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
           
            var eventArgs = new OnPlacementEventArgs(rtnCode);
            if (OnPlacementHandler != null)
            {
                OnPlacementHandler.Invoke(this,eventArgs);
            }
        }
        public event EventHandler OnPlacementHandler = null;
        public void ResetOnPlacementHandler() { OnPlacementHandler = null; }
        public class OnPlacementEventArgs : EventArgs
        {
            public EventPlacementCode ReturnCode { get; private set; }
            private OnPlacementEventArgs() { }
            public OnPlacementEventArgs(EventPlacementCode rtnCode) : this() { ReturnCode = rtnCode; }
        }

        /// <summary>Event Present(002)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Present狀態改變</para>
        /// <para>2. 收到 AskPresentStatus</para>
        /// </remarks>
        private void Present(ReturnFromServer rtnFromServer)
        {  
            // rtnCode: 有無Present訊息
            var rtnCode = (EventPresentCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
          
            if (OnPresentHandler != null)
            {
                var eventArgs = new OnPresentEventArgs(rtnCode);
                OnPresentHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnPresentHandler = null;
        public void ResetOnPresentHandler() { OnPresentHandler = null; }
        public class OnPresentEventArgs : EventArgs
        {
            public EventPresentCode ReturnCode { get; private set; }
            private OnPresentEventArgs() { }
            public OnPresentEventArgs(EventPresentCode rtnCode) : this() { ReturnCode = rtnCode; }
        }

      
        /// <summary>Event Clamper(003)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Clamper 狀態改變</para>
        /// <para>2. 收到 AskClamperStatus</para>
        /// </remarks>
        private void Clamper(ReturnFromServer rtnFromServer)
        {
            //rtnCode: Clamper狀態(關閉, 開啟, 不在定位需復歸)
            var rtnCode = (EventClamperCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            
            if(OnClamperHandler != null)
            {
                var eventArgs = new OnClamperEventArgs(rtnCode);
                OnClamperHandler.Invoke(this,eventArgs);
            }
        }
        public event EventHandler OnClamperHandler = null;
        public void ResetOnClamperHandler() { OnClamperHandler = null; }
        public class OnClamperEventArgs : EventArgs
        {
            public EventClamperCode ReturnCode { get; private set; }
            private OnClamperEventArgs() { }
            public OnClamperEventArgs(EventClamperCode rtnCode) : this() { ReturnCode = rtnCode; }
        }


        /// <summary>Event RFID(004)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨, 完成讀取RFID後</para>
        /// <para>2. 收到AskRFIDStatus</para>
        /// </remarks>
        private void RFID(ReturnFromServer rtnFromServer)
        {   // rfID: 讀取的 RFID
            var rfID = rtnFromServer.ReturnCode;
            
            if (OnRFIDHandler != null)
            {
                var eventArgs = new OnRFIDEventArgs(rfID);
                if (OnRFIDHandler != null)
                {
                    OnRFIDHandler.Invoke(this,eventArgs);
                }
            }
        }
        public event EventHandler OnRFIDHandler = null;
        public void ResetOnRFIDHandler() { OnRFIDHandler = null; }
        public class OnRFIDEventArgs : EventArgs
        {
            public string RFID { get;private set; }
            private OnRFIDEventArgs() { }
            public OnRFIDEventArgs(string rfid):this() { RFID = rfid; }
        } 



        /// <summary>Event Barcode ID (005)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨完成讀取Barcode後</para>
        /// <para>2. 收到AskBarcodeStatus</para>
        /// </remarks>
        private void Barcode_ID(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 讀取成功或失敗
            var rtnCode = (EventPlacementCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            // barcodeID: 讀取成功時的 barcode ID  
            var barcodeID = rtnFromServer.ReturnValue;
          
            if (OnBarcode_IDHandler != null)
            {
                var eventArgs = new OnBarcode_IDEventArgs(rtnCode, barcodeID);
                OnBarcode_IDHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnBarcode_IDHandler = null;
        public void ResetOnBarcode_IDHandler() { OnBarcode_IDHandler = null; }
        public class OnBarcode_IDEventArgs : EventArgs
        {
            public EventPlacementCode ReturnCode { get; private set; }
            public string BarcodeID { get; private set; }
            private OnBarcode_IDEventArgs() { }
            public OnBarcode_IDEventArgs(EventPlacementCode rtnCode, string barCodeID) : this() { ReturnCode = rtnCode; BarcodeID = barCodeID; }
        }


        /// <summary>Event ClamperUnlockComplete(006)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨完成後開啟Clamper</para>
        /// <para>2. 收到AskClamperStatus</para>
        /// </remarks>
        private void ClamperUnlockComplete(ReturnFromServer rtnFromServer)
        {
            // rtnCode: Clamper 關閉/開啟  
            var rtnCode = (EventClamperUnlockCompleteCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
           
            if (OnClamperUnlockCompleteHandler != null)
            {
                var eventArgs = new OnClamperUnlockCompleteEventArgs(rtnCode);
                OnClamperUnlockCompleteHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnClamperUnlockCompleteHandler = null;
        public void ResetOnClamperUnlockCompleteHandler()  { OnClamperUnlockCompleteHandler = null; }
        public class OnClamperUnlockCompleteEventArgs : EventArgs
        {
            public EventClamperUnlockCompleteCode ReturnCode { get; private set; }
            private OnClamperUnlockCompleteEventArgs() { }
            public OnClamperUnlockCompleteEventArgs(EventClamperUnlockCompleteCode rtnCode) : this() { ReturnCode = rtnCode; }
        }



        /// <summary>Event VacummComplete(007)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨完成後底盤吸住</para>
        /// <para>2. 收到AskVacuumStatus</para>
        /// <para>3. 底座真空狀態改變</para>
        /// </remarks>
        private void VacuumComplete(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 是否建立真空
            var rtnCode = (EventVacuumCompleteCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
          
            if (OnVacuumCompleteHandler != null)
            {
                var eventArgs = new OnVacuumCompleteEventArgs(rtnCode);
                OnVacuumCompleteHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnVacuumCompleteHandler = null;
        public void ResetOnVacuumCompleteHandler() { OnVacuumCompleteHandler = null; }
        public class OnVacuumCompleteEventArgs : EventArgs
        {
            public EventVacuumCompleteCode ReturnCode { get; private set; }
            private OnVacuumCompleteEventArgs() { }
            public OnVacuumCompleteEventArgs(EventVacuumCompleteCode rtnCode):this() { ReturnCode = rtnCode; }
        }

       
        /// <summary>Event  DockPODStart(008)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>收到DockRequest後, 開始移動Stage前</remarks>
        private void DockPODStart(ReturnFromServer rtnFromServer)
        {
           
            if (OnDockPODStartHandler != null)
            {
                OnDockPODStartHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public void ResetDockOnPODStartHandler() { OnDockPODStartHandler = null; }
        public event EventHandler OnDockPODStartHandler = null;


        /// <summary>Event  DockPODComplete_HasReticle(009)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. StageDock完畢後</para>
        /// <para>2. 收到AskReticleExistStatus</para>
        /// </remarks>
        private void DockPODComplete_HasReticle(ReturnFromServer rtnFromServer)
        {
           
            if (OnDockPODComplete_HasReticleHandler != null)
            {
                OnDockPODComplete_HasReticleHandler.Invoke(this,EventArgs.Empty);
            }
        }
        public event EventHandler OnDockPODComplete_HasReticleHandler = null;
        public void ResetOnDockPODComplete_HasReticleHandler() { OnDockPODComplete_HasReticleHandler = null; }


        /// <summary>Event  DockPODComplete_Empty(010)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. StageDock完畢後</para>
        /// <para>2. 收到AskReticleExistStatus</para>
        /// </remarks>
        private void DockPODComplete_Empty(ReturnFromServer rtnFromServer)
        {

         
            if (OnDockPODComplete_EmptyHandler != null)
            {
                OnDockPODComplete_EmptyHandler.Invoke(this,EventArgs.Empty);
            }
        }
        public event EventHandler OnDockPODComplete_EmptyHandler = null;
        public void ResetOnDockPODComplete_EmptyHandler() { OnDockPODComplete_EmptyHandler = null; }


        /// <summary>Event UndockComplete(011)</summary>
        /// <param name="rtnFromServer"></param>
        private void UndockComplete(ReturnFromServer rtnFromServer)
        {
           
            if (OnUndockCompleteHandler != null)
            {
                OnUndockCompleteHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnUndockCompleteHandler = null;
        public void ResetOnUndockCompleteHandler() { OnUndockCompleteHandler = null; }

        /// <summary>Event ClamperLockComplete(012)</summary>
        /// <param name="rtnFromServer"></param>
        private void ClamperLockComplete(ReturnFromServer rtnFromServer)
        {
        
            if (OnClamperLockCompleteHandler != null)
            {
                OnClamperLockCompleteHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnClamperLockCompleteHandler = null;
        public void ResetOnClamperLockCompleteHandler() { OnClamperLockCompleteHandler = null; }


       
        /// <summary>Event AlarmResetSuccess (013)</summary>
        /// <remarks>異常恢復成功</remarks>
        /// <param name="rtnFromServer"></param>
        private  void AlarmResetSuccess(ReturnFromServer rtnFromServer)
        {
          
        
            if (OnAlarmResetSuccessHandler != null)
            {
                OnAlarmResetSuccessHandler.Invoke(this, EventArgs.Empty);
            }
            
        }
        public event EventHandler OnAlarmResetSuccessHandler = null;
        public void ResetOnAlarmResetSuccessHandler() { OnAlarmResetSuccessHandler = null; }

        /// <summary>Event AlarmResetFail(014)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>異常恢復失敗</remarks>
        private void AlarmResetFail(ReturnFromServer rtnFromServer)
        {
          
            if (OnAlarmResetFailHandler != null)
            {
                OnAlarmResetFailHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnAlarmResetFailHandler = null;
        public void ResetOnAlarmResetFailHandler() { OnAlarmResetFailHandler = null; }


        /// <summary>Event  ExecuteInitialFirst(015)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>提示進行原點復歸動作</remarks>
        private void ExecuteInitialFirst(ReturnFromServer rtnFromServer)
        {
         
           if(OnExecuteInitialFirstHandler != null)
            {
                OnExecuteInitialFirstHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnExecuteInitialFirstHandler = null;
        public void ResetOnExecuteInitialFirstHandler() { OnExecuteInitialFirstHandler = null; }

      
        /// <summary>Event AlarmResetFirst (016)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>提示進行異常復歸動作</remarks>
        private void ExecuteAlarmResetFirst(ReturnFromServer rtnFromServer)
        {
         
            if (OnExecuteAlarmResetFirstHandler != null)
            {
                OnExecuteAlarmResetFirstHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnExecuteAlarmResetFirstHandler = null;
        public void ResetExecuteOnAlarmResetFirstHandler() { OnExecuteAlarmResetFirstHandler = null; }

        /// <summary>Event StagePosition (017)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Stage位置改變</para>
        /// <para>2. 收到AskStagePosition</para>
        /// </remarks>
        private void StagePosition(ReturnFromServer rtnFromServer)
        {
            // rtnCode: Stage 位置
            var rtnCode = (EventStagePositionCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            
            if (OnStagePositionHandler != null)
            {
                var eventArgs = new OnStagePositionEventArgs(rtnCode);
                OnStagePositionHandler.Invoke(this,eventArgs);
            }
        }
        public event EventHandler OnStagePositionHandler = null;
        public void ResetOnStagePositionHandler() { OnStagePositionHandler = null; }
        public class OnStagePositionEventArgs : EventArgs
        {
            public EventStagePositionCode ReturnCode { get; private set; }
            private OnStagePositionEventArgs() { }
            public OnStagePositionEventArgs(EventStagePositionCode rtnCode):this() { ReturnCode = rtnCode; }
        }



        /// <summary>Event LoadportStatus(018)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Loadport內部狀態改變</para>
        /// <para>2. 收到AskLoadportStatus</para>
        /// </remarks>
        private void LoadportStatus(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventLoadportStatusCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
         
            if (OnLoadportStatusHandler != null)
            {
                var eventArgs = new OnLoadportStatusEventArgs(rtnCode);
                OnLoadportStatusHandler.Invoke(this, eventArgs);
            }
            
        }
        public event EventHandler OnLoadportStatusHandler = null;
        public void ResetOnLoadportStatusHandler() { OnLoadportStatusHandler = null; }
        public class OnLoadportStatusEventArgs : EventArgs
        {
            public EventLoadportStatusCode ReturnCode { get; private set; }
            private OnLoadportStatusEventArgs() { }
            public OnLoadportStatusEventArgs(EventLoadportStatusCode rtnCode) : this() { ReturnCode = rtnCode; }

        }


        /// <summary>Event Initial Complete(019)</summary>
        /// <remarks>初始化完畢後</remarks>
        /// <param name="rtnFromServer"></param>
        private void InitialComplete(ReturnFromServer rtnFromServer)
        {
           
            if (OnInitialCompleteHandler != null)
            {
                OnInitialCompleteHandler.Invoke(this,EventArgs.Empty);
            }
        }
        public void ResetInitialOnCompleteHandler(){   OnInitialCompleteHandler = null; }
        public event EventHandler OnInitialCompleteHandler = null;

      
        /// <summary>
        /// <para>傳送Initial Request 時沒有在指定時間內收到 InitialComplete 事件</para>
        /// <para>原始文件中未定義這個事件, 本事件為自定</para>
        /// </summary>
        /// <param name="rtnFromServer"></param>
        private void InitialUnComplete(/*ReturnFromServer rtnFromServer*/)
        {
            
            if (OnInitialUnCompleteHandler != null)
            {
                OnInitialUnCompleteHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public void ResetOnInitialUnCompleteHandler(){OnInitialUnCompleteHandler = null;}
        public event EventHandler OnInitialUnCompleteHandler = null;


        /// <summary>Event MustInAutoMode(020)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>2020/06/11 new</remarks>
        private void MustInAutoMode(ReturnFromServer rtnFromServer)
        {
          
            if (OnMustInAutoModeHandler != null)
            {
                OnMustInAutoModeHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnMustInAutoModeHandler = null;
        public void ResetOnMustInAutoModeHandler(){ OnMustInAutoModeHandler = null; }

        /// <summary>Event ClamperNotLock(022)</summary>
        /// <param name="rtnFromServer"></param>
        private void ClamperNotLock(ReturnFromServer rtnFromServer)
        {
           
            if (OnClamperNotLockHandler != null)
            {
                OnClamperNotLockHandler.Invoke(this,EventArgs.Empty);
            }
        }
        public  event EventHandler OnClamperNotLockHandler = null;
        public void ResetOnClamperNotLockHandler() { OnClamperNotLockHandler = null; }



        /// <summary>Event PODNotPutProperly(023)</summary>
        /// <remarks>2020/06/11 new</remarks>
        private void PODNotPutProperly(ReturnFromServer rtnFromServer)
        {
         
            if (OnPODNotPutProperlyHandler != null)
            {
                OnPODNotPutProperlyHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnPODNotPutProperlyHandler = null;
        public void ResetOnPODNotPutProperlyHandler() { OnPODNotPutProperlyHandler = null; }


        #endregion
        #region Alarm
        /// <summary>Alarm ClamperActionTimeOut(200)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Clamper馬達運動超時</remarks>
        private void ClamperActionTimeOut(ReturnFromServer rtnFromServer)
        {
         
            if (OnClamperActionTimeOutHandler != null)
            { OnClamperActionTimeOutHandler.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnClamperActionTimeOutHandler = null;
        public void ResetOnClamperActionTimeOutHandler() { OnClamperActionTimeOutHandler = null; }


        /// <summary>Alarm ClamperUnlockPositionFailed(201)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>ClamerUnlock完成後位置錯誤</remarks>
        private void ClamperUnlockPositionFailed(ReturnFromServer rtnFromServer)
        {
          
            if (OnClamperUnlockPositionFailedHandler != null)
            { OnClamperUnlockPositionFailedHandler.Invoke(this,EventArgs.Empty); }
        }
        public event EventHandler OnClamperUnlockPositionFailedHandler = null;
        public void ResetOnClamperUnlockPositionFailedHandler() { OnClamperUnlockPositionFailedHandler = null; }


        /// <summary>Alarm VacuumAbnormality(202)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>StageDock/Undock前真空值錯誤</remarks>
        private void VacuumAbnormality(ReturnFromServer rtnFromServer)
        {
          
            if (OnVacuumAbnormalityHandler != null)
            {
                OnVacuumAbnormalityHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnVacuumAbnormalityHandler = null;
        public void ResetOnVacuumAbnormalityHandler() { OnVacuumAbnormalityHandler = null; }


        /// <summary>Alarm StageMotionTimeout(203)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage運動超時</remarks>
        private void StageMotionTimeout(ReturnFromServer rtnFromServer)
        {
         
            if (OnStageMotionTimeoutHandler != null) { OnStageMotionTimeoutHandler.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnStageMotionTimeoutHandler = null;
        public void ResetOnStageMotionTimeoutHandler() { OnStageMotionTimeoutHandler = null; }


        /// <summary>Alarm StageOverUpLimitation(204)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage上位限制Sensor觸發</remarks>
        private void StageOverUpLimitation(ReturnFromServer rtnFromServer)
        {
            if (OnStageOverUpLimitationHandler != null){ OnStageOverUpLimitationHandler.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnStageOverUpLimitationHandler = null;
        public void ResetOnStageOverUpLimitationHandler() { OnStageOverUpLimitationHandler = null; }

        /// <summary>Alarm StageOverDownLimitation(205)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage下位限制Sensor觸發</remarks>
        private void StageOverDownLimitation(ReturnFromServer rtnFromServer)
        {
            if (OnStageOverDownLimitationHandler != null) { OnStageOverDownLimitationHandler.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnStageOverDownLimitationHandler = null;
        public void ResetOnStageOverDownLimitationHandler() { OnStageOverDownLimitationHandler = null; }



        /// <summary>Alarm ReticlePositionAbnormality(206)</summary>
        /// <param name="rtnFromServer"></param>
        ///<remarks>Dock/Undock時, 光罩滑出POD</remarks>
        private void ReticlePositionAbnormality(ReturnFromServer rtnFromServer)
        {
            if(OnReticlePositionAbnormalityHandler != null)
            {
                OnReticlePositionAbnormalityHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnReticlePositionAbnormalityHandler = null;
        public void ResetOnReticlePositionAbnormalityHandler() { OnReticlePositionAbnormalityHandler = null; }


        /// <summary>Alarm ClamperLockPositionFailed(207)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>ClamerLock完成後位置錯誤</remarks>
        private void ClamperLockPositionFailed(ReturnFromServer rtnFromServer)
        {
            if (OnClamperLockPositionFailed != null) { OnClamperLockPositionFailed.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnClamperLockPositionFailed = null;
        public void ResetOnClamperLockPositionFailed() { OnClamperLockPositionFailed = null; }


        /// <summary>Alarm CoverDisappear(208)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>POD上蓋被人員強行取走</remarks>
        private void CoverDisappear(ReturnFromServer rtnFromServer)
        {
             if (OnCoverDisappearHandler != null) { OnCoverDisappearHandler.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnCoverDisappearHandler = null;
        public void ResetOnCoverDisappearHandler() { OnCoverDisappearHandler = null; }

        /// <summary>Alarm ClamperMotorAbnormality(209)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Clamper開合馬達驅動器異常</remarks>
        private void ClamperMotorAbnormality(ReturnFromServer rtnFromServer)
        {
            if (OnClamperMotorAbnormality != null) { OnClamperMotorAbnormality.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnClamperMotorAbnormality = null;
        public void ResetOnClamperMotorAbnormality()
        { OnClamperMotorAbnormality = null; }


        /// <summary>Alarm StageMotorAbnormality(210)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage升降馬達驅動器異常</remarks>
        private void StageMotorAbnormality(ReturnFromServer rtnFromServer)
        {
            if (OnStageMotorAbnormality != null) { OnStageMotorAbnormality.Invoke(this, EventArgs.Empty); }
        }
        public event EventHandler OnStageMotorAbnormality = null;
        public void ResetOnStageMotorAbnormality() { OnStageMotorAbnormality = null; }
        #endregion



    }

    public class OnReceviceRtnFromServerEventArgs : EventArgs
    {
        public string RtnContent { get; private set; }
        public OnReceviceRtnFromServerEventArgs(string rtnContent)
        {
            RtnContent = rtnContent;
        }
    }

    public class ReturnFromServer
    {
        public string StringCode { get; set; }
        public string StringContent { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnValue { get; set; }
        //public string LastRequestCommandText { get; set; }
        public ReturnFromServer(string content)
        {
            content = content.Replace(BaseHostToLoadPortCommand.CommandPrefixText, "").Replace(BaseHostToLoadPortCommand.CommandPostfixText, "");
            var contentAry = content.Split(new string[] { BaseHostToLoadPortCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            StringCode = contentAry[0];
            StringContent = contentAry[1];
            if (contentAry.Length >= 3)
            {
                ReturnCode= contentAry[2];
                if(contentAry.Length > 3)
                {
                    ReturnValue= contentAry[3];
                }
            }
            else
            {
                ReturnCode = null;
            }
        }
    }


  
}
