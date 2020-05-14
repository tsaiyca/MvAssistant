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
    {
        private const string HttpBinding = "http://127.0.0.1:8002/";
        private static ServiceHost _wcfInstance = null;
        private static readonly Object lockObj = new object();
        private const string ServiceName = "WCFService";
        public readonly static string ServiceUrl = HttpBinding + ServiceName;
        public static ServiceHost GetInstance()
        {
            if (_wcfInstance == null)
            {
                lock (lockObj)
                {
                    if (_wcfInstance == null)
                    {
                        _wcfInstance = new ServiceHost(typeof(MvAssistantWCFService));
                        System.ServiceModel.Channels.Binding httpBinding = new BasicHttpBinding();
                        _wcfInstance.AddServiceEndpoint(typeof(IMvAssistantWCFService), httpBinding, HttpBinding);
                        //  _wcfInstance.AddServiceEndpoint(typeof(IWCFService), httpBinding, "http://127.0.0.1:8009/");
                        if (_wcfInstance.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
                        {
                            // 行為
                            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                            behavior.HttpGetEnabled = true;

                            // Addtrss
                            // behavior.HttpGetUrl = new Uri("http://127.0.0.1:8009/WCFService");
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