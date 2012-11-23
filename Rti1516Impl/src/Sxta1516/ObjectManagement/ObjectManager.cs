using System;
using System.Reflection;

using System.Collections.Generic;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Management;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.Ambassadors;

namespace Sxta.Rti1516.ObjectManagement
{
    public class ObjectManager : IHLACreateObjectRootListener
    {

        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected internal DescriptorManager descriptorManager;

        public ObjectManager(DescriptorManager aDescriptorManager)
        {
            descriptorManager = aDescriptorManager;

            HLAobjectRoot.AddIHLAobjectRootCreationListener(this);
        }

        #region IHlaCreateObjectRootListener Members

        public void OnReceiveCreatedNewObject(object newObject)
        {
            if (newObject is HLAobjectRoot)
            {
                HLAobjectRoot obj = newObject as HLAobjectRoot;
                
                if (obj.HLAprivilegeToDeleteObject)
                {
                    obj.AddIHLAobjectRootListener((IHLAobjectRootListener)obj.OwnFederateAmbassador);

                    // COMMENT ANGEL: Sólo se registran los objetos locales con esta instrucción
                    //                Los objetos remotos lo realizan en el momento que se invoca la callback DiscoverObjectInstance
                    ((ISxtaFederateAmbassador)obj.OwnFederateAmbassador).RegisterObjectInstance(obj);
                }
            }
        }

        #endregion


        public object CreateProxyObject(string objectName, long objectInstanceHandle, long objectClassHandle)
        {
            //ObjectInstanceDescriptor oid = descriptorManager.GetObjectInstanceDescriptor(new XRTIObjectInstanceHandle(objectInstanceHandle));
            //ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(oid.ClassHandle);
            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(new XRTIObjectClassHandle(objectClassHandle));
            HLAProxyAttribute attr = new HLAProxyAttribute();
            attr.ObjectClassHandle = objectClassHandle;
            attr.ObjectInstanceHandle = objectInstanceHandle;
            attr.ObjectName = ocd.Name;

            object obj = HLAobjectRoot.NewRemoteInstance(ocd.NativeName, attr);

            if (log.IsDebugEnabled)
                log.Debug("The constructor of object " + objectName + " has been called");

            //return Activator.CreateInstance(ocd.NativeName, null, new object[] { attr });

            return obj;
        }

        public object UpdateAttributeValuesProxyObject(object instance, IDictionary<string, object> methodNameValueMap)
        {
            if (instance is HLAobjectRoot)
            {
                HLAobjectRoot obj = instance as HLAobjectRoot;
                ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(obj.ClassHandle);
                foreach (KeyValuePair<string, object> entry in methodNameValueMap)
                {
                    if (log.IsDebugEnabled)
                        log.Debug("The method " + entry.Key + " from object " + obj.InstanceHandle + " has been called; new value:" + entry.Value + "; parameter type = " + entry.Value.GetType());

                    HLAAttributeAttribute attr = obj.tableMethodInfo2Attr[entry.Key];
                    attr.propInfo.GetSetMethod().Invoke(obj, new object[] { entry.Value });
                }
            }

            return instance;
        }

        public object UpdateAttributeValuesProxyObject(object instance, HLAattributeHandleValuePair[] methodNameValueMap)
        {
            if (instance is HLAobjectRoot)
            {
                HLAobjectRoot obj = instance as HLAobjectRoot;
                ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(obj.ClassHandle);
                foreach (HLAattributeHandleValuePair entry in methodNameValueMap)
                {
                    if (log.IsDebugEnabled)
                        log.Debug("The method " + entry.AttributeHandle + " from object " + obj.InstanceHandle +
                            " has been called; new value:" + entry.AttributeValue + "; parameter type = " + entry.AttributeValue.GetType());

                    string attrName = ocd.GetAttributeDescriptor(new XRTIAttributeHandle(entry.AttributeHandle)).Name;
                    HLAAttributeAttribute attr = obj.AttrTable[attrName]; 
                    attr.propInfo.GetSetMethod().Invoke(obj, new object[] { entry.AttributeValue });
                    //obj.GetType().BaseType.GetProperty(attrName).GetSetMethod().Invoke(obj, new object[] { entry.AttributeValue });
                }
            }

