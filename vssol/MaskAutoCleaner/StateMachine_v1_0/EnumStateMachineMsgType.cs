﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.StateMachine_v1_0
{
    public enum EnumStateMachineMsgType
    {
        GuardFail, 
        CommandSuccessful,
        RecipeInsertQueue,
        StateMachineException,
        NoTransitionName,
        NotCurrentStateTransition,
        StateMachineFatel,
        MachineRunningControl,
        StateEventError,
        PluralTriggerSameTime
    }
}
