using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.OperateModel.TransferModel.RequestModel.MaskRobot
{
    /// <summary>Mask Robot/Connet Request</summary>
    public class MaskRobot_Connect_RequestModel: BaseMaskRobotRequestModel
    {
        /// <summary>Robot IP</summary>
        public string RobotIP { get; set; }
    }
}
