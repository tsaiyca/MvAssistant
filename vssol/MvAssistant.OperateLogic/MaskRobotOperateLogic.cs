//using MvAssistant.OperateModel.MaskRobotException;
using MvAssistant.Mac.v1_0.Hal.CompRobotTest;
using MvAssistant.OperateModel.OperateException.MaskRobotException;
using MvAssistant.OperateModel.TransferModel.RequestModel.MaskRobot;
using MvAssistant.OperateModel.TransferModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.OperateLogic
{
   public  class MaskRobotOperateLogic
    {
       
        public ResponseResultType Connect<TRequestModel>(string requestJson) where TRequestModel: MaskRobot_Connect_RequestModel
        {
            var rtnV = ResponseResultType.ExceptionOccurred;
            var requestModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TRequestModel>(requestJson);
            rtnV = Connect(requestModel);
            return rtnV;
        }

        /// <summary>Mask Robot Connect</summary>
        /// <param name="requestModel"></param>
        /// <exception cref="MaskRobotConnectFailedException"></exception>
        /// <returns></returns>
        public ResponseResultType Connect(MaskRobot_Connect_RequestModel requestModel) 
        {
            var rtnV = ResponseResultType.ExceptionOccurred;
           
            var robotHandler = new MacHalMaskRobotFanuc();
            robotHandler.ldd.RobotIp = requestModel.RobotIP;
            if (robotHandler.ConnectIfNO() == 0)
            {
                rtnV = ResponseResultType.OK;
            }
            else
            {
                //  this.LogWrite("Connection Fail");
                robotHandler.Close();
                throw new MaskRobotConnectFailedException();
            }

            return rtnV;
        }
    }
}