            return instance;
        }

    }

#if PENDIENTE_DE_INTEGRAR
    public class ObjectManagerListener : AbstractBootstrapObjectModelInteractionListener
    {

        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected internal InteractionManager interactionManager;
        protected internal DescriptorManager descriptorManager;

        public ObjectManagerListener(DescriptorManager descriptorMngr, InteractionManager interactionMngr)
            : base("Object Manager Listener")
        {
            interactionManager = interactionMngr;
            descriptorManager = descriptorMngr;
        }

        /// <summary> 
        /// Acquires a block of handles.
        /// </summary>
        /// <param name="blockSize">the number of handles desired
        /// </param>
        /// <returns> the first handle in the acquired block
        /// </returns>
        /* COMMENT ANGEL: Posiblemente este método haya que borrarlo
        protected virtual long AcquireHandles(long blockSize)
        {
            lock (this)
            {
                long blockStart = handleCounter;
                handleCounter += blockSize;
                return blockStart;
            }
        }
        */

        #region IBootstrapObjectModelInteractionListener Members

        public override void OnReceiveHLAGenericInteraction(HLAGenericInteractionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAGenericInteraction =  " + msg.ToString());
        }

        public override void OnReceiveHLAinteractionFragment(HLAinteractionFragmentMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAinteractionFragment =  " + msg.ToString());
        }

        public override void OnReceiveHLArequestHandles(HLArequestHandlesMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLArequestHandles =  " + msg.ToString());
            try
            {
                HLAreportHandlesMessage reportHandlesMessage = new HLAreportHandlesMessage();
                //reportHandlesMessage.BlockStart = AcquireHandles(msg.BlockSize);
                reportHandlesMessage.BlockSize = msg.BlockSize;

                if (log.IsDebugEnabled)
                    log.Debug("Assigned handles starting at " + reportHandlesMessage.BlockStart);

                interactionManager.SendInteraction(reportHandlesMessage);
            }
            catch (RTIexception rtie)
            {
                throw new FederateInternalError(rtie.ToString());
            }
        }

        public override void OnReceiveHLAreportHandles(HLAreportHandlesMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAreportHandles =  " + msg.ToString());
            lock (this)
            {
                //handleCounter = msg.BlockStart;
                System.Threading.Monitor.Pulse(this);
            }
        }

        public override void OnReceiveHLAcontinue(HLAcontinueMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAcontinue =  " + msg.ToString());
        }

        public override void OnReceiveHLAregisterObjectInstance(HLAregisterObjectInstanceMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAregisterObjectInstance =  " + msg.ToString());

            lock (this)
            {
                ObjectInstanceDescriptor oid = new ObjectInstanceDescriptor(msg.ObjectName, new XRTIObjectInstanceHandle(msg.ObjectInstanceHandle), new XRTIObjectClassHandle(msg.ObjectClassHandle), msg.FederationExecutionHandle);
                descriptorManager.AddObjectInstanceDescriptor(oid);
            }
        }

        public override void OnReceiveHLAregisterObjectInstanceWithTime(HLAregisterObjectInstanceWithTimeMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAregisterObjectInstanceWithTime =  " + msg.ToString());
        }

        public override void OnReceiveHLArequestAttributeValueUpdate(HLArequestAttributeValueUpdateMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLArequestAttributeValueUpdate =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffort(HLAupdateAttributeValuesBestEffortMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAupdateAttributeValuesBestEffort =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesReliable(HLAupdateAttributeValuesReliableMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAupdateAttributeValuesReliable =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffortWithTime(HLAupdateAttributeValuesBestEffortWithTimeMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAupdateAttributeValuesBestEffortWithTime =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesReliableWithTime(HLAupdateAttributeValuesReliableWithTimeMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received HLAupdateAttributeValuesReliableWithTime =  " + msg.ToString());
        }

        #endregion

        #region IInteractionListener Members

        public override void ReceiveInteraction(BaseInteractionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received BaseInteraction Message =  " + msg.ToString());
        }

        #endregion
    }

    public class ObjectManagerMaster : ObjectManager
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int clientHandle = 0;

        public ObjectManagerMaster(int myClientHandle, DescriptorManager descriptorMngr, InteractionManager interactionMngr)
            : base(descriptorMngr) //, interactionMngr)
        {
            clientHandle = myClientHandle;

            //interactionManager.AddInteractionListener(this);
        }

    }
#endif
}
