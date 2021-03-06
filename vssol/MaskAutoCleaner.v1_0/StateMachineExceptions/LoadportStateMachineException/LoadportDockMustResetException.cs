﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportDockMustResetException : StateMachineExceptionBase
    {
        public LoadportDockMustResetException(string message) : base(EnumStateMachineExceptionCode.LoadportDockMustResetException, message)
        {

        }
        public LoadportDockMustResetException() : this("")
        {

        }
    }
}
