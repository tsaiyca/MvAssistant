﻿using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>ReplySetSpeed事件程序的 Event Args</summary>
    public class OnReplySetSpeedEventArgs : EventArgs
    {
        public ReplyResultCode ReplyResultCode { get; private set; }
        private OnReplySetSpeedEventArgs() { }
        public OnReplySetSpeedEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = replyResultCode; }
    }
}
