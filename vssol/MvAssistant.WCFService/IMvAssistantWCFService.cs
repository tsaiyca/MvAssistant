using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Manifest;


namespace MvAssistant.WCFService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IService1"。
    [ServiceContract]
    public interface IMvAssistantWCFService
    {
        [OperationContract]
        string GetData(string value);

      //  [OperationContract]
      //  CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此新增您的服務作業
       [OperationContract]
       int UtHalMaskTransfer_TestPathMove();
    }

    // 使用下列範例中所示的資料合約，新增複合型別至服務作業。
    // 您可以將 XSD 檔案加入專案。建置專案後，您可以直接以命名空間 "MvAssistant.WCFService.ContractType" 使用該處定義的資料型別。
    /*
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    */
}
