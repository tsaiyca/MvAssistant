using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.OperateLogic;
using MvAssistant.OperateModel.TransferModel.RequestModel.MaskRobot;
using MvAssistant.OperateModel.TransferModel.ResponseModel;
using MvAssistant.OperateModel.TransferModel.ResponseModel.MaskRobot;
//using MvAssistant.WCFService.BusinessLogic;
//using MvAssistantTransferModel.TransferModel.RequestModel.MaskRobot;
//using MvAssistantTransferModel.TransferModel.ResponseModel;
//using MvAssistantTransferModel.TransferModel.ResponseModel.MaskRobot;

namespace MvAssistant.WCFService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的類別名稱 "Service1"。
    public class MvAssistantWCFService : IMvAssistantWCFService
    {
        #region Example
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }
       
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        #endregion

        public string MaskRobot_Connect(string requestJson)
        {
            var rtnJson = default(string);
            var responseModel = default(MaskRobot_Connect_ResponseModel);
            try
            {
             //   var requestModel = Newtonsoft.Json.JsonConvert.DeserializeObject<MaskRobot_Connect_RequestModel>(requestJson);
                var feedBack = new MaskRobotOperateLogic().Connect<MaskRobot_Connect_RequestModel>(requestJson);
                responseModel = new MaskRobot_Connect_ResponseModel
                {
                    Message = "Connection Success",
                    ResponseResult=feedBack
                };
            }
            catch(Exception ex)
            {
                responseModel = new MaskRobot_Connect_ResponseModel
                {
                    Message = ex.Message,
                    ResponseResult = ResponseResultType.ExceptionOccurred,
                };
            }
            rtnJson= responseModel.ToJson();
            return rtnJson;
        }


        public string UtHalMaskTransfer_TestPathMove()
        {
            string rtnJson= default(string);
            var respModel = default(MaskRobot_Connect_ResponseModel);
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.Load();


                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;

                    if (mt.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }
                    mt.RobotMove(mt.HomeToOpenStage());
                    mt.RobotMove(mt.OpenStageToHome());
                    mt.ChangeDirection(mt.PosToInspCh());
                    mt.RobotMove(mt.FrontSideIntoInspCh());
                    mt.RobotMove(mt.FrontSideLeaveInspCh());
                    mt.RobotMove(mt.BackSideIntoInspCh());
                    mt.RobotMove(mt.BackSideLeaveInspCh());
                    mt.ChangeDirection(mt.PosToCleanCh());
                    mt.RobotMove(mt.BackSideClean());
                    mt.RobotMove(mt.FrontSideClean());
                    mt.RobotMove(mt.FrontSideCCDTakeImage());
                    mt.RobotMove(mt.BackSideCCDTakeImage());
                    mt.ChangeDirection(mt.PosHome());
                    //mt.Robot.HalMoveAsyn();
                    //mt.HalMoveAsyn();
                    mt.Clamp();
                    respModel = new MaskRobot_Connect_ResponseModel
                    {
                        ResponseResult = ResponseResultType.OK,
                    };
                }
            }
            catch (Exception ex)
            {
                respModel = new MaskRobot_Connect_ResponseModel
                {
                    Message = ex.Message,
                    ResponseResult = ResponseResultType.ExceptionOccurred,
                };
            }
            rtnJson = respModel.ToJson();
            return rtnJson;
        }
    }
}
