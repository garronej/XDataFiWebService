﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelloWebClientAspNet.HelloWcfServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HelloWcfServiceReference.IHelloWcfService")]
    public interface IHelloWcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHelloWcfService/getMessage", ReplyAction="http://tempuri.org/IHelloWcfService/getMessageResponse")]
        string getMessage(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHelloWcfService/getMessage", ReplyAction="http://tempuri.org/IHelloWcfService/getMessageResponse")]
        System.Threading.Tasks.Task<string> getMessageAsync(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHelloWcfServiceChannel : HelloWebClientAspNet.HelloWcfServiceReference.IHelloWcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HelloWcfServiceClient : System.ServiceModel.ClientBase<HelloWebClientAspNet.HelloWcfServiceReference.IHelloWcfService>, HelloWebClientAspNet.HelloWcfServiceReference.IHelloWcfService {
        
        public HelloWcfServiceClient() {
        }
        
        public HelloWcfServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HelloWcfServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWcfServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWcfServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string getMessage(string name) {
            return base.Channel.getMessage(name);
        }
        
        public System.Threading.Tasks.Task<string> getMessageAsync(string name) {
            return base.Channel.getMessageAsync(name);
        }
    }
}
