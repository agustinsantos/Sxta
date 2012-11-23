namespace Sxta.Rti1516.ObjectManagement
{

    using Hla.Rti1516;
    using System;
    using System.Collections.Generic;

    ///<summary>
    ///Autogenerated object instance listener interface. 
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler from MetaFederationObjectModel.xml </author>
    public interface IHLAobjectRootListener
    {
        ///<summary>
        ///Updates a set of attribute values 
        ///</summary>
        ///<param name="theAttributes"> the attribute values associated with the interaction</param>
        void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, string methodName, object newValue);
        void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, IDictionary<string, object> methodNameValueMap);
        void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, string methodName, object newValue, ILogicalTime time);
        void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, IDictionary<string, object> methodNameValueMap, ILogicalTime time);
    }
}
