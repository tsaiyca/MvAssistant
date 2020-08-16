﻿using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineException;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine
{
    /// <summary>ta
    /// Machine State Object Base
    /// Design Pattern - State Pattern
    /// </summary>
    public abstract class MacMachineStateBase : MacStateMachine
    {
        //不需實作IMvContextFlow, 因為只有初始化StateMachine, 沒有其它作業
        //不需實作IDisposable, 因為沒有



        protected MacHalAssemblyBase halAssembly;

        public virtual void Load()
        {
            this.LoadStateMachine();
        }

        public abstract void LoadStateMachine();

        public MacState NewState(Enum name)
        {
            var state = new MacState(name.ToString());
            this.States[state.Name] = state;
            return state;
        }

        public MacTransition NewTransition(MacState from, MacState to, Enum name)
        {
            var transition = new MacTransition(name.ToString(), from, to);
            this.Transitions[transition.Name] = transition;
            return transition;
        }


        /// <summary></summary>
        /// <param name="guard">guard (Func delegate) </param>
        /// <param name="action">action(Action delegate)</param>
        /// <param name="actionParameter">action Parameter (Object)</param>
        /// <param name="exceptionHandler">Exception Handler(Action delegate)</param>
        public void TriggerAsync(Func<DateTime, StateGuardRtns> guard, Action<object> action,object actionParameter,Action<Exception> exceptionHandler)
        {
            Action trigger = () =>
            {
                try
                {
                    DateTime startTime = DateTime.Now;
                    while (true)
                    {
                        StateGuardRtns rtn = guard(startTime);
                        if (rtn != null)
                        {
                            if (action != null)
                            {
                                
                                action.Invoke(actionParameter);
                            }
                            var State = rtn.ThisState;
                            var nextState = rtn.NextState;
                            State.DoExit(rtn.ThisStateExitEventArgs);
                            if (nextState != null)
                            {
                                nextState.DoEntry(rtn.NextStateEntryEventArgs);
                            }
                            break;
                        }
                        Thread.Sleep(10);
                    }
                }
               catch(Exception ex)
                {
                    if(exceptionHandler != null)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            };
            new Task(trigger).Start();

        }

        public void Trigger(MacTransition transition)
        {
            TriggerMember triggerMember = (TriggerMember)transition.TriggerMembers;
            try
            {
              
                if (triggerMember.Guard())
                {
                    if (triggerMember.Action != null)
                    {
                        triggerMember.Action(triggerMember.ActionParameter);
                    }
                    var thisState = transition.StateFrom;
                    var nextState = transition.StateTo;
                    thisState.DoExit(triggerMember.ThisStateExitEventArgs);
                    nextState.DoEntry(triggerMember.NextStateEntryEventArgs);
                }
                else
                {
                    if(triggerMember.NotGuardException != null)
                    {
                        throw triggerMember.NotGuardException;
                    }
                }
            }
            catch (Exception ex)
            {
                if(triggerMember.ExceptionHandler !=null)
                {
                    triggerMember.ExceptionHandler.Invoke(ex);
                }
            }
        }
        public void TriggerAsync(MacTransition transition)
        {

        }

        /// <summary></summary>
        /// <param name="guard">guard (Func delegate)</param>
        /// <param name="action">action (Action delegate)</param>
        /// <param name="actionParameter">action parameter(object)</param>
        /// <param name="exceptionHndler">Exception Handler (Action delegate)</param>
        public void Trigger(Func<StateGuardRtns> guard, Action<object> action, object actionParameter, Action<Exception> exceptionHndler)
        {
            try
            {
                var guardRtns = guard();
              
                    if (guardRtns != null)
                    {
                        if (action != null)
                        {
                            action(actionParameter);
                        }
                        var state = guardRtns.ThisState;
                        var nextState = guardRtns.NextState;
                        var stateExitArgs = guardRtns.ThisStateExitEventArgs;
                        var nextStateEntryArgs = guardRtns.NextStateEntryEventArgs;
                        state.DoExit(stateExitArgs);
                        if (nextState != null)
                        {
                            nextState.DoEntry(nextStateEntryArgs);
                        }
                    }
                
            }
            catch(Exception ex)
            {
                if(exceptionHndler != null)
                {
                    exceptionHndler.Invoke(ex);
                }
            }
        }

       
    }
}
