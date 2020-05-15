//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.OperateModel.OperateException
{
    /// <summary>自訂例外的基礎類別</summary>
    public abstract  class BaseException:Exception
    {
        /// <summary>建構式</summary>
        /// <param name="message">訊息</param>
        public BaseException(string message) : base(message)
        {

        }
    }
}