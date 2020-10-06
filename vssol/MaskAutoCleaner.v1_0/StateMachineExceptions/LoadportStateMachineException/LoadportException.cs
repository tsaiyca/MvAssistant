﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
   public  class LoadportException:StateMachineExceptionBase
    {
        public LoadportException(string message) : base(EnumStateMachineExceptionCode.LoadportException, message)
        {

        }
        public LoadportException() : this("")
        {

        }
    }
}
