﻿using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AutoSwitch;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.FiberOptic;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MachineManifest(EnumDevice.openstage_assembly)]
    [GuidAttribute("66F97306-5CB8-4188-B835-CDC87DF1EF23")]
    public class HalOpenStage : HalAssemblyBase, IHalOpenStage
    {


        #region Device Components (請看範例說明)


        [MachineManifest(EnumDevice.openstage_ccd_side_1)]
        public IHalCamera Openstage_ccd_side_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_ccd_top_1)]
        public IHalCamera Openstage_ccd_top_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_ccd_front_1)]
        public IHalCamera Openstage_ccd_front_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_ccd_barcode_1)]
        public IHalCamera Openstage_ccd_barcode_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_lightbar_top_1)]
        public IHalLight Openstage_lightbar_top_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_lightbar_barcode_1)]
        public IHalLight Openstage_lightbar_barcode_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_particle_counter_1)]
        public IHalParticleCounter Openstage_particle_counter_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_fiber_optic_open_box_1)]
        public IHalFiberOptic Openstage_fiber_optic_open_box_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_fiber_optic_box_category_1)]
        public IHalFiberOptic Openstage_fiber_optic_box_category_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_box_holder_1)]
        public IHalCylinder Openstage_box_holder_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_box_holder_2)]
        public IHalCylinder Openstage_box_holder_2 { get; set; }
        [MachineManifest(EnumDevice.openstage_auto_switch_1)]
        public IHalAutoSwitch Openstage_auto_switch_1 { get; set; }
        [MachineManifest(EnumDevice.openstage_auto_switch_2)]
        public IHalAutoSwitch Openstage_auto_switch_2 { get; set; }


        #endregion Device Components

        #region HAL Interface Functions
        int IHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }


        #endregion HAL Interface Functions

     


    }
}
