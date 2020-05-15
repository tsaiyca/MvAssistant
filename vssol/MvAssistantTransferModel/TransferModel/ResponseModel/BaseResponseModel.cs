using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistantTransferModel.ResponseModel
{
    public abstract class BaseResponseModel
    {
        /// <summary>執行結果</summary>
        public ResponseResultType ResponseResult { get; set; }
        /// <summary>訊息</summary>
        public string Message { get;set; }
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
