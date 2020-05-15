using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace MvAssistant.WCFService.Host
{
   public  class MvAssistantWCFServiceHostInstance
    {    // netsh http add urlacl url=http://+:8002/ user="\Everyone"
        /// <summary>Binding 位址</summary>
        private static string HttpBinding = "http://127.0.0.1:8002/";
        /// <summary>Service Host 實體</summary>
        private static ServiceHost _wcfInstance = null;
        /// <summary>Lock Object</summary>
        private static readonly Object lockObj = new object();
        /// <summary>Service Name </summary>
        private const string ServiceName = "WCFService";
        /// <summary>Service Url</summary>
        public readonly static string ServiceUrl = HttpBinding + ServiceName;

        /// <summary>取得 WCF Service Host</summary>
        /// <returns></returns>
        public static ServiceHost GetInstance(string httpBindingAddr=null)
        {
            HttpBinding = httpBindingAddr != null ? httpBindingAddr : HttpBinding;
            if (_wcfInstance == null)
            {
                lock (lockObj)
                {
                    if (_wcfInstance == null)
                    {
                        _wcfInstance = new ServiceHost(typeof(MvAssistantWCFService));
                        System.ServiceModel.Channels.Binding httpBinding = new BasicHttpBinding();
                        _wcfInstance.AddServiceEndpoint(typeof(IMvAssistantWCFService), httpBinding, HttpBinding);
                     
                        if (_wcfInstance.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
                        {
                            // 行為
                            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                            behavior.HttpGetEnabled = true;

                            // Address
                            behavior.HttpGetUrl = new Uri(ServiceUrl);
                            _wcfInstance.Description.Behaviors.Add(behavior);

                            //boot
                            _wcfInstance.Open();
                        }
                    }
                }
            }
            return _wcfInstance;
        }

        public static void StopService()
        {
            if(_wcfInstance != null)
            {
                try
                {
                    _wcfInstance.Close();
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}