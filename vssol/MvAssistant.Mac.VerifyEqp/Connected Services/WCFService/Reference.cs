﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaskCleanerVerify.WCFService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/MvAssistant.WCFService")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFService.IMvAssistantWCFService")]
    public interface IMvAssistantWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/GetData", ReplyAction="http://tempuri.org/IMvAssistantWCFService/GetDataResponse")]
        string GetData(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/GetData", ReplyAction="http://tempuri.org/IMvAssistantWCFService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IMvAssistantWCFService/GetDataUsingDataContractResponse")]
        MaskCleanerVerify.WCFService.CompositeType GetDataUsingDataContract(MaskCleanerVerify.WCFService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IMvAssistantWCFService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<MaskCleanerVerify.WCFService.CompositeType> GetDataUsingDataContractAsync(MaskCleanerVerify.WCFService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/UtHalMaskTransfer_TestPathMove", ReplyAction="http://tempuri.org/IMvAssistantWCFService/UtHalMaskTransfer_TestPathMoveResponse")]
        string UtHalMaskTransfer_TestPathMove();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/UtHalMaskTransfer_TestPathMove", ReplyAction="http://tempuri.org/IMvAssistantWCFService/UtHalMaskTransfer_TestPathMoveResponse")]
        System.Threading.Tasks.Task<string> UtHalMaskTransfer_TestPathMoveAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/MaskRobot_Connect", ReplyAction="http://tempuri.org/IMvAssistantWCFService/MaskRobot_ConnectResponse")]
        string MaskRobot_Connect(string requestJson);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMvAssistantWCFService/MaskRobot_Connect", ReplyAction="http://tempuri.org/IMvAssistantWCFService/MaskRobot_ConnectResponse")]
        System.Threading.Tasks.Task<string> MaskRobot_ConnectAsync(string requestJson);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMvAssistantWCFServiceChannel : MaskCleanerVerify.WCFService.IMvAssistantWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MvAssistantWCFServiceClient : System.ServiceModel.ClientBase<MaskCleanerVerify.WCFService.IMvAssistantWCFService>, MaskCleanerVerify.WCFService.IMvAssistantWCFService {
        
        public MvAssistantWCFServiceClient() {
        }
        
        public MvAssistantWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MvAssistantWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MvAssistantWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MvAssistantWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(string value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(string value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public MaskCleanerVerify.WCFService.CompositeType GetDataUsingDataContract(MaskCleanerVerify.WCFService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<MaskCleanerVerify.WCFService.CompositeType> GetDataUsingDataContractAsync(MaskCleanerVerify.WCFService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public string UtHalMaskTransfer_TestPathMove() {
            return base.Channel.UtHalMaskTransfer_TestPathMove();
        }
        
        public System.Threading.Tasks.Task<string> UtHalMaskTransfer_TestPathMoveAsync() {
            return base.Channel.UtHalMaskTransfer_TestPathMoveAsync();
        }
        
        public string MaskRobot_Connect(string requestJson) {
            return base.Channel.MaskRobot_Connect(requestJson);
        }
        
        public System.Threading.Tasks.Task<string> MaskRobot_ConnectAsync(string requestJson) {
            return base.Channel.MaskRobot_ConnectAsync(requestJson);
        }
    }
}
