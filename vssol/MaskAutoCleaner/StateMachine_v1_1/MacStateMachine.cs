﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateMachine_v1_1
{

    public class MacStateMachine
    {
        protected Dictionary<String, MacState> States = new Dictionary<string, MacState>();
        protected Dictionary<String, MacTransition> Transitions = new Dictionary<string, MacTransition>();


    }
}
