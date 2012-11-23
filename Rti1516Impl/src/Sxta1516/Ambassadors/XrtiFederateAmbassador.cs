using System;
using System.Collections.Generic;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

using Sxta.Rti1516.Management;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.ObjectManagement;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.XrtiHandles;

namespace Sxta.Rti1516.Ambassadors
{
    /// <summary> 
    /// The proxy ambassador.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class FederateAmbassador : BaseAmbassador, IHLAobjectRootListener
    {
        protected XrtiExecutiveAmbassador rti;

        public delegate void NewObjectDiscovered(object proxy);

        public event NewObjectDiscovered OnNewObject;

        public FederateAmbassador(IRTIambassador prtiAmbassador)
            : base(prtiAmbassador)
        {
            rti = ((XrtiExecutiveAmbassador)rtiAmbassador);
        }

        public override void DiscoverObjectInstance(IObjectInstanceHandle theObject, IObjectClassHandle theObjectClass, string objectName)
        {
            long instanceHandle = ((XRTIObjectInstanceHandle)theObject).Identifier;
            long classHandle = ((XRTIObjectClassHandle)theObjectClass).Identifier;

            object proxy = rti.objectManager.CreateProxyObject(objectName, instanceHandle, classHandle);

            if (proxy != null)
            {
                RegisterObjectInstance(proxy);
            }
            else
            {
                throw new RTIinternalError("Type not found: " + objectName + ".");
            }
            if (OnNewObject != null)
            {
                if (log.IsInfoEnabled)
                    log.Info("Discovered a new object: " + proxy);
                OnNewObject(proxy);
            }
        }

        public override void RegisterObjectInstance(object obj)
        {
            lock (this)
            {
                if (obj != null && obj is HLAobjectRoot)
                {
                    HLAobjectRoot objRoot = obj as HLAobjectRoot;

                    string className = obj.GetType().BaseType.Name;
                    long objectClassHandle = ((XRTIObjectClassHandle)rti.descriptorManager.GetObjectClassDescriptor(className).Handle).Identifier;
                    objRoot.ClassHandle = new XRTIObjectClassHandle(objectClassHandle);

                    objectInstanceHandleProxyMap[objRoot.InstanceHandle] = objRoot;
                    objectListIsValid = false;

                    ObjectInstanceDescriptor oid = new ObjectInstanceDescriptor(objRoot.ObjectName, objRoot.InstanceHandle, objRoot.ClassHandle, objRoot.OwnFederationExecutionHandle);
                    objectInstanceDescriptorList.Add(oid);
                    rti.descriptorManager.AddObjectInstanceDescriptor(oid);

                    // COMMENT ANGEL: Si el objeto ha sido creado localmente --> Se debe enviar la creación al resto de federados
                    if (objRoot.HLAprivilegeToDeleteObject)
                    {
                        rti.RegisterObjectInstance(objRoot);
                    }

                }
            }
        }

        #region IHLAobjectRootListener Members

        public void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, string methodName, object newValue)
        {
            if (log.IsDebugEnabled)
                log.Debug("The method " + methodName + " from object " + instanceHandle + " has been called; new value:" + newValue + "; parameter type = " + newValue.GetType());

            HLAattributeHandleValuePair[] attributeHandleValuePair = CreateAttributeHandleValuePairList(methodName, newValue, instanceHandle);

            // TODO ANGEL: que pasa con userSuppliedTag
            rti.UpdateAttributeValues(instanceHandle, attributeHandleValuePair, null);
        }

