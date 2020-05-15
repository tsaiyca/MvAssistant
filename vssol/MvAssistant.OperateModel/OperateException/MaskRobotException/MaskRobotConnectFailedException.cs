using MvAssistant.OperateModel.OperateException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.OperateModel.OperateException.MaskRobotException
{
    /// <summary>Mask Robot Connect 失敗</summary>
    public class MaskRobotConnectFailedException:BaseException
    {
        /// <summary>Mask Robot Connect 失敗</summary>
        public MaskRobotConnectFailedException() : base("Connection Fail")
        {

        }
        /// <summary>Mask Robot Connect 失敗</summary>
        /// <param name="message">訊息</param>
        public MaskRobotConnectFailedException(string message) : base(message)
        {

        }
    }
}
