using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistantTransferModel.RequestModel.MaskRobot
{
   public class MaskRobot_Connect_RequestModel: BaseMaskRobotRequest
    {
        public string RobotIP { get; set; }
    }
}
