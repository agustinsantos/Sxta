using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.Ambassadors;
using Sxta.Rti1516.XrtiHandles;

namespace Sxta.Rti1516.LowLevelManagement
{
    public class LowLevelManagementObjectModelInteractionListener : AbstractLowLevelManagementObjectModelInteractionListener
    {
        protected XrtiExecutiveAmbassador parent;
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected ObjectClassDescriptor federationExecutionDescriptor;
        //protected Dictionary<long, HLAfederation> federations = new Dictionary<long, HLAfederation>();
        //protected IAttributeHandleValueMapFactory factory = new XRTIAttributeHandleValueMapFactory();

        public LowLevelManagementObjectModelInteractionListener(XrtiExecutiveAmbassador p, String aName)
            : base(aName)
        {
            parent = p;
            federationExecutionDescriptor = parent.descriptorManager.GetObjectClassDescriptor("HLAfederationExecution");
        }

        #region ILowLevelManagementObjectModelInteractionListener Members

        public override void OnReceiveHLArequestHandles(HLArequestHandlesMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLArequestHandles =  " + msg.ToString());
        }

        public override void OnReceiveHLAreportHandles(HLAreportHandlesMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAreportHandles =  " + msg.ToString());
        }

        public override void OnReceiveHLAregisterObjectInstance(HLAregisterObjectInstanceMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAregisterObjectInstance =  " + msg.ToString());
        }

        public override void OnReceiveHLAregisterObjectInstanceWithTime(HLAregisterObjectInstanceWithTimeMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAregisterObjectInstanceWithTime =  " + msg.ToString());
        }

