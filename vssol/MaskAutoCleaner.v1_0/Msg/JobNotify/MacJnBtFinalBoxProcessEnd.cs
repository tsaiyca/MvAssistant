﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.JobNotify
{
    public class MacJnBtFinalBoxProcessEnd : MacJobNotifyBase
    {
        public List<BoxInfo> BoxInfoList;
    }
}
