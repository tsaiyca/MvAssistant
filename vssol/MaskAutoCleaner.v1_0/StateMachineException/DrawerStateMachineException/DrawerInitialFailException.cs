﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.DrawerStateMachineException
{
    public class DrawerInitialFailException:StateMachineExceptionBase
    {
        public DrawerInitialFailException(string message):base(EnumStateMachineExceptionCode.DrawerInitialFailException)
        {

        }
        public DrawerInitialFailException() : this("")
        {

        }
    }
}