        public override void OnReceiveHLArequestAttributeValueUpdate(HLArequestAttributeValueUpdateMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLArequestAttributeValueUpdate =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffort(HLAupdateAttributeValuesBestEffortMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAupdateAttributeValuesBestEffortMessage =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesReliable(HLAupdateAttributeValuesReliableMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAupdateAttributeValuesReliableMessage =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffortWithTime(HLAupdateAttributeValuesBestEffortWithTimeMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAupdateAttributeValuesBestEffortWithTimeMessage =  " + msg.ToString());
        }

        public override void OnReceiveHLAupdateAttributeValuesReliableWithTime(HLAupdateAttributeValuesReliableWithTimeMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAupdateAttributeValuesReliableWithTimeMessage =  " + msg.ToString());
        }

        #endregion

        #region IInteractionListener Members

        public override void ReceiveInteraction(BaseInteractionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received BaseInteractionMessage =  " + msg.ToString());
        }

        #endregion
    }

    public class FederationLowLevelManagementObjectModelInteractionListener : LowLevelManagementObjectModelInteractionListener
    {
        public FederationLowLevelManagementObjectModelInteractionListener(XrtiExecutiveAmbassador p, String aName)
            : base(p, aName)
        {
        }

        private void AddCallbackUpdateAttributeValues(HLAattributeHandleValuePair[] handleValuePairList, long objectInstanceHandle, byte[] userSuppliedTag, long federateHandle, long interactionIndex, ILogicalTime time)
        {
            if (parent.State != null)
            {
                Lrc.ReflectAttributeValuesExtCallback callback =
                    new Lrc.ReflectAttributeValuesExtCallback(this.parent.FederateAmbassador,
                        time, federateHandle, interactionIndex, new XRTIObjectInstanceHandle(objectInstanceHandle),
                            handleValuePairList, userSuppliedTag);

                parent.lrc.AddCallback(callback);
            }
        }

        private void AddCallbackRegisterObjectInstance(long objectInstanceHandle, long objectClassHandle, string objectName, long federateHandle, long interactionIndex, ILogicalTime time)
        {
            if (parent.State != null)
            {
                Lrc.DiscoverObjectInstanceCallback callback =
                    new Lrc.DiscoverObjectInstanceCallback(this.parent.FederateAmbassador,
                        time, federateHandle, interactionIndex, new XRTIObjectInstanceHandle(objectInstanceHandle),
                            new XRTIObjectClassHandle(objectClassHandle), objectName);

                parent.lrc.AddCallback(callback);
            }
        }

        #region ILowLevelManagementObjectModelInteractionListener Members

        public override void OnReceiveHLAregisterObjectInstance(HLAregisterObjectInstanceMessage msg)
        {
            base.OnReceiveHLAregisterObjectInstance(msg);

            AddCallbackRegisterObjectInstance(msg.ObjectInstanceHandle, msg.ObjectClassHandle, msg.ObjectName, 0, 0, null);
        }

        public override void OnReceiveHLAregisterObjectInstanceWithTime(HLAregisterObjectInstanceWithTimeMessage msg)
        {
            base.OnReceiveHLAregisterObjectInstanceWithTime(msg);

            ILogicalTime time = parent.LogicalTimeFactory.Decode(msg.LogicalTime, 0);

            AddCallbackRegisterObjectInstance(msg.ObjectInstanceHandle, msg.ObjectClassHandle, msg.ObjectName, msg.FederateHandle, msg.InteractionIndex, time);
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffort(HLAupdateAttributeValuesBestEffortMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesBestEffort(msg);

            AddCallbackUpdateAttributeValues(msg.AttributeHandleValuePairList, msg.ObjectInstanceHandle, msg.UserSuppliedTag, 0, 0,null);
        }

        public override void OnReceiveHLAupdateAttributeValuesReliable(HLAupdateAttributeValuesReliableMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesReliable(msg);

            AddCallbackUpdateAttributeValues(msg.AttributeHandleValuePairList, msg.ObjectInstanceHandle, msg.UserSuppliedTag, 0, 0, null);
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffortWithTime(HLAupdateAttributeValuesBestEffortWithTimeMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesBestEffortWithTime(msg);

            ILogicalTime time = parent.LogicalTimeFactory.Decode(msg.LogicalTime, 0);

            AddCallbackUpdateAttributeValues(msg.AttributeHandleValuePairList, msg.ObjectInstanceHandle, msg.UserSuppliedTag, msg.FederateHandle, msg.InteractionIndex, time);
        }

        public override void OnReceiveHLAupdateAttributeValuesReliableWithTime(HLAupdateAttributeValuesReliableWithTimeMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesReliableWithTime(msg);

            ILogicalTime time = parent.LogicalTimeFactory.Decode(msg.LogicalTime, 0);

            AddCallbackUpdateAttributeValues(msg.AttributeHandleValuePairList, msg.ObjectInstanceHandle, msg.UserSuppliedTag, msg.FederateHandle, msg.InteractionIndex, time);
        }

        #endregion
    }

    public class MetaFederationLowLevelManagementInteractionListener : LowLevelManagementObjectModelInteractionListener
    {
        public MetaFederationLowLevelManagementInteractionListener(XrtiExecutiveAmbassador p, String aName)
            : base(p, aName)
        {
        }

        #region ILowLevelManagementObjectModelInteractionListener Members

        public override void OnReceiveHLAregisterObjectInstance(HLAregisterObjectInstanceMessage msg)
        {
            base.OnReceiveHLAregisterObjectInstance(msg);
            
            Lrc.DiscoverObjectInstanceCallback callback =
                new Lrc.DiscoverObjectInstanceCallback(this.parent.MetaFederateAmbassador,
                    new XRTIObjectInstanceHandle(msg.ObjectInstanceHandle),
                        new XRTIObjectClassHandle(msg.ObjectClassHandle), msg.ObjectName);

            callback.Call();
        }

        public override void OnReceiveHLAregisterObjectInstanceWithTime(HLAregisterObjectInstanceWithTimeMessage msg)
        {
            base.OnReceiveHLAregisterObjectInstanceWithTime(msg);

            Lrc.DiscoverObjectInstanceCallback callback =
                new Lrc.DiscoverObjectInstanceCallback(this.parent.MetaFederateAmbassador,
                    new XRTIObjectInstanceHandle(msg.ObjectInstanceHandle),
                        new XRTIObjectClassHandle(msg.ObjectClassHandle), msg.ObjectName);

            callback.Call();
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffort(HLAupdateAttributeValuesBestEffortMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesBestEffort(msg);

            ProcessHLAupdateAttributeValuesMetafederation(msg);
        }

        public override void OnReceiveHLAupdateAttributeValuesReliable(HLAupdateAttributeValuesReliableMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesReliable(msg);

            ProcessHLAupdateAttributeValuesMetafederation(msg);
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffortWithTime(HLAupdateAttributeValuesBestEffortWithTimeMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesBestEffortWithTime(msg);

            ProcessHLAupdateAttributeValuesMetafederation(msg);
        }

        public override void OnReceiveHLAupdateAttributeValuesReliableWithTime(HLAupdateAttributeValuesReliableWithTimeMessage msg)
        {
            base.OnReceiveHLAupdateAttributeValuesReliableWithTime(msg);

            ProcessHLAupdateAttributeValuesMetafederation(msg);
        }

        #endregion

        protected void ProcessHLAupdateAttributeValuesMetafederation(HLAupdateAttributeValuesMessage msg)
        {
            Lrc.ReflectAttributeValuesExtCallback callback =
                new Lrc.ReflectAttributeValuesExtCallback(this.parent.MetaFederateAmbassador,
                    new XRTIObjectInstanceHandle(msg.ObjectInstanceHandle),
                        msg.AttributeHandleValuePairList, msg.UserSuppliedTag);

            callback.Call();

            ObjectInstanceDescriptor oid = parent.descriptorManager.GetObjectInstanceDescriptor(new XRTIObjectInstanceHandle(msg.ObjectInstanceHandle));
            ObjectClassDescriptor ocd = parent.descriptorManager.GetObjectClassDescriptor("Sxtafederate");

            bool timePropertyFound = false;
            if (oid.ClassHandle.Equals(ocd.Handle))
            {
                if (msg.AttributeHandleValuePairList.Length > 0)
                {
                    for (int i = 0; i < msg.AttributeHandleValuePairList.Length && !timePropertyFound; i++)
                    {
                        HLAattributeHandleValuePair pair = msg.AttributeHandleValuePairList[i];
                        string propertyName = ocd.GetAttributeDescriptor(new XRTIAttributeHandle(pair.AttributeHandle)).Name;
                        if (propertyName.Equals("HLAlogicalTime") || propertyName.Equals("HLAlookahead")
                            || propertyName.Equals("HLApendingTime") || propertyName.Equals("HLALITS") || propertyName.Equals("HLAtimeManagerState")
                                    || propertyName.Equals("HLAtimeConstrained") || propertyName.Equals("HLAtimeRegulating"))
                        {
                            timePropertyFound = true;
                        }
                    }

                    if (timePropertyFound)
                    {
                        parent.UpdateFederate();
                    }
                }
            }
        }
    }
}
