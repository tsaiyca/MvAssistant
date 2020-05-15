using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.OperateModel.TransferModel.RequestModel
{
    /// <summary>Request 資料基礎類別</summary>
    public abstract class BaseRequestModel
   {

        /// <summary>轉換為 JSon string</summary>
        /// <returns></returns>
        public string ToJson()
       {
            var rtnJson = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return rtnJson;
        }
   }
}
