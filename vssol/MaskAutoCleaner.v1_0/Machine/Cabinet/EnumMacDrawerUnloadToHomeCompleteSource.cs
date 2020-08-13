﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum  EnumMacDrawerUnloadToHomeCompleteSource
    {
        /// <summary>從 GotoHomeIng 而來</summary>
        GotoHomeIng,
        /// <summary>從 Check Box Exist 而來</summary>
        UnloadCheckBoxExist,
        /// <summary>從 Check Box Not Exist 而來</summary>
        UnloadCheckBoxNotExist
    }
}
