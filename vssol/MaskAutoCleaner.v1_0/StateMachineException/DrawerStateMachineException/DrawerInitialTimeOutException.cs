﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.DrawerStateMachineException
{
    public class DrawerInitialTimeOutException : StateMachineExceptionBase
    {
        public DrawerInitialTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerInitialTimeOutException, message)
        {
        }
        public DrawerInitialTimeOutException() : this("")
        {

        }
    }
}
