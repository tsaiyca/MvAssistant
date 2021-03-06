﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("B94641B4-C070-47B6-8498-CE4CCE0D5574")]
    public interface IHalDiffPressure : IMacHalComponent
    {
        /// <summary>
        /// 讀取[壓差計] value
        /// </summary>
        /// <returns>pressure value, unit: float</returns>
        float GetPressureValue();
    }
}