        public void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, string methodName, object newValue, ILogicalTime time)
        {
            if (log.IsDebugEnabled)
                log.Debug("The method " + methodName + " from object " + instanceHandle + " has been called; new value:" + newValue + "; parameter type = " + newValue.GetType());

            HLAattributeHandleValuePair[] attributeHandleValuePair = CreateAttributeHandleValuePairList(methodName, newValue, instanceHandle);

            // TODO ANGEL: que pasa con userSuppliedTag
            rti.UpdateAttributeValues(instanceHandle, attributeHandleValuePair, null, time);
        }

        public void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, IDictionary<string, object> methodNameValueMap)
        {
            HLAattributeHandleValuePair[] attributeHandleValuePair = CreateAttributesHandleValuePairList(methodNameValueMap, instanceHandle);

            // TODO ANGEL: que pasa con userSuppliedTag
            rti.UpdateAttributeValues(instanceHandle, attributeHandleValuePair, null);
        }

        public void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, IDictionary<string, object> methodNameValueMap, ILogicalTime time)
        {
            HLAattributeHandleValuePair[] attributeHandleValuePair = CreateAttributesHandleValuePairList(methodNameValueMap, instanceHandle);

            // TODO ANGEL: que pasa con userSuppliedTag
            rti.UpdateAttributeValues(instanceHandle, attributeHandleValuePair, new byte[1], time);
        }

        private HLAattributeHandleValuePair[] CreateAttributeHandleValuePairList(string methodName, object newValue, IObjectInstanceHandle instanceHandle)
        {
            ObjectInstanceDescriptor oid = rti.descriptorManager.GetObjectInstanceDescriptor(instanceHandle);
            IObjectClassHandle och = oid.ClassHandle;
            ObjectClassDescriptor ocd = rti.descriptorManager.GetObjectClassDescriptor(och);

            HLAattributeHandleValuePair[] attributeHandleValuePairList = new HLAattributeHandleValuePair[1];
            attributeHandleValuePairList[0] = new HLAattributeHandleValuePair();
            attributeHandleValuePairList[0].AttributeHandle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor(methodName).Handle).Identifier;
            attributeHandleValuePairList[0].AttributeValue = newValue;

            return attributeHandleValuePairList;
        }

        private HLAattributeHandleValuePair[] CreateAttributesHandleValuePairList(IDictionary<string, object> methodNameValueMap, IObjectInstanceHandle instanceHandle)
        {
            HLAattributeHandleValuePair[] attributeHandleValuePairList = new HLAattributeHandleValuePair[methodNameValueMap.Count];

            ObjectInstanceDescriptor oid = rti.descriptorManager.GetObjectInstanceDescriptor(instanceHandle);
            IObjectClassHandle och = oid.ClassHandle;
            ObjectClassDescriptor ocd = rti.descriptorManager.GetObjectClassDescriptor(och);

            int count = 0;
            foreach (KeyValuePair<string, object> entry in methodNameValueMap)
            {
                if (log.IsDebugEnabled)
                    log.Debug("The method " + entry.Key + " from object " + instanceHandle + " has been called; new value:" + entry.Value + "; parameter type = " + entry.Value.GetType());

                attributeHandleValuePairList[count] = new HLAattributeHandleValuePair();
                attributeHandleValuePairList[count].AttributeHandle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor(entry.Key).Handle).Identifier;
                attributeHandleValuePairList[count].AttributeValue = entry.Value;
                count++;
            }

            return attributeHandleValuePairList;
        }

        #endregion

        public override void ReflectAttributeValuesExt(IObjectInstanceHandle theObject, HLAattributeHandleValuePair[] theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport)
        {
            // TODO ANGEL: LOCK DUDOSO
            //lock (this)
            //{
            if (objectInstanceHandleProxyMap.ContainsKey(theObject))
            {
                object instance = objectInstanceHandleProxyMap[theObject];
                rti.objectManager.UpdateAttributeValuesProxyObject(instance, theAttributes);
            }
            else
            {
                if (log.IsErrorEnabled)
                    log.Error("Object " + theObject + " not found. Attributes to reflect: " + theAttributes);
            }
            //}
        }

        public override void DumpObjects()
        {
            lock (this)
            {
                ICollection<HLAobjectRoot> objs = objectInstanceHandleProxyMap.Values;
                log.Info("In DumpObjects. Number of known objects = " + objs.Count);
                foreach (HLAobjectRoot obj in objs)
                {
                    if (obj.HLAprivilegeToDeleteObject)
                        log.Info(obj + " is local");
                    else
                        log.Info(obj + " is remote");
                }
            }
        }

        public virtual IList<HLAobjectRoot> GetObjects()
        {
            lock (this)
            {
                return new List<HLAobjectRoot>(objectInstanceHandleProxyMap.Values).AsReadOnly();
            }
        }

        private List<HLAobjectRoot> objectList;
        private bool objectListIsValid = false;
        public virtual List<HLAobjectRoot> GetObjectsCollection()
        {
            lock (this)
            {
                if (!objectListIsValid)
                {
                    objectList = new List<HLAobjectRoot>(objectInstanceHandleProxyMap.Values);
                    objectListIsValid = true;
                }
                return objectList;
            }
        }

        public virtual IList<ObjectInstanceDescriptor> GetDescriptorObjects()
        {
            lock (this)
            {
                return new List<ObjectInstanceDescriptor>(objectInstanceDescriptorList).AsReadOnly();
            }
        }
    }

    public class XrtiFederateAmbassador : FederateAmbassador
    {
        //private IHLAfederate federate;

        public XrtiFederateAmbassador(IRTIambassador prtiAmbassador)
            : base(prtiAmbassador)
        {
        }

        public override void TimeRegulationEnabled(ILogicalTime time)
        {
            if (log.IsInfoEnabled)
                log.Info("Federate is time regulator [time = " + time + "]");
        }

        public override void TimeConstrainedEnabled(ILogicalTime time)
        {
            if (log.IsInfoEnabled)
                log.Info("Federate is time constrained [time = " + time + "]");
        }

        public override void TimeAdvanceGrant(ILogicalTime theTime)
        {
            if (log.IsInfoEnabled)
                log.Info("Federate is time advance grant to time " + theTime);
        }

    }
}