using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.OperateModel.TransferModel.ResponseModel
{
    /// <summary>執行結果</summary>
    public enum ResponseResultType
    {
        /// <summary>執行結果正常未發生例外、需回傳資料集時資料集不為空集合</summary>
        OK =0,
        /// <summary>執行結果正常未發生例外、但需回傳資料集時資料集為空集合</summary>
        EmptyData,
        /// <summary>發生例外</summary>
        ExceptionOccurred
    }
}
