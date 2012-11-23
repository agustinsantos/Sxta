using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Sxta.Rti1516.Ambassadors;
using Sxta.Rti1516.LowLevelManagement;
using Hla.Rti1516;
using Sxta.Rti1516.Channels;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.BoostrapProtocol;

namespace Sxta.Rti1516.TimeManagementSample
{
    public class MyLowLevelManagementObjectModelInteractionListener : LowLevelManagementObjectModelInteractionListener
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected TimeManagementForm form;

        public MyLowLevelManagementObjectModelInteractionListener(XrtiExecutiveAmbassador p, String aName, TimeManagementForm form)
            : base(p, aName)
        {
            this.form = form;
        }

        public override void OnReceiveHLAupdateAttributeValuesBestEffort(HLAupdateAttributeValuesBestEffortMessage msg)
        {
            ProcessHLAupdateAttributeValues(msg);
        }

        public override void OnReceiveHLAupdateAttributeValuesReliable(HLAupdateAttributeValuesReliableMessage msg)
        {
            ProcessHLAupdateAttributeValues(msg);
        }

        protected void ProcessHLAupdateAttributeValues(HLAupdateAttributeValuesMessage msg)
        {
            // Metafederation RO message
            if (msg.FederationExecutionHandle == HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE)
            {
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
                                || propertyName.Equals("HLApendingTime") || propertyName.Equals("HLALITS"))
                                    // propertyName.Equals("HLAtimeManagerState") || propertyName.Equals("HLAtimeConstrained") || propertyName.Equals("HLAtimeRegulating"))
                            {
                                timePropertyFound = true;
                            }
                        }

                        if (timePropertyFound)
                        {
                            form.UpdateTimeManagementValueLabels();
                        }
                    }
                }
            }
        }

        public override void ReceiveInteraction(BaseInteractionMessage msg)
        {
            try
            {
                if (msg is HLAregisterObjectInstanceMessage)
                {
                    this.OnReceiveHLAregisterObjectInstance(msg as HLAregisterObjectInstanceMessage);
                }
                else if (msg is HLAregisterObjectInstanceWithTimeMessage)
                {
                    this.OnReceiveHLAregisterObjectInstanceWithTime(msg as HLAregisterObjectInstanceWithTimeMessage);
                }
                else if (msg is HLAupdateAttributeValuesBestEffortWithTimeMessage)
                {
                    this.OnReceiveHLAupdateAttributeValuesBestEffortWithTime(msg as HLAupdateAttributeValuesBestEffortWithTimeMessage);
                }
                else if (msg is HLAupdateAttributeValuesReliableWithTimeMessage)
                {
                    this.OnReceiveHLAupdateAttributeValuesReliableWithTime(msg as HLAupdateAttributeValuesReliableWithTimeMessage);
                }
                else if (msg is HLAupdateAttributeValuesBestEffortMessage)
                {
                    this.OnReceiveHLAupdateAttributeValuesBestEffort(msg as HLAupdateAttributeValuesBestEffortMessage);
                }
                else if (msg is HLAupdateAttributeValuesReliableMessage)
                {
                    this.OnReceiveHLAupdateAttributeValuesReliable(msg as HLAupdateAttributeValuesReliableMessage);
                }
            }
            catch (System.IO.IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }

        }
    }
}