using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.OperateModel.TransferModel.ResponseModel
{
    /// <summary>操作結果基礎類別</summary>
    public abstract class BaseResponseModel
    {
        /// <summary>執行結果</summary>
        public ResponseResultType ResponseResult { get; set; }
        /// <summary>訊息</summary>
        public string Message { get;set; }
        
        /// <summary>轉換為JSon string</summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
