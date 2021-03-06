﻿using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public class MacMsInspectionCh : MacMachineStateBase
    {
        private IMacHalInspectionCh HalInspectionCh { get { return this.halAssembly as IMacHalInspectionCh; } }
        private IMacHalUniversal HalUniversal { get { return this.Mediater.MachineMgr.CtrlMachines[EnumMachineID.MID_UNI_A_ASB.ToString()].halAssembly as IMacHalUniversal; } }

        private MacState _currentState = null;

        public void ResetState()
        { this.States[EnumMacMsInspectionChState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        private void SetCurrentState(MacState state)
        { _currentState = state; }

        public MacState CurrentState { get { return _currentState; } }

        public MacMsInspectionCh() { LoadStateMachine(); }

        MacInspectionChUnitStateTimeOutController timeoutObj = new MacInspectionChUnitStateTimeOutController();

        /// <summary> 狀態機啟動 </summary>
        public void SystemBootup()
        {
            this.States[EnumMacMsInspectionChState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        /// <summary> Inspection Chamber初始化 </summary>
        public void Initial()
        {
            this.States[EnumMacMsInspectionChState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        /// <summary> 檢測Pellicle </summary>
        public void InspectPellicle()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToInspectPellicle.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> Mask被取出後將狀態改為Idle ( 必須先由Mask Transfer取出Mask ) </summary>
        public void ReturnToIdleAfterReleasePellicle()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleasePellicle.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        /// <summary> 檢測Glass </summary>
        public void InspectGlass()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToInspectGlass.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> Mask被取出後將狀態改為Idle ( 必須先由Mask Transfer取出Mask ) </summary>
        public void ReturnToIdleAfterReleaseGlass()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleaseGlass.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsInspectionChState.Start);
            MacState sInitial = NewState(EnumMacMsInspectionChState.Initial);

            MacState sIdle = NewState(EnumMacMsInspectionChState.Idle);
            MacState sPellicleOnStage = NewState(EnumMacMsInspectionChState.MaskOnStage);
            MacState sDefensingPellicle = NewState(EnumMacMsInspectionChState.DefensingMask);
            MacState sInspectingPellicle = NewState(EnumMacMsInspectionChState.InspectingMask);
            MacState sPellicleOnStageInspected = NewState(EnumMacMsInspectionChState.MaskOnStageInspected);
            MacState sWaitingForReleasePellicle = NewState(EnumMacMsInspectionChState.WaitingForReleaseMask);

            MacState sGlassOnStage = NewState(EnumMacMsInspectionChState.GlassOnStage);
            MacState sDefensingGlass = NewState(EnumMacMsInspectionChState.DefensingGlass);
            MacState sInspectingGlass = NewState(EnumMacMsInspectionChState.InspectingGlass);
            MacState sGlassOnStageInspected = NewState(EnumMacMsInspectionChState.GlassOnStageInspected);
            MacState sWaitingForReleaseGlass = NewState(EnumMacMsInspectionChState.WaitingForReleaseGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsInspectionChTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsInspectionChTransition.Initial);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacMsInspectionChTransition.StandbyAtIdle);

            MacTransition tIdle_PellicleOnStage = NewTransition(sIdle, sPellicleOnStage, EnumMacMsInspectionChTransition.ReceiveTriggerToInspectPellicle);
            MacTransition tPellicleOnStage_DefensingPellicle = NewTransition(sPellicleOnStage, sDefensingPellicle, EnumMacMsInspectionChTransition.DefensePellicle);
            MacTransition tDefensingPellicle_InspectingPellicle = NewTransition(sDefensingPellicle, sInspectingPellicle, EnumMacMsInspectionChTransition.InspectPellicle);
            MacTransition tInspectingPellicle_PellicleOnStageInspected = NewTransition(sInspectingPellicle, sPellicleOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithPellicleInspected);
            MacTransition tPellicleOnStageInspected_WaitingForReleasePellicle = NewTransition(sPellicleOnStageInspected, sWaitingForReleasePellicle, EnumMacMsInspectionChTransition.WaitForReleasePellicle);
            MacTransition tWaitingForReleasePellicle_NULL = NewTransition(sWaitingForReleasePellicle, null, EnumMacMsInspectionChTransition.StandbyAtWaitForReleasePellicle);
            MacTransition tWaitingForReleasePellicle_Idle = NewTransition(sWaitingForReleasePellicle, sIdle, EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleasePellicle);

            MacTransition tIdle_GlassOnStage = NewTransition(sIdle, sGlassOnStage, EnumMacMsInspectionChTransition.ReceiveTriggerToInspectGlass);
            MacTransition tGlassOnStage_DefensingGlass = NewTransition(sGlassOnStage, sDefensingGlass, EnumMacMsInspectionChTransition.DefenseGlass);
            MacTransition tDefensingGlass_InspectingGlass = NewTransition(sDefensingGlass, sInspectingGlass, EnumMacMsInspectionChTransition.InspectGlass);
            MacTransition tInspectingGlass_GlassOnStageInspected = NewTransition(sInspectingGlass, sGlassOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithGlassInspected);
            MacTransition tGlassOnStageInspected_WaitingForReleaseGlass = NewTransition(sGlassOnStageInspected, sWaitingForReleaseGlass, EnumMacMsInspectionChTransition.WaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_NULL = NewTransition(sWaitingForReleaseGlass, null, EnumMacMsInspectionChTransition.StandbyAtWaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_Idle = NewTransition(sWaitingForReleaseGlass, sIdle, EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleaseGlass);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tStart_Initial;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sStart.OnExit += (sender, e) =>
            { };
            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalInspectionCh.Initial();
                }
                catch (Exception ex)
                {
                    throw new InspectionChInitialFailException(ex.Message);
                }

                var transition = tInitial_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInitial.OnExit += (sender, e) =>
            { };
            sIdle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalInspectionCh.XYPosition(0, 0);
                    HalInspectionCh.WPosition(0);
                }
                catch (Exception ex)
                {
                    throw new InspectionChPLCExecuteFailException(ex.Message);
                }

                var transition = tIdle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdle.OnExit += (sender, e) =>
            { };


            sPellicleOnStage.OnEntry += (sender, e) =>
{
    SetCurrentState((MacState)sender);

    CheckEquipmentStatus();
    CheckAssemblyAlarmSignal();
    CheckAssemblyWarningSignal();

    try
    {
    }
    catch (Exception ex)
    {
        throw new InspectionChException(ex.Message);
    }

    var transition = tPellicleOnStage_DefensingPellicle;
    TriggerMember triggerMember = new TriggerMember
    {
        Guard = () =>
        {
            return true;
        },
        Action = null,
        ActionParameter = null,
        ExceptionHandler = (thisState, ex) =>
        { // TODO: do something
                    },
        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
        ThisStateExitEventArgs = new MacStateExitEventArgs(),
    };
    transition.SetTriggerMembers(triggerMember);
    Trigger(transition);
};
            sPellicleOnStage.OnExit += (sender, e) =>
            { };
            sDefensingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    //上方相機
                    HalInspectionCh.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "bmp");
                    Thread.Sleep(500);

                    //側邊相機
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChDefenseFailException(ex.Message);
                }

                var transition = tDefensingPellicle_InspectingPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDefensingPellicle.OnExit += (sender, e) =>
            { };
            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalInspectionCh.ZPosition(-29.6);
                    //上方相機
                    for (int i = 158; i <= 296; i += 23)
                    {
                        for (int j = 123; j <= 261; j += 23)
                        {
                            HalInspectionCh.XYPosition(i, j);
                            HalInspectionCh.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                            Thread.Sleep(500);
                        }
                    }

                    //側邊相機
                    HalInspectionCh.XYPosition(246, 208);
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChInspectFailException(ex.Message);
                }

                var transition = tInspectingPellicle_PellicleOnStageInspected;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingPellicle.OnExit += (sender, e) =>
            { };
            sPellicleOnStageInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalInspectionCh.XYPosition(0, 0);
                    HalInspectionCh.ZPosition(0);
                }
                catch (Exception ex)
                {
                    throw new InspectionChPLCExecuteFailException(ex.Message);
                }

                var transition = tPellicleOnStageInspected_WaitingForReleasePellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sPellicleOnStageInspected.OnExit += (sender, e) =>
            { };
            sWaitingForReleasePellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tWaitingForReleasePellicle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sWaitingForReleasePellicle.OnExit += (sender, e) =>
            { };


            sGlassOnStage.OnEntry += (sender, e) =>
{
    SetCurrentState((MacState)sender);

    CheckEquipmentStatus();
    CheckAssemblyAlarmSignal();
    CheckAssemblyWarningSignal();

    try
    {
    }
    catch (Exception ex)
    {
        throw new InspectionChException(ex.Message);
    }

    var transition = tGlassOnStage_DefensingGlass;
    TriggerMember triggerMember = new TriggerMember
    {
        Guard = () =>
        {
            return true;
        },
        Action = null,
        ActionParameter = null,
        ExceptionHandler = (thisState, ex) =>
        { // TODO: do something
                    },
        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
        ThisStateExitEventArgs = new MacStateExitEventArgs(),
    };
    transition.SetTriggerMembers(triggerMember);
    Trigger(transition);
};
            sGlassOnStage.OnExit += (sender, e) =>
            { };
            sDefensingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    //上方相機
                    HalInspectionCh.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "bmp");
                    Thread.Sleep(500);

                    //側邊相機
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChDefenseFailException(ex.Message);
                }

                var transition = tDefensingGlass_InspectingGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDefensingGlass.OnExit += (sender, e) =>
            { };
            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalInspectionCh.ZPosition(-29.6);
                    //上方相機
                    for (int i = 158; i <= 296; i += 23)
                    {
                        for (int j = 123; j <= 261; j += 23)
                        {
                            HalInspectionCh.XYPosition(i, j);
                            HalInspectionCh.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                            Thread.Sleep(500);
                        }
                    }

                    //側邊相機
                    HalInspectionCh.XYPosition(246, 208);
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChInspectFailException(ex.Message);
                }

                var transition = tInspectingGlass_GlassOnStageInspected;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingGlass.OnExit += (sender, e) =>
            { };
            sGlassOnStageInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalInspectionCh.XYPosition(0, 0);
                    HalInspectionCh.ZPosition(0);
                }
                catch (Exception ex)
                {
                    throw new InspectionChPLCExecuteFailException(ex.Message);
                }

                var transition = tGlassOnStageInspected_WaitingForReleaseGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sGlassOnStageInspected.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tWaitingForReleaseGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sWaitingForReleaseGlass.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
        }

        private bool CheckEquipmentStatus()
        {
            string Result = null;
            if (HalUniversal.ReadPowerON() == false) Result += "Equipment is power off now, ";
            if (HalUniversal.ReadBCP_Maintenance()) Result += "Key lock in the electric control box is turn to maintenance, ";
            if (HalUniversal.ReadCB_Maintenance()) Result += "Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance, ";
            if (HalUniversal.ReadBCP_EMO().Item1) Result += "EMO_1 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item2) Result += "EMO_2 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item3) Result += "EMO_3 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item4) Result += "EMO_4 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item5) Result += "EMO_5 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item1) Result += "EMO_6 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item2) Result += "EMO_7 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item3) Result += "EMO_8 has been trigger, ";
            if (HalUniversal.ReadLP1_EMO()) Result += "Load Port_1 EMO has been trigger, ";
            if (HalUniversal.ReadLP2_EMO()) Result += "Load Port_2 EMO has been trigger, ";
            if (HalUniversal.ReadBCP_Door()) Result += "The door of electric control box has been open, ";
            if (HalUniversal.ReadLP1_Door()) Result += "The door of Load Port_1 has been open, ";
            if (HalUniversal.ReadLP2_Door()) Result += "The door of Load Pord_2 has been open, ";
            if (HalUniversal.ReadBCP_Smoke()) Result += "Smoke detected in the electric control box, ";

            if (Result == null)
                return true;
            else
                throw new UniversalEquipmentException(Result);
        }

        private bool CheckAssemblyAlarmSignal()
        {
            //var CB_Alarm = HalUniversal.ReadAlarm_Cabinet();
            //var CC_Alarm = HalUniversal.ReadAlarm_CleanCh();
            //var CF_Alarm = HalUniversal.ReadAlarm_CoverFan();
            //var BT_Alarm = HalUniversal.ReadAlarm_BTRobot();
            //var MTClampInsp_Alarm = HalUniversal.ReadAlarm_MTClampInsp();
            //var MT_Alarm = HalUniversal.ReadAlarm_MTRobot();
            var IC_Alarm = HalUniversal.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            //var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            //if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            //if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            //if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            //if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
            //if (LP_Alarm != "") throw new LoadportPLCAlarmException(LP_Alarm);
            //if (OS_Alarm != "") throw new OpenStagePLCAlarmException(OS_Alarm);

            return true;
        }

        private bool CheckAssemblyWarningSignal()
        {
            //var CB_Warning = HalUniversal.ReadWarning_Cabinet();
            //var CC_Warning = HalUniversal.ReadWarning_CleanCh();
            //var CF_Warning = HalUniversal.ReadWarning_CoverFan();
            //var BT_Warning = HalUniversal.ReadWarning_BTRobot();
            //var MTClampInsp_Warning = HalUniversal.ReadWarning_MTClampInsp();
            //var MT_Warning = HalUniversal.ReadWarning_MTRobot();
            var IC_Warning = HalUniversal.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            //var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            //if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            //if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            //if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            //if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            //if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }

        public class MacInspectionChUnitStateTimeOutController
        {
            const int defTimeOutSec = 20;
            public bool IsTimeOut(DateTime startTime, int targetDiffSecs)
            {
                var thisTime = DateTime.Now;
                var diff = thisTime.Subtract(startTime).TotalSeconds;
                if (diff >= targetDiffSecs)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsTimeOut(DateTime startTime)
            {
                return IsTimeOut(startTime, defTimeOutSec);
            }
        }
    }
}
