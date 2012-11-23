using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Management;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;

using Sxta.Rti1516.LowLevelManagement; // PATCH ANGEL: Por clase AbstractBootstrapObjectModelInteractionListener. Borrar cuando no este

namespace Sxta.Rti1516.BoostrapProtocol
{
    public class InteractionDispatcher
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

        public delegate void ReceiveInteractionDelegate(BaseInteractionMessage msg);
        public delegate void ReceiveCreationObjectDelegate(HLAobjectRoot objectRoot);

        private Dictionary<long, Dictionary<Type, ReceiveInteractionDelegate>> interactionsDelegates;

        private InteractionManager interactionManager;

        public InteractionDispatcher(InteractionManager aInteractionManager)
        {
            interactionsDelegates = new Dictionary<long, Dictionary<Type, ReceiveInteractionDelegate>>();

            interactionManager = aInteractionManager;
        }

        public void AddListener(long handle, AbstractLowLevelManagementObjectModelInteractionListener listener)
        {
            Type objType;

            if (!interactionsDelegates.ContainsKey(handle))
            {
                interactionsDelegates[handle] = new Dictionary<Type, ReceiveInteractionDelegate>();

                objType = typeof(BaseInteractionMessage);
                //interactionManager.AddReceiveInteractionDelegate(objType, "BaseInteraction", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
                interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.ReceiveInteraction));
            }

            objType = typeof(HLArequestHandlesMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLArequestHandles", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLArequestHandlesBase));

            objType = typeof(HLAreportHandlesMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAreportHandles", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAreportHandlesBase));

            objType = typeof(HLAregisterObjectInstanceMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAregisterObjectInstance", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAregisterObjectInstanceBase));

            objType = typeof(HLAregisterObjectInstanceWithTimeMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAregisterObjectInstanceWithTime", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAregisterObjectInstanceWithTimeBase));

            objType = typeof(HLArequestAttributeValueUpdateMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLArequestAttributeValueUpdate", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLArequestAttributeValueUpdateBase));

            objType = typeof(HLAupdateAttributeValuesBestEffortWithTimeMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAupdateAttributeValuesBestEffortWithTime", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAupdateAttributeValuesBestEffortWithTimeBase));

            objType = typeof(HLAupdateAttributeValuesReliableWithTimeMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAupdateAttributeValuesReliableWithTime", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAupdateAttributeValuesReliableWithTimeBase));

            objType = typeof(HLAupdateAttributeValuesBestEffortMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAupdateAttributeValuesBestEffort", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAupdateAttributeValuesBestEffortBase));

            objType = typeof(HLAupdateAttributeValuesReliableMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAupdateAttributeValuesReliable", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAupdateAttributeValuesReliableBase));
        }

        public void AddListener(long handle, AbstractManagementObjectModelInteractionListener listener)
        {
            Type objType;

            if (!interactionsDelegates.ContainsKey(handle))
            {
                interactionsDelegates[handle] = new Dictionary<Type, ReceiveInteractionDelegate>();

                objType = typeof(BaseInteractionMessage);
                //interactionManager.AddReceiveInteractionDelegate(objType, "BaseInteraction", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
                interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.ReceiveInteraction));
            }

            objType = typeof(HLApublishObjectClassAttributesMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLApublishObjectClassAttributes", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLApublishObjectClassAttributesBase));

            objType = typeof(HLApublishInteractionClassMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLApublishInteractionClass", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLApublishInteractionClassBase));

            objType = typeof(HLAsubscribeObjectClassAttributesMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAsubscribeObjectClassAttributes", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAsubscribeObjectClassAttributesBase));

            objType = typeof(HLAsubscribeInteractionClassMessage);
            interactionManager.AddReceiveInteractionDelegate(objType, "HLAsubscribeInteractionClass", new InteractionManager.ReceiveInteractionDelegate(this.DispatchInteraction));
            interactionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(listener.OnReceiveHLAsubscribeInteractionClassBase));
        }

        public void DispatchInteraction(BaseInteractionMessage msg)
        {
            if (interactionsDelegates.ContainsKey(msg.FederationExecutionHandle))
            {
                if (interactionsDelegates[msg.FederationExecutionHandle].ContainsKey(msg.GetType()))
                    interactionsDelegates[msg.FederationExecutionHandle][msg.GetType()](msg);
                else
                    interactionsDelegates[msg.FederationExecutionHandle][typeof(BaseInteractionMessage)](msg);
            }
        }

    }
}
