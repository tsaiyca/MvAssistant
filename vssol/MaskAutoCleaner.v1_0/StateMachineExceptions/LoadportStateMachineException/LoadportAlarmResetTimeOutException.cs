﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportAlarmResetTimeOutException : StateMachineExceptionBase
    {
        public LoadportAlarmResetTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportAlarmResetTimeOutException, message)
        {

        }
        public LoadportAlarmResetTimeOutException() : this("")
        {

        }
    }
}
