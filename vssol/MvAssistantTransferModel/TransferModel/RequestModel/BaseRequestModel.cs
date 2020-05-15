using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistantTransferModel.TransferModel.RequestModel
{
   public abstract class BaseRequestModel
   {
     public string ToJson()
     {
            var rtnJson = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return rtnJson;

     }
   }
}
