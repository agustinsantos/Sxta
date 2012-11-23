using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.BoostrapProtocol;

namespace Sxta.Rti1516.Ambassadors
{
    // PATCH ANGEL: Lo que implementaba anteriormente --> IHlaCreateObjectRootListener, IHLAobjectRootListener
    //              En la region LO_QUE_HABÍA_ANTES se encuentra todo el código anterior cuando está clase implementaba los interfaces anteriores
    //              Esta clase debería ser internal no public. Está así porque me daba un error de incoherencia de accesibilidad que no sabía corregir
    public abstract class BaseAmbassador : ISxtaFederateAmbassador
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
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The list of interaction listeners.
        /// </summary>
        protected List<IInteractionListener> interactionListeners = new List<IInteractionListener>();

        /// <summary> Maps object instance handles to object instance proxies.</summary>
        protected IDictionary<IObjectInstanceHandle, HLAobjectRoot> objectInstanceHandleProxyMap = new Dictionary<IObjectInstanceHandle, HLAobjectRoot>();

        protected IList<ObjectInstanceDescriptor> objectInstanceDescriptorList = new List<ObjectInstanceDescriptor>();

        protected IRTIambassador rtiAmbassador;

        /// <summary> The object handle counter.</summary>
        protected internal long handleCounter = 0;
        public long HandleCounter
        {
            get { return Interlocked.Increment(ref handleCounter); }
        }

        protected void InitHandleCounter()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            byte[] randomBytes = new byte[56];
            random.NextBytes(randomBytes);

            byte[] maskBytes = new byte[8];
            for (int i = 0; i < maskBytes.Length; i++)
                maskBytes.SetValue((byte)0, i);

            byte[] firstHandler = new byte[64];

            randomBytes.CopyTo(firstHandler, 0);
            maskBytes.CopyTo(firstHandler, 56);

            handleCounter = BitConverter.ToInt64(firstHandler, 0);
        }

        public BaseAmbassador(IRTIambassador prtiAmbassador)
        {
            rtiAmbassador = prtiAmbassador;

            InitHandleCounter();
        }

        #region ISxtaFederateAmbassador Members

        // TODO ANGEL: Se añade un método de actualización específico para las propiedades de los objetos.
        //             Es debido a que usaban un IAttributeHandleValueMap que imponía una serie de problemas: implicaba la conversión de los values de object a byte[] 
        public virtual void ReflectAttributeValuesExt(IObjectInstanceHandle theObject, HLAattributeHandleValuePair[] theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RegisterObjectInstance(object obj)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void DumpObjects()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IFederateAmbassador Members

        public virtual void SynchronizationPointRegistrationSucceeded(string synchronizationPointLabel)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void SynchronizationPointRegistrationFailed(string synchronizationPointLabel, SynchronizationPointFailureReason reason)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AnnounceSynchronizationPoint(string synchronizationPointLabel, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationSynchronized(string synchronizationPointLabel)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void InitiateFederateSave(string label)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void InitiateFederateSave(string label, ILogicalTime time)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationSaved()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationNotSaved(SaveFailureReason reason)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationSaveStatusResponse(FederateHandleSaveStatusPair[] response)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RequestFederationRestoreSucceeded(string label)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RequestFederationRestoreFailed(string label)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationRestoreBegun()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void InitiateFederateRestore(string label, IFederateHandle federateHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationRestored()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationNotRestored(RestoreFailureReason reason)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void FederationRestoreStatusResponse(FederateHandleRestoreStatusPair[] response)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void StartRegistrationForObjectClass(IObjectClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void StopRegistrationForObjectClass(IObjectClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TurnInteractionsOn(IInteractionClassHandle theHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TurnInteractionsOff(IInteractionClassHandle theHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ObjectInstanceNameReservationSucceeded(string objectName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ObjectInstanceNameReservationFailed(string objectName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void DiscoverObjectInstance(IObjectInstanceHandle theObject, IObjectClassHandle theObjectClass, string objectName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, IRegionHandleSet sentRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IRegionHandleSet sentRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle retractionHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle retractionHandle, IRegionHandleSet sentRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, IRegionHandleSet sentRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IRegionHandleSet sentRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle messageRetractionHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle messageRetractionHandle, IRegionHandleSet sentRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RemoveObjectInstance(IObjectInstanceHandle theObject, byte[] userSuppliedTag, OrderType sentOrdering)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RemoveObjectInstance(IObjectInstanceHandle theObject, byte[] userSuppliedTag, OrderType sentOrdering, ILogicalTime theTime, OrderType receivedOrdering)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RemoveObjectInstance(IObjectInstanceHandle theObject, byte[] userSuppliedTag, OrderType sentOrdering, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle retractionHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AttributesInScope(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AttributesOutOfScope(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ProvideAttributeValueUpdate(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TurnUpdatesOnForObjectInstance(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TurnUpdatesOffForObjectInstance(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RequestAttributeOwnershipAssumption(IObjectInstanceHandle theObject, IAttributeHandleSet offeredAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RequestDivestitureConfirmation(IObjectInstanceHandle theObject, IAttributeHandleSet offeredAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AttributeOwnershipAcquisitionNotification(IObjectInstanceHandle theObject, IAttributeHandleSet securedAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AttributeOwnershipUnavailable(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RequestAttributeOwnershipRelease(IObjectInstanceHandle theObject, IAttributeHandleSet candidateAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ConfirmAttributeOwnershipAcquisitionCancellation(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void InformAttributeOwnership(IObjectInstanceHandle theObject, IAttributeHandle theAttribute, IFederateHandle theOwner)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AttributeIsNotOwned(IObjectInstanceHandle theObject, IAttributeHandle theAttribute)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void AttributeIsOwnedByRTI(IObjectInstanceHandle theObject, IAttributeHandle theAttribute)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TimeRegulationEnabled(ILogicalTime time)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TimeConstrainedEnabled(ILogicalTime time)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void TimeAdvanceGrant(ILogicalTime theTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RequestRetraction(IMessageRetractionHandle theHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}