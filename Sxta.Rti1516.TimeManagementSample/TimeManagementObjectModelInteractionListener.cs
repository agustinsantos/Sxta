using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;

using Sxta.Rti1516.Time;
using Sxta.Rti1516.Ambassadors;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.Lrc;

using log4net;

namespace Sxta.Rti1516.TimeManagementSample
{
    public class TimeManagementObjectModelInteractionListener : ITimeManagementObjectModelInteractionListener
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private XrtiExecutiveAmbassador rti;
        private ILogicalTimeFactory timeFactory;
        private Home home;
        private ILogicalTimeInterval moveActorInterval;
        private TimeManagementForm form;
        private TimeManagementSimulation simulation;

        public TimeManagementObjectModelInteractionListener(XrtiExecutiveAmbassador aRti, 
            ILogicalTimeFactory aTimeFactory, Home home, ILogicalTimeInterval moveActorInterval, 
                TimeManagementForm form, TimeManagementSimulation simulation)
        {
            this.rti = aRti;
            this.timeFactory = aTimeFactory;
            this.home = home;
            this.moveActorInterval = moveActorInterval;
            this.form = form;
            this.simulation = simulation;
        }

        ///<summary>
        ///Notifies that the box is already in its house 
        ///</summary>
        ///<param name="msg"> the message associated with the interaction</param>
        ///<exception cref="InteractionClassNotRecognized"> if the interaction class was not recognized</exception>
        ///<exception cref="InteractionParameterNotRecognized"> if a parameter of the interaction was not
        /// recognized</exception>
        ///<exception cref="InteractionClassNotSubscribed"> if the federate had not subscribed to the
        /// interaction class</exception>
        ///<exception cref="FederateInternalError"> if an error occurs in the federate</exception>
        public void OnReceiveBoxInHouse(BoxInHouseMessage msg)
        {
            ILogicalTime time = timeFactory.Decode(msg.Time, 0);

            if (log.IsDebugEnabled)
            {
                log.Debug("Received BoxInHouseMessage [time = " + time + "]");
            }

            form.UpdateBoxInHouseTimeLabel(time);

            ObjectInstanceDescriptor oid = rti.descriptorManager.GetObjectInstanceDescriptor(home.InstanceHandle);
            IObjectClassHandle och = oid.ClassHandle;
            ObjectClassDescriptor ocd = rti.descriptorManager.GetObjectClassDescriptor(och);

            HLAattributeHandleValuePair[] handleValuePairList = new HLAattributeHandleValuePair[1];
            handleValuePairList[0] = new HLAattributeHandleValuePair();
            handleValuePairList[0].AttributeHandle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("BoxesCount").Handle).Identifier;
            handleValuePairList[0].AttributeValue = 1;

            rti.UpdateAttributeValues(home.InstanceHandle, handleValuePairList, new byte[1], time);
        }

            /*
            if (rti.State != null)
            {
                
                // 1. Add a callback that notifies the box will arrive
                ObjectInstanceDescriptor oid = rti.descriptorManager.GetObjectInstanceDescriptor(home.InstanceHandle);
                IObjectClassHandle och = oid.ClassHandle;
                ObjectClassDescriptor ocd = rti.descriptorManager.GetObjectClassDescriptor(och);

                HLAattributeHandleValuePair[] handleValuePairList = new HLAattributeHandleValuePair[1];
                handleValuePairList[0] = new HLAattributeHandleValuePair();
                handleValuePairList[0].AttributeHandle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("BoxesCount").Handle).Identifier;
                handleValuePairList[0].AttributeValue = 1;

                // ###############################################################################
                Lrc.ReflectAttributeValuesExtCallback callback =
                    new Lrc.ReflectAttributeValuesExtCallback(home.OwnFederateAmbassador,
                        time, home.InstanceHandle, handleValuePairList, new byte[1]);

                rti.State.Add(callback);

                if (log.IsDebugEnabled)
                {
                    log.Debug("Added callback " + callback + " for time " + time);
                }
                // ###############################################################################
                rti.UpdateAttributeValues(home.InstanceHandle, handleValuePairList, new byte[1], time);

                // 2. Add a callback that notifies the box will leave 
                time = time.Add(moveActorInterval);

                handleValuePairList = new HLAattributeHandleValuePair[1];
                handleValuePairList[0] = new HLAattributeHandleValuePair();
                handleValuePairList[0].AttributeHandle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("BoxesCount").Handle).Identifier;
                handleValuePairList[0].AttributeValue = 0;

                // ###############################################################################
                callback = new Lrc.ReflectAttributeValuesExtCallback(home.OwnFederateAmbassador,
                        time, home.InstanceHandle, handleValuePairList, new byte[1]);

                rti.State.Add(callback);

                if (log.IsDebugEnabled)
                {
                    log.Debug("Added callback " + callback + " for time " + time);
                }
                // ###############################################################################
                rti.UpdateAttributeValues(home.InstanceHandle, handleValuePairList, new byte[1], time);
            }
            */

        #region IInteractionListener Members

        public void ReceiveInteraction(BaseInteractionMessage msg)
        {
            /*
            if (log.IsDebugEnabled)
                log.Debug("Received BaseInteractionMessage =  " + msg.ToString());
            */
        }

        #endregion
    }
}
