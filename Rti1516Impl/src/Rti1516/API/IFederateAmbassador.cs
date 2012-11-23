namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// The interface used by the run time infrastructure to interact with
	/// the federate.
	/// </summary>
	public interface IFederateAmbassador
	{
		/// <summary> 
		/// Notifies the federate that the synchronization point registration operation has
		/// succeeded.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the registered synchronization point
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  SynchronizationPointRegistrationSucceeded(System.String synchronizationPointLabel);
		
		/// <summary> 
		/// Notifies the federate that the synchronization point registration operation has
		/// failed.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the synchronization point
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  SynchronizationPointRegistrationFailed(System.String synchronizationPointLabel, SynchronizationPointFailureReason reason);
		
		/// <summary> 
		/// Notifies the federate that a synchronization point has been announced.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the announced synchronization point
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the announcement
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AnnounceSynchronizationPoint(System.String synchronizationPointLabel, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Notifies the federate that the federation has been synchronized.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the synchronization point
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationSynchronized(System.String synchronizationPointLabel);
		
		/// <summary> 
		/// Notifies the federate that it should initiate a save operation.
		/// </summary>
		/// <param name="label">the label to save under
		/// </param>
		/// <exception cref="UnableToPerformSave">  if the federate cannot perform the operation
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  InitiateFederateSave(System.String label);
		
		/// <summary> 
		/// Notifies the federate that it should initiate a save operation.
		/// </summary>
		/// <param name="label">the label to save under
		/// </param>
		/// <param name="time">the time of the save operation
		/// </param>
		/// <exception cref="InvalidLogicalTime">  if the specified time is invalid
		/// </exception>
		/// <exception cref="UnableToPerformSave">  if the federate cannot perform the operation
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  InitiateFederateSave(System.String label, ILogicalTime time);
		
		/// <summary> 
		/// Notifies the federate that the federation has been saved.
		/// </summary>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationSaved();
		
		/// <summary> 
		/// Notifies the federate that the federation has not been saved.
		/// </summary>
		/// <param name="reason">the reason for the failure of the save operation
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationNotSaved(SaveFailureReason reason);
		
		/// <summary> 
		/// Provides information to the federate concerning the save status of other
		/// members of the federation.
		/// </summary>
		/// <param name="response">the responses associated with each federate
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationSaveStatusResponse(FederateHandleSaveStatusPair[] response);
		
		/// <summary> 
		/// Notifies the federate that its request to restore the saved state of the
		/// federation has succeeded.
		/// </summary>
		/// <param name="label">the label of the stored state to restore
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RequestFederationRestoreSucceeded(System.String label);
		
		/// <summary> 
		/// Notifies the federate that its request to restore the saved state of the
		/// federation has failed.
		/// </summary>
		/// <param name="label">the label of the stored state to restore
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RequestFederationRestoreFailed(System.String label);
		
		/// <summary> 
		/// Notifies the federate that the federation restore operation has begun.
		/// </summary>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationRestoreBegun();
		
		/// <summary> 
		/// Notifies the federate that it should initiate a restore operation.
		/// </summary>
		/// <param name="label">the label of the stored state to restore
		/// </param>
		/// <param name="federateHandle">the federate handle
		/// </param>
		/// <exception cref="SpecifiedSaveLabelDoesNotExist">  if the specified save label does not exist
		/// </exception>
		/// <exception cref="CouldNotInitiateRestore">  if the restore operation cannot be performed
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  InitiateFederateRestore(System.String label, IFederateHandle federateHandle);
		
		/// <summary> 
		/// Notifies the federate that the federation has been restored.
		/// </summary>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationRestored();
		
		/// <summary> 
		/// Notifies the federate that the federation has not been restored.
		/// </summary>
		/// <param name="reason">the reason for the failure of the restore operation
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationNotRestored(RestoreFailureReason reason);
		
		/// <summary> 
		/// Provides information to the federate concerning the restore status of other
		/// members of the federation.
		/// </summary>
		/// <param name="response">the responses associated with each federate
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  FederationRestoreStatusResponse(FederateHandleRestoreStatusPair[] response);
		
		/// <summary> 
		/// Notifies the federate that it should begin registering objects of a certain class.
		/// </summary>
		/// <param name="theClass">the handle of the class of objects to begin registering
		/// </param>
		/// <exception cref="ObjectClassNotPublished">  if the specified object class is not published
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  StartRegistrationForObjectClass(IObjectClassHandle theClass);
		
		/// <summary> 
		/// Notifies the federate that it should Stop registering objects of a certain class.
		/// </summary>
		/// <param name="theClass">the handle of the class of objects to Stop registering
		/// </param>
		/// <exception cref="ObjectClassNotPublished">  if the specified object class is not published
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  StopRegistrationForObjectClass(IObjectClassHandle theClass);
		
		/// <summary> 
		/// Notifies the federate that it should turn interactions of a certain class on.
		/// </summary>
		/// <param name="theHandle">the handle of the class of interactions to turn on
		/// </param>
		/// <exception cref="InteractionClassNotPublished">  if the specified interaction class is not published
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TurnInteractionsOn(IInteractionClassHandle theHandle);
		
		/// <summary> 
		/// Notifies the federate that it should turn interactions of a certain class off.
		/// </summary>
		/// <param name="theHandle">the handle of the class of interactions to turn off
		/// </param>
		/// <exception cref="InteractionClassNotPublished">  if the specified interaction class is not published
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TurnInteractionsOff(IInteractionClassHandle theHandle);
		
		/// <summary> 
		/// Notifies the federate that its request to reserve an object instance name has
		/// succeeded.
		/// </summary>
		/// <param name="objectName">the reserved object instance name
		/// </param>
		/// <exception cref="UnknownName">  if the name is unknown
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ObjectInstanceNameReservationSucceeded(System.String objectName);
		
		/// <summary> 
		/// Notifies the federate that its request to reserve an object instance name has
		/// failed.
		/// </summary>
		/// <param name="objectName">the object instance name
		/// </param>
		/// <exception cref="UnknownName">  if the name is unknown
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ObjectInstanceNameReservationFailed(System.String objectName);
		
		/// <summary> 
		/// Notifies the federate of the presence of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the newly discovered object
		/// </param>
		/// <param name="theObjectClass">the class handle of the newly discovered object
		/// </param>
		/// <param name="objectName">the name of the newly discovered object
		/// </param>
		/// <exception cref="CouldNotDiscover">  if the object could not be discovered
		/// </exception>
		/// <exception cref="ObjectClassNotRecognized">  if the object class was not recognized
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  DiscoverObjectInstance(IObjectInstanceHandle theObject, IObjectClassHandle theObjectClass, System.String objectName);
		
		/// <summary> 
		/// Notifies the federate of changes to the state of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the modified object
		/// </param>
		/// <param name="theAttributes">the map between attribute handles and the new values of
		/// the identified attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the state change
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the update was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the update
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is not known
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if the attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to the
		/// attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport);
		
		/// <summary> 
		/// Notifies the federate of changes to the state of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the modified object
		/// </param>
		/// <param name="theAttributes">the map between attribute handles and the new values of
		/// the identified attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the state change
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the update was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the update
		/// </param>
		/// <param name="sentRegions">the set of region handles identifying the regions associated
		/// with the attribute update
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is not known
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if the attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to the
		/// attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, IRegionHandleSet sentRegions);
		
		/// <summary> 
		/// Notifies the federate of changes to the state of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the modified object
		/// </param>
		/// <param name="theAttributes">the map between attribute handles and the new values of
		/// the identified attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the state change
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the update was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the update
		/// </param>
		/// <param name="theTime">the logical time associated with the attribute update
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the update was received
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is not known
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if the attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to the
		/// attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering);
		
		/// <summary> 
		/// Notifies the federate of changes to the state of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the modified object
		/// </param>
		/// <param name="theAttributes">the map between attribute handles and the new values of
		/// the identified attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the state change
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the update was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the update
		/// </param>
		/// <param name="theTime">the logical time associated with the attribute update
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the update was received
		/// </param>
		/// <param name="sentRegions">the set of region handles identifying the regions associated
		/// with the attribute update
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is not known
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if the attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to the
		/// attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IRegionHandleSet sentRegions);
		
		/// <summary>
		///  Notifies the federate of changes to the state of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the modified object
		/// </param>
		/// <param name="theAttributes">the map between attribute handles and the new values of
		/// the identified attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the state change
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the update was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the update
		/// </param>
		/// <param name="theTime">the logical time associated with the attribute update
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the update was received
		/// </param>
		/// <param name="retractionHandle">the message retraction handle
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is not known
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if the attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to the
		/// attribute
		/// </exception>
		/// <exception cref="InvalidLogicalTime">  if the specified logical time was invalid
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle retractionHandle);
		
		/// <summary> 
		/// Notifies the federate of changes to the state of an object instance.
		/// </summary>
		/// <param name="theObject">the instance handle of the modified object
		/// </param>
		/// <param name="theAttributes">the map between attribute handles and the new values of
		/// the identified attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the state change
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the update was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the update
		/// </param>
		/// <param name="theTime">the logical time associated with the attribute update
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the update was received
		/// </param>
		/// <param name="retractionHandle">the message retraction handle
		/// </param>
		/// <param name="sentRegions">the set of region handles identifying the regions associated
		/// with the attribute update
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is not known
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if the attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to the
		/// attribute
		/// </exception>
		/// <exception cref="InvalidLogicalTime">  if the specified logical time was invalid
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReflectAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle retractionHandle, IRegionHandleSet sentRegions);
		
		/// <summary> 
		/// Notifies the federate of a received interaction.
		/// </summary>
		/// <param name="interactionClass">the class of the received interaction
		/// </param>
		/// <param name="theParameters">the map between parameter handles and the values of
		/// the identified parameters
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the interaction
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
		/// </exception>
		/// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
		/// recognized
		/// </exception>
		/// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
		/// interaction class
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport);
		
		/// <summary> 
		/// Notifies the federate of a received interaction.
		/// </summary>
		/// <param name="interactionClass">the class of the received interaction
		/// </param>
		/// <param name="theParameters">the map between parameter handles and the values of
		/// the identified parameters
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the interaction
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the interaction
		/// </param>
		/// <param name="sentRegions">the set of region handles identifying the regions associated
		/// with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
		/// </exception>
		/// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
		/// recognized
		/// </exception>
		/// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
		/// interaction class
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, IRegionHandleSet sentRegions);
		
		/// <summary>
		///  Notifies the federate of a received interaction.
		/// </summary>
		/// <param name="interactionClass">the class of the received interaction
		/// </param>
		/// <param name="theParameters">the map between parameter handles and the values of
		/// the identified parameters
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the interaction
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the interaction
		/// </param>
		/// <param name="theTime">the logical time associated with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
		/// </exception>
		/// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
		/// recognized
		/// </exception>
		/// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
		/// interaction class
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering);
		
		/// <summary> 
		/// Notifies the federate of a received interaction.
		/// </summary>
		/// <param name="interactionClass">the class of the received interaction
		/// </param>
		/// <param name="theParameters">the map between parameter handles and the values of
		/// the identified parameters
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the interaction
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the interaction
		/// </param>
		/// <param name="theTime">the logical time associated with the interaction
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the interaction was received
		/// </param>
		/// <param name="sentRegions">the set of region handles identifying the regions associated
		/// with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
		/// </exception>
		/// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
		/// recognized
		/// </exception>
		/// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
		/// interaction class
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IRegionHandleSet sentRegions);
		
		/// <summary> 
		/// Notifies the federate of a received interaction.
		/// </summary>
		/// <param name="interactionClass">the class of the received interaction
		/// </param>
		/// <param name="theParameters">the map between parameter handles and the values of
		/// the identified parameters
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the interaction
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the interaction
		/// </param>
		/// <param name="theTime">the logical time associated with the interaction
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the interaction was received
		/// </param>
		/// <param name="messageRetractionHandle">the message retraction handle associated with the
		/// interaction
		/// </param>
		/// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
		/// </exception>
		/// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
		/// recognized
		/// </exception>
		/// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
		/// interaction class
		/// </exception>
		/// <exception cref="InvalidLogicalTime">  if the specified logical time was invalid
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle messageRetractionHandle);
		
		/// <summary>
		///  Notifies the federate of a received interaction.
		/// </summary>
		/// <param name="interactionClass">the class of the received interaction
		/// </param>
		/// <param name="theParameters">the map between parameter handles and the values of
		/// the identified parameters
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the interaction
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTransport">the type of transport associated with the interaction
		/// </param>
		/// <param name="theTime">the logical time associated with the interaction
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the interaction was received
		/// </param>
		/// <param name="messageRetractionHandle">the message retraction handle associated with the
		/// interaction
		/// </param>
		/// <param name="sentRegions">the set of region handles identifying the regions associated
		/// with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
		/// </exception>
		/// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
		/// recognized
		/// </exception>
		/// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
		/// interaction class
		/// </exception>
		/// <exception cref="InvalidLogicalTime">  if the specified logical time was invalid
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ReceiveInteraction(IInteractionClassHandle interactionClass, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle messageRetractionHandle, IRegionHandleSet sentRegions);
		
		/// <summary>
		///  Notifies the federate that an object instance has been removed.
		/// </summary>
		/// <param name="theObject">the instance handle associated with the object
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the removal operation
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RemoveObjectInstance(IObjectInstanceHandle theObject, byte[] userSuppliedTag, OrderType sentOrdering);
		
		/// <summary> 
		/// Notifies the federate that an object instance has been removed.
		/// </summary>
		/// <param name="theObject">the instance handle associated with the object
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the removal operation
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTime">the logical time associated with the removal operation
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the interaction was received
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RemoveObjectInstance(IObjectInstanceHandle theObject, byte[] userSuppliedTag, OrderType sentOrdering, ILogicalTime theTime, OrderType receivedOrdering);
		
		/// <summary> 
		/// Notifies the federate that an object instance has been removed.
		/// </summary>
		/// <param name="theObject">the instance handle associated with the object
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the removal operation
		/// </param>
		/// <param name="sentOrdering">the type of ordering with which the interaction was sent
		/// </param>
		/// <param name="theTime">the logical time associated with the removal operation
		/// </param>
		/// <param name="receivedOrdering">the type of ordering with which the interaction was received
		/// </param>
		/// <param name="retractionHandle">the message retraction handle associated with the interaction
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="InvalidLogicalTime">  if the specified logical time was invalid
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RemoveObjectInstance(IObjectInstanceHandle theObject, byte[] userSuppliedTag, OrderType sentOrdering, ILogicalTime theTime, OrderType receivedOrdering, IMessageRetractionHandle retractionHandle);
		
		/// <summary> 
		/// Notifies the federate that a set of attributes have entered its scope.
		/// </summary>
		/// <param name="theObject">the handle of the object instance whose attributes have entered
		/// scope
		/// </param>
		/// <param name="theAttributes">the set of attribute handles identifying the attributes that
		/// have entered scope
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an identified attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to an identified
		/// attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AttributesInScope(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Notifies the federate that a set of attributes have left its scope.
		/// </summary>
		/// <param name="theObject">the handle of the object instance whose attributes have left
		/// scope
		/// </param>
		/// <param name="theAttributes">the set of attribute handles identifying the attributes that
		/// have left scope
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an identified attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotSubscribed">  if the federate had not subscribed to an identified
		/// attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AttributesOutOfScope(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Notifies the federate that it should provide an update regarding a set of object
		/// attributes.
		/// </summary>
		/// <param name="theObject">the handle of the object instance whose attributes should be sent
		/// </param>
		/// <param name="theAttributes">the set of attribute handles identifying the attributes that
		/// should be sent
		/// </param>
        /// <param name="userSuppliedTag">a user-supplied tag associated with the operation
        /// </param>
        /// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an identified attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotOwned">  if the federate did not own a specified attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ProvideAttributeValueUpdate(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Notifies the federate that it should turn updates on for an owned object instance.
		/// </summary>
		/// <param name="theObject">the handle of the object instance whose attributes should be sent
		/// </param>
		/// <param name="theAttributes">the set of attribute handles identifying the attributes that
		/// should be sent
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an identified attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotOwned">  if the federate did not own a specified attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TurnUpdatesOnForObjectInstance(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Notifies the federate that it should turn updates off for an owned object instance.
		/// </summary>
		/// <param name="theObject">the handle of the object instance whose attributes should not be sent
		/// </param>
		/// <param name="theAttributes">the set of attribute handles identifying the attributes that
		/// should not be sent
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an identified attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotOwned">  if the federate did not own a specified attribute
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TurnUpdatesOffForObjectInstance(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary>
		///  Requests that the federate assume ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attributes are
		/// associated
		/// </param>
		/// <param name="offeredAttributes">the set of handles to the attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the transfer
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeAlreadyOwned">  if an attribute was already owned
		/// </exception>
		/// <exception cref="AttributeNotPublished">  if an attribute is not published
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RequestAttributeOwnershipAssumption(IObjectInstanceHandle theObject, IAttributeHandleSet offeredAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Requests that the federate confirm divestiture of a set of attributes.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attributes are
		/// associated
		/// </param>
		/// <param name="offeredAttributes">the set of handles to the attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the transfer
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotOwned">  if an attribute was not owned
		/// </exception>
		/// <exception cref="AttributeDivestitureWasNotRequested">  if divestiture of a specified
		/// attribute was not requested
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RequestDivestitureConfirmation(IObjectInstanceHandle theObject, IAttributeHandleSet offeredAttributes);
		
		/// <summary> 
		/// Notifies the federate that it has acquired ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attributes are
		/// associated
		/// </param>
		/// <param name="securedAttributes">the set of handles to the attributes
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the transfer
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeAcquisitionWasNotRequested">  if acquisition of a specified
		/// attribute was not requested
		/// </exception>
		/// <exception cref="AttributeAlreadyOwned">  if an attribute was already owned
		/// </exception>
		/// <exception cref="AttributeNotPublished">  if an attribute is not published
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AttributeOwnershipAcquisitionNotification(IObjectInstanceHandle theObject, IAttributeHandleSet securedAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Notifies the federate that ownership of a set of attributes is unavailable.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attributes are
		/// associated
		/// </param>
		/// <param name="theAttributes">the set of handles to the attributes
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized">  if an attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeAlreadyOwned">  if an attribute was already owned
		/// </exception>
		/// <exception cref="AttributeAcquisitionWasNotRequested">  if acquisition of a specified
		/// attribute was not requested
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AttributeOwnershipUnavailable(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Notifies the federate of a request to release ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attributes are
		/// associated
		/// </param>
		/// <param name="candidateAttributes">the attributes that have been requested to be released
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag associated with the request
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized"> if an attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if an attribute was not owned
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RequestAttributeOwnershipRelease(IObjectInstanceHandle theObject, IAttributeHandleSet candidateAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Notifies the federate of confirmation of attribute ownership acquisition cancellation.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attributes are
		/// associated
		/// </param>
		/// <param name="theAttributes">a set of handles to the attributes
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized"> if an attribute was not recognized
		/// </exception>
		/// <exception cref="AttributeAlreadyOwned"> if an attribute was already owned
		/// </exception>
		/// <exception cref="AttributeAcquisitionWasNotCanceled"> if the attribute acquisition operation
		/// was not canceled
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  ConfirmAttributeOwnershipAcquisitionCancellation(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Notifies the federate of the ownership of an attribute.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attribute is
		/// associated
		/// </param>
		/// <param name="theAttribute">a handle to the attribute
		/// </param>
		/// <param name="theOwner">a handle to the owner of the attribute
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized"> if the attribute was not recognized
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  InformAttributeOwnership(IObjectInstanceHandle theObject, IAttributeHandle theAttribute, IFederateHandle theOwner);
		
		/// <summary>
		///  Notifies the federate that an attribute if not owned.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attribute is
		/// associated
		/// </param>
		/// <param name="theAttribute">a handle to the attribute
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized"> if the attribute was not recognized
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AttributeIsNotOwned(IObjectInstanceHandle theObject, IAttributeHandle theAttribute);
		
		/// <summary> 
		/// Notifies the federate that an attribute is owned by the run-time infrastructure.
		/// </summary>
		/// <param name="theObject">a handle to the object instance with which the attribute is
		/// associated
		/// </param>
		/// <param name="theAttribute">a handle to the attribute
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance was unknown
		/// </exception>
		/// <exception cref="AttributeNotRecognized"> if the attribute was not recognized
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  AttributeIsOwnedByRTI(IObjectInstanceHandle theObject, IAttributeHandle theAttribute);
		
		/// <summary> 
		/// Notifies the federate that time regulation has been enabled.
		/// </summary>
		/// <param name="time">the current logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time was invalid
		/// </exception>
		/// <exception cref="NoRequestToEnableTimeRegulationWasPending"> if no request to
		/// enable time regulation was pending
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TimeRegulationEnabled(ILogicalTime time);
		
		/// <summary> 
		/// Notifies the federate that time-constrained mode has been enabled.
		/// </summary>
		/// <param name="time">the current logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time was invalid
		/// </exception>
		/// <exception cref="NoRequestToEnableTimeConstrainedWasPending"> if no request to
		/// enable time-constrained mode was pending
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TimeConstrainedEnabled(ILogicalTime time);
		
		/// <summary> 
		/// Notifies the federate that a time advance has been granted.
		/// </summary>
		/// <param name="theTime">the new logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="JoinedFederateIsNotInTimeAdvancingState"> if the federate is not
		/// in a time advancing state
		/// </exception>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  TimeAdvanceGrant(ILogicalTime theTime);
		
		/// <summary> 
		/// Notifies the federate that a retraction has been requested.
		/// </summary>
		/// <param name="theHandle">the handle identifying the message to be retracted
		/// </param>
		/// <exception cref="FederateInternalError">  if an error occurs in the federate
		/// </exception>
		void  RequestRetraction(IMessageRetractionHandle theHandle);
	}
}