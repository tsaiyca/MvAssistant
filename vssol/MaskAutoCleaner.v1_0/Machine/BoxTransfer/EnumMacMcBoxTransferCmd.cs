﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
   public  enum EnumMacMcBoxTransferCmd
    {
        SystemBootup,
        MoveToLock,
        MoveToUnlock,
        Initial,
        BankOut,
        MoveToCabinetGet,
    }
}
