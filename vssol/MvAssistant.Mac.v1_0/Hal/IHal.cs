﻿using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac
{
    /// <summary>
    /// HAL (Hardware Abstract Layer) Interface
    /// </summary>
    [GuidAttribute("A11710A1-CF37-4D4D-835C-323BAEC46658")]
    public interface IHal : IManifestMachine
    {
        /// <summary>
        /// implement connection terminate in here
        /// </summary>
        /// <returns>0: success; 1: common fail; 2以上的數字各device可自行定義</returns>
        int HalClose();

        /// <summary>
        /// implement device connect / initialize in here
        /// </summary>
        /// <returns>0: success; 1: common fail; 2以上的數字各device可自行定義</returns>
        int HalConnect();

        /// <summary>
        /// device是否連線正常 / session正常
        /// </summary>
        /// <returns>true: still alive; false: zombie</returns>
        bool HalIsConnected();
    }
}
