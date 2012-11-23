using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.Ambassadors;

namespace Sxta.Rti1516.Lrc
{
    public abstract class Callback
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

        protected internal IFederateAmbassador federate;

        protected long federateHandle;

        public long FederateHandle
        {
            get { return federateHandle;  } 
            set { federateHandle = value; }
        }

        protected long interactionIndex;

        public long InteractionIndex
        {
            get { return interactionIndex; }
            set { interactionIndex = value; }
        }

        protected ILogicalTime time;

        public ILogicalTime Time
        {
            get { return time;  }
            set { time = value; }
        }

        public Callback(IFederateAmbassador federate)
        {
            this.federate = federate;
        }

        public Callback(IFederateAmbassador federate, ILogicalTime time)
            : this(federate)
        {
            this.time = time;
        }

        public Callback(IFederateAmbassador federate, ILogicalTime time, long federateHandle, long interactionIndex) 
            : this(federate, time)
        {
            this.federateHandle = federateHandle;
            this.interactionIndex = interactionIndex;
        }

        public bool IsTimeStamped()
        {
            return this.time != null;
        }

        public abstract void Call();
    }

    public sealed class TimeAdvanceGrantedCallback : Callback
    {
        private ILogicalTime timeParam;

        public TimeAdvanceGrantedCallback(IFederateAmbassador federate, ILogicalTime time, ILogicalTime timeParam)
            : base(federate, time)
        {
            this.timeParam = timeParam;
        }

        public override void Call()
        {
            // make appropriate changes to state //
            //TODO
            //TODO federate.State.SetAdvancing(false);
            //TODO federate.State.SetCurrentTime(time);
            //TODO federate.State.SetRequestedTime(time); // just to keep them in sync

            // call the fedamb //
            federate.TimeAdvanceGrant(timeParam);
        }
    }

    public sealed class TimeConstrainedEnabledCallback : Callback
    {
        private ILogicalTime timeParam;
        public TimeConstrainedEnabledCallback(IFederateAmbassador federate, ILogicalTime time, ILogicalTime timeParam)
            : base(federate, time)
        {
            this.timeParam = timeParam;
        }

        public override void Call()
        {
            // make appropriate changes to state //
            //TODO
            //TODO federate.State.SetCurrentTime(time);
            //TODO federate.State.SetConstrained(LRCState.Status.ON);

            // call the fedamb //
            federate.TimeConstrainedEnabled(timeParam);
        }
    }

    public sealed class TimeRegulationEnabledCallback : Callback
    {
        private ILogicalTime timeParam;
        public TimeRegulationEnabledCallback(IFederateAmbassador federate, ILogicalTime time, ILogicalTime timeParam)
            : base(federate, time)
        {
            this.timeParam = timeParam;
        }

        public override void Call()
        {
            // make appropriate changes to state //
            //TODO
            //TODO federate.State.SetCurrentTime(time);
            //TODO federate.State.SetRegulating( LRCState.Status.ON );

            // call the fedamb //
            federate.TimeRegulationEnabled(timeParam);
        }
    }

    public sealed class SynchronizationPointRegistrationSucceededCallback : Callback
    {
        private String synchronizationPointLabel;

        public SynchronizationPointRegistrationSucceededCallback(IFederateAmbassador federate, ILogicalTime time, String label)
            : base(federate, time)
        {
            this.synchronizationPointLabel = label;
        }

        public override void Call()
        {
            // registration was successful //
            federate.SynchronizationPointRegistrationSucceeded(synchronizationPointLabel);
        }
    }

    public sealed class SynchronizationPointRegistrationFailedCallback : Callback
    {
        private String synchronizationPointLabel;
        private SynchronizationPointFailureReason reason;

        public SynchronizationPointRegistrationFailedCallback(IFederateAmbassador federate, ILogicalTime time, String label, SynchronizationPointFailureReason reasonParam)
            : base(federate, time)
        {
            this.synchronizationPointLabel = label;
            this.reason = reasonParam;
        }

        public override void Call()
        {
            // registration failed //
            federate.SynchronizationPointRegistrationFailed(synchronizationPointLabel, reason);
        }
    }

    public sealed class AnnounceSynchronizationPointCallback : Callback
    {
        private String synchronizationPointLabel;
        private byte[] userSuppliedTag;
        public AnnounceSynchronizationPointCallback(IFederateAmbassador federate, ILogicalTime time, String label, byte[] tag)
            : base(federate, time)
        {
            this.synchronizationPointLabel = label;
            this.userSuppliedTag = tag;
        }

        public override void Call()
        {
            // register point with the LRC //
            //TODO federate.State.RegisterSyncPoint(synchronizationPointLabel);

            federate.AnnounceSynchronizationPoint(synchronizationPointLabel, userSuppliedTag);
        }
    }

    public sealed class FederationSynchronizedCallback : Callback
    {
        private String synchronizationPointLabel;
        public FederationSynchronizedCallback(IFederateAmbassador federate, ILogicalTime time, String label)
            : base(federate, time)
        {
            this.synchronizationPointLabel = label;
        }

        public override void Call()
        {
            // notify the LRC of the change //
            //TODO federate.State.FederationSynchronized(synchronizationPointLabel);

            federate.FederationSynchronized(synchronizationPointLabel);
        }
    }

    public sealed class DiscoverObjectInstanceCallback : Callback
    {
        private IObjectInstanceHandle theObject;
        private IObjectClassHandle theObjectClass;
        private System.String objectName;

        public DiscoverObjectInstanceCallback(IFederateAmbassador federate, IObjectInstanceHandle theObjectParam, IObjectClassHandle theObjectClassParam, string objectNameParam)
            : base(federate)
        {
            this.theObject = theObjectParam;
            this.theObjectClass = theObjectClassParam;
            this.objectName = objectNameParam;
        }

        public DiscoverObjectInstanceCallback(IFederateAmbassador federate, ILogicalTime time, long federateHandle, long interactionIndex, IObjectInstanceHandle theObjectParam, IObjectClassHandle theObjectClassParam, string objectNameParam)
            : base(federate, time, federateHandle, interactionIndex)
        {
            this.theObject = theObjectParam;
            this.theObjectClass = theObjectClassParam;
            this.objectName = objectNameParam;
        }

        public override void Call()
        {
            // register the object //
            //TODO federate.State.getLRCInstanceRepository().AddInstance(instance, discoveredClass);

            federate.DiscoverObjectInstance(theObject, theObjectClass, objectName);
        }

        public override String ToString()
        {
            return "DiscoverObjectInstanceCallback[" + theObject + ";" + theObjectClass + ";" + objectName + "]";
        }
    }

    public class RemoveObjectInstanceCallback : Callback
    {
        protected IObjectInstanceHandle theObject;
        protected byte[] userSuppliedTag;
        protected OrderType sentOrdering;
        public RemoveObjectInstanceCallback(IFederateAmbassador federate, ILogicalTime time, IObjectInstanceHandle theObjectParam, byte[] userSuppliedTagParam, OrderType sentOrderingParam)
            : base(federate, time)
        {
            this.theObject = theObjectParam;
            this.userSuppliedTag = userSuppliedTagParam;
            this.sentOrdering = sentOrderingParam;
        }

        public override void Call()
        {
            // remove the instance //
            //TODO federate.State.getLRCInstanceRepository().removeInstance(objectHandle);

            federate.RemoveObjectInstance(theObject, userSuppliedTag, sentOrdering);
        }
    }

    public sealed class TimeStampedRemoveObjectInstanceCallback : RemoveObjectInstanceCallback
    {
        private ILogicalTime timeParam;
        private OrderType receivedOrdering;
        public TimeStampedRemoveObjectInstanceCallback(IFederateAmbassador federate, ILogicalTime time, IObjectInstanceHandle theObjectParam, byte[] userSuppliedTagParam, OrderType sentOrderingParam, ILogicalTime theTimeParam, OrderType receivedOrderingParam)
            : base(federate, time, theObjectParam, userSuppliedTagParam, sentOrderingParam)
        {
            this.timeParam = theTimeParam;
            this.receivedOrdering = receivedOrderingParam;
        }

        public override void Call()
        {
            // remove the instance //
            //TODO federate.State.getLRCInstanceRepository().removeInstance(objectHandle);
            federate.RemoveObjectInstance(theObject, userSuppliedTag, sentOrdering, timeParam, receivedOrdering);
        }
    }

    public sealed class ReceiveInteractionCallback : Callback
    {
        private IInteractionClassHandle interactionClass;
        private IParameterHandleValueMap theParameters;
        private byte[] userSuppliedTag;
        public ReceiveInteractionCallback(IFederateAmbassador federate, ILogicalTime time, IInteractionClassHandle interactionClassParam, IParameterHandleValueMap theParametersParam, byte[] userSuppliedTagParam)
            : base(federate, time)
        {
            this.interactionClass = interactionClassParam;
            this.theParameters = theParametersParam;
            this.userSuppliedTag = userSuppliedTagParam;
        }

        public override void Call()
        {
            // TODO ReceiveInteraction has several forms //
            federate.ReceiveInteraction(interactionClass, theParameters, userSuppliedTag, null, null);
        }
    }

    public sealed class ReflectAttributeValuesCallback : Callback
    {
        private IObjectInstanceHandle theObject;
        private IAttributeHandleValueMap theAttributes;
        private byte[] userSuppliedTag;
        public ReflectAttributeValuesCallback(IFederateAmbassador federate, ILogicalTime time, IObjectInstanceHandle theObjectParam, IAttributeHandleValueMap theAttributesParam, byte[] userSuppliedTagParam)
            : base(federate, time)
        {
            this.theObject = theObjectParam;
            this.theAttributes = theAttributesParam;
            this.userSuppliedTag = userSuppliedTagParam;
        }

        public override void Call()
        {
            // TODO ReflectAttributeValues has several forms //
            federate.ReflectAttributeValues(theObject, theAttributes, userSuppliedTag, null, null);
        }
    }

    public sealed class ReflectAttributeValuesExtCallback : Callback
    {
        private IObjectInstanceHandle theObject;
        private HLAattributeHandleValuePair[] theAttributes;
        private byte[] userSuppliedTag;

        public ReflectAttributeValuesExtCallback(IFederateAmbassador federate, IObjectInstanceHandle theObjectParam, HLAattributeHandleValuePair[] theAttributesParam, byte[] userSuppliedTagParam)
            : base(federate)
        {
            this.theObject = theObjectParam;
            this.theAttributes = theAttributesParam;
            this.userSuppliedTag = userSuppliedTagParam;
        }

        public ReflectAttributeValuesExtCallback(IFederateAmbassador federate, ILogicalTime time, long federateHandle, long interactionIndex, IObjectInstanceHandle theObjectParam, HLAattributeHandleValuePair[] theAttributesParam, byte[] userSuppliedTagParam)
            : base(federate, time, federateHandle, interactionIndex)
        {
            this.theObject = theObjectParam;
            this.theAttributes = theAttributesParam;
            this.userSuppliedTag = userSuppliedTagParam;
        }

        public override void Call()
        {
            // TODO ReflectAttributeValues has several forms //
            ((ISxtaFederateAmbassador)federate).ReflectAttributeValuesExt(theObject, theAttributes, userSuppliedTag, null, null);
        }

        public override String ToString()
        {
            String attributesString = "{ ";
            
            for (int i = 0; i < theAttributes.Length; i++)
            {
                attributesString = attributesString + theAttributes[i].ToString();
            }
            attributesString = attributesString + " }";

            return "ReflectAttributeValuesExtCallback[" + theObject + ";" + attributesString + "]";
        }
    }

    public sealed class ProvideAttributeValueUpdateCallback : Callback
    {
        private IObjectInstanceHandle theObject;
        private IAttributeHandleSet theAttributes;
        private byte[] userSuppliedTag;
        public ProvideAttributeValueUpdateCallback(IFederateAmbassador federate, ILogicalTime time, IObjectInstanceHandle theObjectParam, IAttributeHandleSet theAttributesParam, byte[] userSuppliedTagParam)
            : base(federate, time)
        {
            this.theObject = theObjectParam;
            this.theAttributes = theAttributesParam;
            this.userSuppliedTag = userSuppliedTagParam;
        }

        public override void Call()
        {
            federate.ProvideAttributeValueUpdate(theObject, theAttributes, userSuppliedTag);
        }
    }
}
