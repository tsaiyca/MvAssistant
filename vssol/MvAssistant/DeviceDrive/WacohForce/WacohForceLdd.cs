﻿using CToolkit.v1_1.Net;
using MvAssistant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MvAssistant.DeviceDrive.WacohForce
{



    public class WacohForceLdd : IDisposable
    {

        CtkNonStopTcpClient netNonStopTcpClient = new CtkNonStopTcpClient();
        public CtkNonStopTcpClient netClient { get { return this.netNonStopTcpClient; } }
        WacohForceMessageReceiver messageReceiver = new WacohForceMessageReceiver();

        public IPEndPoint localEP { get { return this.netNonStopTcpClient.localEP; } set { this.netNonStopTcpClient.localEP = value; } }
        public IPEndPoint remoteEP { get { return this.netNonStopTcpClient.remoteEP; } set { this.netNonStopTcpClient.remoteEP = value; } }

        Boolean correctionFlag = false;
        WacohForceVector centerForceVector = new WacohForceVector();

        WacohForceEnumConnectionStatus _connectionStatus = WacohForceEnumConnectionStatus.Disconnection;
        WacohForceEnumConnectionStatus connectionStatus
        {
            get { return _connectionStatus; }
            set
            {
                try
                {
                    Monitor.TryEnter(this, 5000);
                    this._connectionStatus = value;
                }
                finally { Monitor.Exit(this); }
            }
        }


        public WacohForceLdd()
        {
            this.netNonStopTcpClient.EhDataReceive += (sender, e) =>
            {
                var ee = e as CtkNonStopTcpStateEventArgs;
                var msg = ee.TrxMessageBuffer;
                this.messageReceiver.Receive(msg.Buffer, msg.Offset, msg.Length);
                this.messageReceiver.AnalysisMessage();

                if (this.messageReceiver.Count == 0) return;

                var vec = this.messageReceiver.Dequeue();
                if (this.correctionFlag)
                {
                    this.centerForceVector = vec;
                    lock (this)
                        correctionFlag = false;
                }

                var ea = new WacohForceMessageEventArgs();
                ea.centerForceVector = this.centerForceVector;
                ea.rawForceVector = vec;
                this.OnDataReceive(ea);
            };


            this.netNonStopTcpClient.EhDisconnect += (sender, e) =>
            {
                this.connectionStatus = WacohForceEnumConnectionStatus.Disconnection;
            };
            this.netNonStopTcpClient.EhFirstConnect += (sender, e) =>
            {
                this.connectionStatus = WacohForceEnumConnectionStatus.Connected;
            };

        }

        ~WacohForceLdd() { this.Dispose(false); }


        public int ConnectIfNo()
        {
            if (this.remoteEP == null) return -1;


            if (this.connectionStatus == WacohForceEnumConnectionStatus.Connecting) return 1;

            this.netNonStopTcpClient.ConnectIfNo();
            if (this.netNonStopTcpClient.IsLocalReadyConnect)
                this.connectionStatus = WacohForceEnumConnectionStatus.Connecting;

            return 0;
        }




        public int Close()
        {
            if (this.netNonStopTcpClient != null)
                this.netNonStopTcpClient.Disconnect();


            return 0;
        }


        public bool IsConnect()
        {
            return this.netNonStopTcpClient.IsRemoteConnected;
        }





        public void SendCmd_RequestData()
        {
            this.netNonStopTcpClient.WriteMsg("R");
        }

        public void SendCmd_CorrectRequestData()
        {
            lock (this)
                this.correctionFlag = true;

            this.netNonStopTcpClient.WriteMsg("R");
        }








        #region Event

        public void CleanEvent()
        {
            try
            {
                foreach (Delegate d in this.evtDataReceive.GetInvocationList())
                {
                    this.evtDataReceive -= (EventHandler<WacohForceMessageEventArgs>)d;
                }
            }
            catch (Exception ex) { MvLog.Write(ex); }
        }





        public event EventHandler<WacohForceMessageEventArgs> evtDataReceive;
        void OnDataReceive(WacohForceMessageEventArgs ea)
        {
            if (this.evtDataReceive == null) return;
            this.evtDataReceive(this, ea);
        }


        #endregion




        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
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
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        void DisposeManaged()
        {
        }

        void DisposeUnmanaged()
        {

        }

        void DisposeSelf()
        {
            this.Close();
        }

        #endregion




        public int DdZeroCorrect(IEnumerable<float> input, IEnumerable<float> offset, IEnumerable<float> result)
        {
            throw new NotImplementedException();
        }
    }

}
