﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("82C726E9-13C6-443C-85C8-2D34F4413D38")]
    public interface IHalVibration : IMacHalComponent
    {
        /// <summary>
        /// 讀取[震動感測器] value
        /// </summary>
        /// <returns>震動value, unit: float</returns>
        float GetVibrationValue();
    }
}
