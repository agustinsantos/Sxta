namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// The principal interface of the run-time infrastructure.
	/// </summary>
	public interface IRTIambassador
	{
		/// <summary> 
		/// Returns the High Level Architecture specification version to which the
		/// run-time infrastructure conforms.
		/// </summary>
		/// <returns> the High Level Architecture specification version to which the
		/// run-time infrastructure conforms
		/// </returns>
		System.String HLAversion
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's attribute handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's attribute handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IAttributeHandleFactory AttributeHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's attribute handle set factory.
		/// </summary>
		/// <returns> the run-time infrastructure's attribute handle set factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		AttributeHandleSetFactory AttributeHandleSetFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's attribute handle value map factory.
		/// </summary>
		/// <returns> the run-time infrastructure's attribute handle value map factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IAttributeHandleValueMapFactory AttributeHandleValueMapFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's attribute set region set pair list factory.
		/// </summary>
		/// <returns> the run-time infrastructure's attribute set region set pair list factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IAttributeSetRegionSetPairListFactory AttributeSetRegionSetPairListFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's dimension handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's dimension handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IDimensionHandleFactory DimensionHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's dimension handle set factory.
		/// </summary>
		/// <returns> the run-time infrastructure's dimension handlet set factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IDimensionHandleSetFactory DimensionHandleSetFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's federate handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's federate handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IFederateHandleFactory FederateHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's federate handle set factory.
		/// </summary>
		/// <returns> the run-time infrastructure's federate handle set factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IFederateHandleSetFactory FederateHandleSetFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's interaction class handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's interaction class handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IInteractionClassHandleFactory InteractionClassHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's object class handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's object class handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IObjectClassHandleFactory ObjectClassHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's object instance handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's object instance handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IObjectInstanceHandleFactory ObjectInstanceHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's parameter handle factory.
		/// </summary>
		/// <returns> the run-time infrastructure's parameter handle factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IParameterHandleFactory ParameterHandleFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's parameter handle value map factory.
		/// </summary>
		/// <returns> the run-time infrastructure's parameter handle value map factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IParameterHandleValueMapFactory ParameterHandleValueMapFactory
		{
			get;
			
		}

		/// <summary> 
		/// Returns the run-time infrastructure's region handle set factory.
		/// </summary>
		/// <returns> the run-time infrastructure's region handle set factory
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of an execution
		/// </exception>
		IRegionHandleSetFactory RegionHandleSetFactory
		{
			get;
			
		}

		
		/// <summary> 
		/// Creates a new federation execution.
		/// </summary>
		/// <param name="federationExecutionName">the name of the new federation execution
		/// </param>
		/// <param name="fdd">the location of the federation description document
		/// </param>
		/// <exception cref="FederationExecutionAlreadyExists"> if the execution already exists
		/// </exception>
		/// <exception cref="CouldNotOpenFDD"> if the federation description document could not
		/// be opened
		/// </exception>
		/// <exception cref="ErrorReadingFDD"> if an error occurred while reading the federation
		/// description document
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  CreateFederationExecution(System.String federationExecutionName, System.Uri fdd);
		
		/// <summary> 
		/// Destroys a federation execution.
		/// </summary>
		/// <param name="federationExecutionName">the name of the federation execution to destroy
		/// </param>
		/// <exception cref="FederatesCurrentlyJoined"> if federates are still participating in the
		/// execution
		/// </exception>
		/// <exception cref="FederationExecutionDoesNotExist"> if the federation execution does not
		/// exist
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DestroyFederationExecution(System.String federationExecutionName);
		
		/// <summary> 
		/// Joins a federation execution.
		/// </summary>
		/// <param name="federateType">a string describing the federate's role in the federation
		/// </param>
		/// <param name="federationExecutionName">the name of the federation to join
		/// </param>
		/// <param name="federateReference">the federate ambassador object
		/// </param>
		/// <param name="serviceReferences">the federate's mobile services
		/// </param>
		/// <exception cref="FederateAlreadyExecutionMember"> if the federate is already a member of
		/// an execution
		/// </exception>
		/// <exception cref="FederationExecutionDoesNotExist"> if the federation execution does not
		/// exist
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IFederateHandle JoinFederationExecution(System.String federateType, System.String federationExecutionName, IFederateAmbassador federateReference, MobileFederateServices serviceReferences);
		
		/// <summary> 
		/// Resigns from the currently joined federation execution.
		/// </summary>
		/// <param name="resignAction">the action to take upon resigning
		/// </param>
		/// <exception cref="OwnershipAcquisitionPending"> if an ownership acquisition operation is
		/// pending
		/// </exception>
		/// <exception cref="FederateOwnsAttributes"> if the federate still owns attributes
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ResignFederationExecution(ResignAction resignAction);
		
		/// <summary> 
		/// Enables callbacks.
		/// </summary>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableCallbacks();
		
		/// <summary> 
		/// Disables callbacks.
		/// </summary>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableCallbacks();
		
		/// <summary> 
		/// Performs a callback, notifying the federate of a pending messages through the
		/// federate ambassador interface.
		/// </summary>
		/// <param name="seconds">the number of seconds to wait before issuing the callback
		/// </param>
		/// <returns> <code>true</code> if more messages are pending, <code>false</code> otherwise
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		bool EvokeCallback(double seconds);
		
		/// <summary> 
		/// Performs multiple callbacks over a specified time period, notifying the federate of
		/// pending messages through the federate ambassador interface.
		/// </summary>
		/// <param name="minimumTime">the number of seconds to wait before issuing the first callback
		/// </param>
		/// <param name="maximumTime">the maximum time to spend issuing callbacks
		/// </param>
		/// <returns> <code>true</code> if more messages are pending, <code>false</code> otherwise
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		bool EvokeMultipleCallbacks(double minimumTime, double maximumTime);
		
		/// <summary> 
		/// Returns the object class handle associated with the specified name.
		/// </summary>
		/// <param name="theName">the name of the object class
		/// </param>
		/// <returns> the object class handle associated with the specified name
		/// </returns>
		/// <exception cref="NameNotFound"> if the name was not found
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectClassHandle GetObjectClassHandle(System.String theName);
		
		/// <summary> 
		/// Returns the name of the specified object class.
		/// </summary>
		/// <param name="theHandle">the handle of the object class
		/// </param>
		/// <returns> the name of the specified object class
		/// </returns>
		/// <exception cref="InvalidObjectClassHandle"> if the object class handle is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetObjectClassName(IObjectClassHandle theHandle);
		
		/// <summary> 
		/// Returns the attribute handle associated with the specified name.
		/// </summary>
		/// <param name="whichClass">the class with which the attribute is associated
		/// </param>
		/// <param name="theName">the name of the attribute
		/// </param>
		/// <returns> the attribute handle associated with the specified name
		/// </returns>
		/// <exception cref="InvalidObjectClassHandle"> if the object class handle is invalid
		/// </exception>
		/// <exception cref="NameNotFound"> if the name was not found
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IAttributeHandle GetAttributeHandle(IObjectClassHandle whichClass, System.String theName);
		
		/// <summary> 
		/// Returns the name of the specified attribute.
		/// </summary>
		/// <param name="whichClass">the class with which the attribute is associated
		/// </param>
		/// <param name="theHandle">the handle of the attribute
		/// </param>
		/// <returns> the name of the specified attribute
		/// </returns>
		/// <exception cref="InvalidObjectClassHandle"> if the object class handle is invalid
		/// </exception>
		/// <exception cref="InvalidAttributeHandle"> if the attribute handle is invalid
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if the attribute is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetAttributeName(IObjectClassHandle whichClass, IAttributeHandle theHandle);
		
		/// <summary> 
		/// Returns the interaction handle associated with the specified name.
		/// </summary>
		/// <param name="theName">the name of the interaction class
		/// </param>
		/// <returns> the interaction class handle associated with the specified name
		/// </returns>
		/// <exception cref="NameNotFound"> if the name was not found
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IInteractionClassHandle GetInteractionClassHandle(System.String theName);
		
		/// <summary> 
		/// Returns the name of the specified interaction class.
		/// </summary>
		/// <param name="theHandle">the handle of the interaction class
		/// </param>
		/// <returns> the name of the specified interaction class
		/// </returns>
		/// <exception cref="InvalidInteractionClassHandle"> if the interaction class handle is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetInteractionClassName(IInteractionClassHandle theHandle);
		
		/// <summary> 
		/// Returns the parameter handle associated with the specified name.
		/// </summary>
		/// <param name="whichClass">the interaction class with which the parameter is associated
		/// </param>
		/// <param name="theName">the name of the parameter
		/// </param>
		/// <returns> the parameter handle associated with the specified name
		/// </returns>
		/// <exception cref="InvalidInteractionClassHandle"> if the interaction class handle is invalid
		/// </exception>
		/// <exception cref="NameNotFound"> if the name was not found
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IParameterHandle GetParameterHandle(IInteractionClassHandle whichClass, System.String theName);
		
		/// <summary> 
		/// Returns the name of the specified parameter.
		/// </summary>
		/// <param name="whichClass">the interaction class with which the parameter is associated
		/// </param>
		/// <param name="theHandle">the handle of the parameter
		/// </param>
		/// <returns> the name of the specified parameter
		/// </returns>
		/// <exception cref="InvalidInteractionClassHandle"> if the interaction class handle is invalid
		/// </exception>
		/// <exception cref="InvalidParameterHandle"> if the parameter handle is invalid
		/// </exception>
		/// <exception cref="InteractionParameterNotDefined"> if the parameter is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetParameterName(IInteractionClassHandle whichClass, IParameterHandle theHandle);
		
		/// <summary> 
		/// Returns the object instance handle associated with the specified name.
		/// </summary>
		/// <param name="theName">the name of the object instance
		/// </param>
		/// <returns> the object instance handle associated with the name
		/// </returns>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectInstanceHandle GetObjectInstanceHandle(System.String theName);
		
		/// <summary> 
		/// Returns the name of the specified object instance.
		/// </summary>
		/// <param name="theHandle">the handle of the object instance
		/// </param>
		/// <returns> the name of the specified object instance
		/// </returns>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetObjectInstanceName(IObjectInstanceHandle theHandle);
		
		/// <summary> 
		/// Returns the dimension handle associated with the specified name.
		/// </summary>
		/// <param name="theName">the name of the dimension handle
		/// </param>
		/// <returns> the dimension handle associated with the specified name
		/// </returns>
		/// <exception cref="NameNotFound"> if the name was not found
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IDimensionHandle GetDimensionHandle(System.String theName);
		
		/// <summary> 
		/// Returns the name associated with the specified dimension handle.
		/// </summary>
		/// <param name="theHandle">the dimension handle
		/// </param>
		/// <returns> the name associated with the specified dimension handle
		/// </returns>
		/// <exception cref="InvalidDimensionHandle"> if the specified dimension handle is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetDimensionName(IDimensionHandle theHandle);
		
		/// <summary> 
		/// Returns the upper bound of the specified dimension.
		/// </summary>
		/// <param name="theHandle">the dimension handle
		/// </param>
		/// <returns> the upper bound of the specified dimension
		/// </returns>
		/// <exception cref="InvalidDimensionHandle"> if the specified dimension handle is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		long GetDimensionUpperBound(IDimensionHandle theHandle);
		
		/// <summary> 
		/// Returns the set of available dimensions for a class attribute.
		/// </summary>
		/// <param name="whichClass">the object class with which the attribute is associated
		/// </param>
		/// <param name="theHandle">the attribute handle
		/// </param>
		/// <returns> the set of available dimensions for the attribute
		/// </returns>
		/// <exception cref="InvalidObjectClassHandle"> if the object class handle is invalid
		/// </exception>
		/// <exception cref="InvalidAttributeHandle"> if the attribute handle is invalid
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if the attribute is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IDimensionHandleSet GetAvailableDimensionsForClassAttribute(IObjectClassHandle whichClass, IAttributeHandle theHandle);
		
		/// <summary> 
		/// Returns the class handle corresponding to the specified object.
		/// </summary>
		/// <param name="theObject">the object handle
		/// </param>
		/// <returns> the class handle corresponding to the specified object
		/// </returns>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectClassHandle GetKnownObjectClassHandle(IObjectInstanceHandle theObject);
		
		/// <summary> 
		/// Returns the set of available dimensions for an interaction class.
		/// </summary>
		/// <param name="theHandle">the interaction class handle
		/// </param>
		/// <returns> the set of available dimensions for the interaction class
		/// </returns>
		/// <exception cref="InvalidInteractionClassHandle"> if the interaction class handle is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IDimensionHandleSet GetAvailableDimensionsForInteractionClass(IInteractionClassHandle theHandle);
		
		/// <summary> 
		/// Returns the transportation type corresponding to the specified name.
		/// </summary>
		/// <param name="theName">the transportation type name
		/// </param>
		/// <returns> the transportation type corresponding to the specified name
		/// </returns>
		/// <exception cref="InvalidTransportationName"> if the transportation name is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		TransportationType GetTransportationType(System.String theName);
		
		/// <summary> 
		/// Returns the name corresponding to the specified transportation type.
		/// </summary>
		/// <param name="theType">the transportation type
		/// </param>
		/// <returns> the name corresponding to the specified transportation type
		/// </returns>
		/// <exception cref="InvalidTransportationType"> if the transportation type is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetTransportationName(TransportationType theType);
		
		/// <summary> 
		/// Returns the order type corresponding to the specified name.
		/// </summary>
		/// <param name="theName">the order type name
		/// </param>
		/// <returns> the order type corresponding to the specified name
		/// </returns>
		/// <exception cref="InvalidOrderName"> if the order name is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		OrderType GetOrderType(System.String theName);
		
		/// <summary> 
		/// Returns the name corresponding to the specified order type.
		/// </summary>
		/// <param name="theType">the order type
		/// </param>
		/// <returns> the name corresponding to the specified order type
		/// </returns>
		/// <exception cref="InvalidOrderType"> if the order type is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		System.String GetOrderName(OrderType theType);
		
		/// <summary> 
		/// Enables the object class relevance advisory switch.
		/// </summary>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="ObjectClassRelevanceAdvisorySwitchIsOn"> if the switch is already on
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableObjectClassRelevanceAdvisorySwitch();
		
		/// <summary> 
		/// Disables the object class relevance advisory switch.
		/// </summary>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="ObjectClassRelevanceAdvisorySwitchIsOff">  if the switch is already off
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableObjectClassRelevanceAdvisorySwitch();
		
		/// <summary> 
		/// Enables the attribute relevance advisory switch.
		/// </summary>
		/// <exception cref="AttributeRelevanceAdvisorySwitchIsOn">  if the switch is already on
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableAttributeRelevanceAdvisorySwitch();
		
		/// <summary> 
		/// Disables the attribute relevance advisory switch.
		/// </summary>
		/// <exception cref="AttributeRelevanceAdvisorySwitchIsOff">  if the switch is already off
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableAttributeRelevanceAdvisorySwitch();
		
		/// <summary> 
		/// Enables the attribute scope advisory switch.
		/// </summary>
		/// <exception cref="AttributeScopeAdvisorySwitchIsOn">  if the switch is already on
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableAttributeScopeAdvisorySwitch();
		
		/// <summary> 
		/// Disables the attribute scope advisory switch.
		/// </summary>
		/// <exception cref="AttributeScopeAdvisorySwitchIsOff">  if the switch is already off
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableAttributeScopeAdvisorySwitch();
		
		/// <summary> 
		/// Enables the interaction relevance advisory switch.
		/// </summary>
		/// <exception cref="InteractionRelevanceAdvisorySwitchIsOn"> if the switch is already on
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableInteractionRelevanceAdvisorySwitch();
		
		/// <summary> 
		/// Disables the interaction relevance advisory switch.
		/// </summary>
		/// <exception cref="InteractionRelevanceAdvisorySwitchIsOff"> if the switch is already off
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableInteractionRelevanceAdvisorySwitch();
		
		/// <summary> 
		/// Returns the dimension handle set corresponding to the specified region.
		/// </summary>
		/// <param name="region">the region handle
		/// </param>
		/// <returns> the dimension handle set corresponding to the specified region
		/// </returns>
		/// <exception cref="InvalidRegion"> if the specified region is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IDimensionHandleSet GetDimensionHandleSet(IRegionHandle region);
		
		/// <summary> 
		/// Returns the range bounds on the specified region and dimension.
		/// </summary>
		/// <param name="region">the region handle
		/// </param>
		/// <param name="dimension">the dimension handle
		/// </param>
		/// <returns> the range bounds on the specified region and dimension
		/// </returns>
		/// <exception cref="InvalidRegion">  if the specified region is invalid
		/// </exception>
		/// <exception cref="RegionDoesNotContainSpecifiedDimension">  if the region does not contain the
		/// dimension
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		RangeBounds GetRangeBounds(IRegionHandle region, IDimensionHandle dimension);
		
		/// <summary> 
		/// Sets the range bounds on the specified region and dimension.
		/// </summary>
		/// <param name="region">the region handle
		/// </param>
		/// <param name="dimension">the dimension handle
		/// </param>
		/// <param name="bounds">the new set of range bounds
		/// </param>
		/// <exception cref="InvalidRegion"> if the specified region is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if the specified region was not created by
		/// this federate
		/// </exception>
		/// <exception cref="RegionDoesNotContainSpecifiedDimension"> if the region does not contain the
		/// dimension
		/// </exception>
		/// <exception cref="InvalidRangeBound"> if one of the range bounds is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SetRangeBounds(IRegionHandle region, IDimensionHandle dimension, RangeBounds bounds);
		
		/// <summary> 
		/// Normalizes a federate handle, returning it in its equivalent normalized form.
		/// </summary>
		/// <param name="federateHandle">the federate handle to normalize
		/// </param>
		/// <returns> the federate handle in its equivalent normalized form
		/// </returns>
		/// <exception cref="InvalidFederateHandle"> if the supplied federate handle is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		long NormalizeFederateHandle(IFederateHandle federateHandle);
		
		/// <summary> 
		/// Normalizes a service group, returning it in its equivalent normalized form.
		/// </summary>
		/// <param name="group">the service group to normalize
		/// </param>
		/// <returns> the service group in its equivalent normalized form
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		long NormalizeServiceGroup(ServiceGroup group);
		
		/// <summary> 
		/// Publishes the specified object class attributes.
		/// </summary>
		/// <param name="theClass">the object class associated with the attributes to be
		/// published
		/// </param>
		/// <param name="attributeList">the list of attributes to publish
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the specified attributes is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  PublishObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList);
		
		/// <summary> 
		/// Unpublishes the specified object class.
		/// </summary>
		/// <param name="theClass">the object class to unpublish
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="OwnershipAcquisitionPending"> if an ownership acquisition operation is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnpublishObjectClass(IObjectClassHandle theClass);
		
		/// <summary> 
		/// Unpublishes the specified object class attributes.
		/// </summary>
		/// <param name="theClass">the object class associated with the attributes to be
		/// unpublished
		/// </param>
		/// <param name="attributeList">the list of attributes to unpublish
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the specified attributes is not defined
		/// </exception>
		/// <exception cref="OwnershipAcquisitionPending"> if an ownership acquisition operation is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnpublishObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList);
		
		/// <summary> 
		/// Publishes the specified interaction class.
		/// </summary>
		/// <param name="theInteraction">the interaction class to publish
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the specified interaction class is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  PublishInteractionClass(IInteractionClassHandle theInteraction);
		
		/// <summary> 
		/// Unpublishes the specified interaction class.
		/// </summary>
		/// <param name="theInteraction">the interaction class to unpublish
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the specified interaction class is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnpublishInteractionClass(IInteractionClassHandle theInteraction);
		
		/// <summary> 
		/// Subscribes to a set of object class attributes.
		/// </summary>
		/// <param name="theClass">the object class associated with the attributes to subscribe to
		/// </param>
		/// <param name="attributeList">the set of attributes to subscribe to
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the specified attributes is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList);
		
		/// <summary> 
		/// Passively subscribes to a set of object class attributes.
		/// </summary>
		/// <param name="theClass">the object class associated with the attributes to subscribe to
		/// </param>
		/// <param name="attributeList">the set of attributes to subscribe to
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the specified attributes is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeObjectClassAttributesPassively(IObjectClassHandle theClass, IAttributeHandleSet attributeList);
		
		/// <summary> 
		/// Unsubscribes from an object class.
		/// </summary>
		/// <param name="theClass">the object class to unsubscribe from
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnsubscribeObjectClass(IObjectClassHandle theClass);
		
		/// <summary> 
		/// Unsubscribes from a set of object class attributes.
		/// </summary>
		/// <param name="theClass">the object class associated with the attributes to unsubscribe from
		/// </param>
		/// <param name="attributeList">the set of attributes to unsubscribe from
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the specified attributes is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnsubscribeObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList);
		
		/// <summary> 
		/// Subscribes to the specified interaction class.
		/// </summary>
		/// <param name="theClass">the interaction class to subscribe to
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is not defined
		/// </exception>
		/// <exception cref="FederateServiceInvocationsAreBeingReportedViaMOM">  if federate service
		/// invocations are being reported via the management object model
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeInteractionClass(IInteractionClassHandle theClass);
		
		/// <summary> 
		/// Passively subscribes to the specified interaction class.
		/// </summary>
		/// <param name="theClass">the interaction class to subscribe to
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is not defined
		/// </exception>
		/// <exception cref="FederateServiceInvocationsAreBeingReportedViaMOM">  if federate service
		/// invocations are being reported via the management object model
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeInteractionClassPassively(IInteractionClassHandle theClass);
		
		/// <summary> 
		/// Unsubscribes from the specified interaction class.
		/// </summary>
		/// <param name="theClass">the interaction class to unsubscribe from
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is not defined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnsubscribeInteractionClass(IInteractionClassHandle theClass);
		
		/// <summary> 
		/// Reserves the specified object instance name.
		/// </summary>
		/// <param name="theObjectName">the object name to reserve
		/// </param>
		/// <exception cref="IllegalName">  if the name is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ReserveObjectInstanceName(System.String theObjectName);
		
		/// <summary> 
		/// Registers a new instance of the specified object class.
		/// </summary>
		/// <param name="theClass">the class of the object to register
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="ObjectClassNotPublished"> if the specified object class is not published
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectInstanceHandle RegisterObjectInstance(IObjectClassHandle theClass);
		
		/// <summary> 
		/// Registers a new instance of the specified object class.
		/// </summary>
		/// <param name="theClass">the class of the object to register
		/// </param>
		/// <param name="theObjectName">the instance name for the new object
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is not defined
		/// </exception>
		/// <exception cref="ObjectClassNotPublished"> if the specified object class is not published
		/// </exception>
		/// <exception cref="ObjectInstanceNameNotReserved"> if the instance name has not been reserved
		/// </exception>
		/// <exception cref="ObjectInstanceNameInUse"> if the instance name is already in use
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectInstanceHandle RegisterObjectInstance(IObjectClassHandle theClass, System.String theObjectName);
		
		/// <summary> 
		/// Updates the attribute values associated with an object.
		/// </summary>
		/// <param name="theObject">the object whose attributes are to be updated
		/// </param>
		/// <param name="theAttributes">the attributes to update and their corresponding values
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the update
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UpdateAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Updates the attribute values associated with an object.
		/// </summary>
		/// <param name="theObject">the object whose attributes are to be updated
		/// </param>
		/// <param name="theAttributes">the attributes to update and their corresponding values
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the update
		/// </param>
		/// <param name="theTime">the logical time associated with the update
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="InvalidLogicalTime"> if the supplied logical time is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		MessageRetractionReturn UpdateAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, ILogicalTime theTime);
		
		/// <summary> 
		/// Sends an interaction.
		/// </summary>
		/// <param name="theInteraction">the class of interaction to send
		/// </param>
		/// <param name="theParameters">the parameters of the interaction
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotPublished"> if the interaction class is not published
		/// </exception>
		/// <exception cref="InteractionClassNotDefined"> if the interaction class is undefined
		/// </exception>
		/// <exception cref="InteractionParameterNotDefined"> if one of the parameters is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SendInteraction(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Sends an interaction.
		/// </summary>
		/// <param name="theInteraction">the class of interaction to send
		/// </param>
		/// <param name="theParameters">the parameters of the interaction
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the interaction
		/// </param>
		/// <param name="theTime">the logical time associated with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotPublished"> if the interaction class is not published
		/// </exception>
		/// <exception cref="InteractionClassNotDefined"> if the interaction class is undefined
		/// </exception>
		/// <exception cref="InteractionParameterNotDefined"> if one of the parameters is undefined
		/// </exception>
		/// <exception cref="InvalidLogicalTime"> if the supplied logical time is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		MessageRetractionReturn SendInteraction(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, ILogicalTime theTime);
		
		/// <summary> 
		/// Deletes the specified object instance.
		/// </summary>
		/// <param name="objectHandle">the object to Delete
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the operation
		/// </param>
		/// <exception cref="DeletePrivilegeNotHeld"> if the Delete privilege is not held
		/// </exception>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DeleteObjectInstance(IObjectInstanceHandle objectHandle, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Deletes the specified object instance.
		/// </summary>
		/// <param name="objectHandle">the object to Delete
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the operation
		/// </param>
		/// <param name="theTime">the logical time associated with the Delete operation
		/// </param>
		/// <exception cref="DeletePrivilegeNotHeld"> if the Delete privilege is not held
		/// </exception>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="InvalidLogicalTime"> if the supplied logical time is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		MessageRetractionReturn DeleteObjectInstance(IObjectInstanceHandle objectHandle, byte[] userSuppliedTag, ILogicalTime theTime);
		
		/// <summary> Deletes the specified object instance locally.
		/// 
		/// </summary>
		/// <param name="objectHandle">the object to Delete
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="FederateOwnsAttributes"> if the federate owns attributes of the object
		/// </exception>
		/// <exception cref="OwnershipAcquisitionPending"> if an ownership acquisition operation is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  localDeleteObjectInstance(IObjectInstanceHandle objectHandle);
		
		/// <summary> 
		/// Changes the transportation type associated with a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the attributes to modify
		/// </param>
		/// <param name="theType">the new transportation type
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ChangeAttributeTransportationType(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, TransportationType theType);
		
		/// <summary> 
		/// Changes the transporation type associated with an interaction class.
		/// </summary>
		/// <param name="theClass">the interaction class to modify
		/// </param>
		/// <param name="theType">the new transportation type
		/// </param>
		/// <exception cref="InteractionClassNotDefined"> if the interaction class is undefined
		/// </exception>
		/// <exception cref="InteractionClassNotPublished"> if the interaction class is unpublished
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ChangeInteractionTransportationType(IInteractionClassHandle theClass, TransportationType theType);
		
		/// <summary> 
		/// Requests an attribute value update.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to be updated
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associated with the request
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RequestAttributeValueUpdate(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Requests an attribute value update.
		/// </summary>
		/// <param name="theClass">the class of objects with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to be updated
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associated with the request
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is undefined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RequestAttributeValueUpdate(IObjectClassHandle theClass, IAttributeHandleSet theAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Unconditionally divests ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to divest ownership of
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnconditionalAttributeOwnershipDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Performs a negotiated divestiture of ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to divest ownership of
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the action
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="AttributeAlreadyBeingDivested">  if one of the attributes is already being divested
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  NegotiatedAttributeOwnershipDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Confirms divestiture of ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to confirm divestiture of ownership of
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the action
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="AttributeDivestitureWasNotRequested"> if attribute divestiture was not requested
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ConfirmDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Acquires ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="desiredAttributes">the set of attributes to acquire
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the action
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="ObjectClassNotPublished"> if the object class is not published
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotPublished"> if one of the attributes is not published
		/// </exception>
		/// <exception cref="FederateOwnsAttributes"> if the federate already owns the attributes
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  AttributeOwnershipAcquisition(IObjectInstanceHandle theObject, IAttributeHandleSet desiredAttributes, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Acquires ownership of a set of attributes if they are available.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="desiredAttributes">the set of attributes to acquire
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="ObjectClassNotPublished"> if the object class is not published
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotPublished"> if one of the attributes is not published
		/// </exception>
		/// <exception cref="FederateOwnsAttributes"> if the federate already owns the attributes
		/// </exception>
		/// <exception cref="AttributeAlreadyBeingAcquired">  if one of the attributes is already being acquired
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  AttributeOwnershipAcquisitionIfAvailable(IObjectInstanceHandle theObject, IAttributeHandleSet desiredAttributes);
		
		/// <summary> 
		/// Divests ownership of a set of attributes if wanted.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to divest ownership of
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IAttributeHandleSet AttributeOwnershipDivestitureIfWanted(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Cancels a negotiated divestiture of ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to divest ownership of
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="AttributeDivestitureWasNotRequested"> if attribute divestiture was not requested
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  CancelNegotiatedAttributeOwnershipDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> 
		/// Cancels the acquisition of ownership of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to divest ownership of
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeAlreadyOwned"> if one of the attributes is already owned
		/// </exception>
		/// <exception cref="AttributeAcquisitionWasNotRequested"> if attribute acquisition was not requested
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  CancelAttributeOwnershipAcquisition(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes);
		
		/// <summary> Requests information concerning the ownership of an attribute.
		/// 
		/// </summary>
		/// <param name="theObject">the object with which the attribute is associated
		/// </param>
		/// <param name="theAttribute">the attribute of interest
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if the attribute is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  QueryAttributeOwnership(IObjectInstanceHandle theObject, IAttributeHandle theAttribute);
		
		/// <summary> Checks whether a specified attribute is owned by the federate.
		/// 
		/// </summary>
		/// <param name="theObject">the object with which the attribute is associated
		/// </param>
		/// <param name="theAttribute">the attribute of interest
		/// </param>
		/// <returns> <code>true</code> if the federate owns the specified attribute,
		/// <code>false</code> otherwise
		/// </returns>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if the attribute is undefined
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		bool IsAttributeOwnedByFederate(IObjectInstanceHandle theObject, IAttributeHandle theAttribute);
		
		/// <summary> 
		/// Enables time regulation.
		/// </summary>
		/// <param name="theLookahead">the amount of lookahead to use
		/// </param>
		/// <exception cref="TimeRegulationAlreadyEnabled">  if time regulation is already enabled
		/// </exception>
		/// <exception cref="InvalidLookahead">  if the specified amount of lookahead is invalid
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeRegulationPending"> if a request for time regulation is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableTimeRegulation(ILogicalTimeInterval theLookahead);
		
		/// <summary> 
		/// Disables time regulation.
		/// </summary>
		/// <exception cref="TimeRegulationIsNotEnabled">  if time regulation is already disabled
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableTimeRegulation();
		
		/// <summary> 
		/// Enables time-constrained mode.
		/// </summary>
		/// <exception cref="TimeConstrainedAlreadyEnabled">  if time-constrained mode is already enabled
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeConstrainedPending"> if a request for time-constrained mode is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableTimeConstrained();
		
		/// <summary>
		///  Disables time-constrained mode.
		/// </summary>
		/// <exception cref="TimeConstrainedIsNotEnabled"> if time-constrained mode is already disabled
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableTimeConstrained();
		
		/// <summary> 
		/// Requests a time advance.
		/// </summary>
		/// <param name="theTime">the new logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="LogicalTimeAlreadyPassed"> if the specified logical time already passed
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeRegulationPending"> if a request for time regulation is pending
		/// </exception>
		/// <exception cref="RequestForTimeConstrainedPending"> if a request for time-constrained mode is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  TimeAdvanceRequest(ILogicalTime theTime);
		
		/// <summary> 
		/// Requests a time advance if available.
		/// </summary>
		/// <param name="theTime">the new logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="LogicalTimeAlreadyPassed"> if the specified logical time already passed
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeRegulationPending"> if a request for time regulation is pending
		/// </exception>
		/// <exception cref="RequestForTimeConstrainedPending"> if a request for time-constrained mode is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  TimeAdvanceRequestAvailable(ILogicalTime theTime);
		
		/// <summary> 
		/// Requests the next message.
		/// </summary>
		/// <param name="theTime">the new logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="LogicalTimeAlreadyPassed"> if the specified logical time already passed
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeRegulationPending"> if a request for time regulation is pending
		/// </exception>
		/// <exception cref="RequestForTimeConstrainedPending"> if a request for time-constrained mode is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  NextMessageRequest(ILogicalTime theTime);
		
		/// <summary> .
		/// Requests the next message if available
		/// </summary>
		/// <param name="theTime">the new logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="LogicalTimeAlreadyPassed"> if the specified logical time already passed
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeRegulationPending"> if a request for time regulation is pending
		/// </exception>
		/// <exception cref="RequestForTimeConstrainedPending"> if a request for time-constrained mode is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  NextMessageRequestAvailable(ILogicalTime theTime);
		
		/// <summary> 
		/// Requests a queue flush.
		/// </summary>
		/// <param name="theTime">the logical time
		/// </param>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="LogicalTimeAlreadyPassed"> if the specified logical time already passed
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="RequestForTimeRegulationPending"> if a request for time regulation is pending
		/// </exception>
		/// <exception cref="RequestForTimeConstrainedPending"> if a request for time-constrained mode is pending
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  FlushQueueRequest(ILogicalTime theTime);
		
		/// <summary> 
		/// Enables asynchronous delivery.
		/// </summary>
		/// <exception cref="AsynchronousDeliveryAlreadyEnabled"> if asynchronous delivery is already enabled
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  EnableAsynchronousDelivery();
		
		/// <summary> 
		/// Disables asynchronous delivery.
		/// </summary>
		/// <exception cref="AsynchronousDeliveryAlreadyDisabled"> if asynchronous delivery is already disabled
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DisableAsynchronousDelivery();
		
		/// <summary> 
		/// Queries the greatest available logical time.
		/// </summary>
		/// <returns> the greatest available logical time
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		TimeQueryReturn QueryGALT();
		
		/// <summary> 
		/// Queries the current logical time.
		/// </summary>
		/// <returns> the current logical time
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		ILogicalTime QueryLogicalTime();
		
		/// <summary> 
		/// Queries the least incoming time stamp.
		/// </summary>
		/// <returns> the least incoming time stamp
		/// </returns>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		TimeQueryReturn QueryLITS();
		
		/// <summary> 
		/// Modifies the lookahead interval.
		/// </summary>
		/// <param name="theLookahead">the new lookahead interval.
		/// </param>
		/// <exception cref="TimeRegulationIsNotEnabled">  if time regulation is not enabled
		/// </exception>
		/// <exception cref="InvalidLookahead">  if the specified lookahead interval is invalid
		/// </exception>
		/// <exception cref="InTimeAdvancingState"> if the federation is in a time-advancing state
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ModifyLookahead(ILogicalTimeInterval theLookahead);
		
		/// <summary> 
		/// Queries the lookahead interval.
		/// </summary>
		/// <returns> the lookahead interval
		/// </returns>
		/// <exception cref="TimeRegulationIsNotEnabled">  if time regulation is not enabled
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		ILogicalTimeInterval QueryLookahead();
		
		/// <summary> 
		/// Retracts a message.
		/// </summary>
		/// <param name="theHandle">the handle of the message to Retract
		/// </param>
		/// <exception cref="InvalidMessageRetractionHandle">  if the message retraction handle is invalid
		/// </exception>
		/// <exception cref="TimeRegulationIsNotEnabled">  if time regulation is not enabled
		/// </exception>
		/// <exception cref="MessageCanNoLongerBeRetracted">  if the message can no longer be retracted
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  Retract(IMessageRetractionHandle theHandle);
		
		/// <summary> 
		/// Changes the order type of a set of attributes.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="theAttributes">the set of attributes to modify
		/// </param>
		/// <param name="theType">the new order type
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the specified object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
		/// </exception>
		/// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ChangeAttributeOrderType(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, OrderType theType);
		
		/// <summary> 
		/// Changes the order type of a class of interactions.
		/// </summary>
		/// <param name="theClass">the class of interactions to modify
		/// </param>
		/// <param name="theType">the new order type
		/// </param>
		/// <exception cref="InteractionClassNotDefined"> if the interaction class is undefined
		/// </exception>
		/// <exception cref="InteractionClassNotPublished"> if the interaction class is unpublished
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  ChangeInteractionOrderType(IInteractionClassHandle theClass, OrderType theType);
		
		/// <summary> Creates a new region with the specified dimensions.
		/// 
		/// </summary>
		/// <param name="dimensions">the set of dimensions for the region
		/// </param>
		/// <returns> a handle to the newly created region
		/// </returns>
		/// <exception cref="InvalidDimensionHandle"> if one of the dimension handles is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IRegionHandle CreateRegion(IDimensionHandleSet dimensions);
		
		/// <summary> 
		/// Commits modifications to a set of regions.
		/// </summary>
		/// <param name="regions">the regions to commit
		/// </param>
		/// <exception cref="InvalidRegion"> if one of the specified regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created by
		/// this federate
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  CommitRegionModifications(IRegionHandleSet regions);
		
		/// <summary> 
		/// Deletes a region.
		/// </summary>
		/// <param name="theRegion">the region to Delete
		/// </param>
		/// <exception cref="InvalidRegion"> if one of the specified regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created by
		/// this federate
		/// </exception>
		/// <exception cref="RegionInUseForUpdateOrSubscription">  if the region is in use for updates or
		/// subscription
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  DeleteRegion(IRegionHandle theRegion);
		
		/// <summary> 
		/// Registers an object instance with associated regions.
		/// </summary>
		/// <param name="theClass">the class of the object to register
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the object class is not defined
		/// </exception>
		/// <exception cref="ObjectClassNotPublished"> if the object class is not published
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="AttributeNotPublished"> if one of the attributes is not published
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectInstanceHandle RegisterObjectInstanceWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions);
		
		/// <summary> Registers an object instance with associated regions.
		/// 
		/// </summary>
		/// <param name="theClass">the class of the object to register
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <param name="theObject">the object name
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the object class is not defined
		/// </exception>
		/// <exception cref="ObjectClassNotPublished"> if the object class is not published
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="AttributeNotPublished"> if one of the attributes is not published
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="ObjectInstanceNameNotReserved"> if the object instance name was not reserved
		/// </exception>
		/// <exception cref="ObjectInstanceNameInUse"> if the object instance name is in use
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		IObjectInstanceHandle RegisterObjectInstanceWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions, System.String theObject);
		
		/// <summary> 
		/// Associates object attributes with regions for updates.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  AssociateRegionsForUpdates(IObjectInstanceHandle theObject, IAttributeSetRegionSetPairList attributesAndRegions);
		
		/// <summary> 
		/// Unassociates object attributes with regions for updates.
		/// </summary>
		/// <param name="theObject">the object with which the attributes are associated
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnassociateRegionsForUpdates(IObjectInstanceHandle theObject, IAttributeSetRegionSetPairList attributesAndRegions);
		
		/// <summary> 
		/// Subscribes to a set of object class attributes with associated regions.
		/// </summary>
		/// <param name="theClass">the object class with which the attributes are associated
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeObjectClassAttributesWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions);
		
		/// <summary> 
		/// Passively subscribes to a set of object class attributes with associated regions.
		/// </summary>
		/// <param name="theClass">the object class with which the attributes are associated
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeObjectClassAttributesPassivelyWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions);
		
		/// <summary> 
		/// Unsubscribes from a set of object class attributes with associated regions.
		/// </summary>
		/// <param name="theClass">the object class with which the attributes are associated
		/// </param>
		/// <param name="attributesAndRegions">the list of attributes and associated regions
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the object class is not defined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the attributes is not defined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnsubscribeObjectClassAttributesWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions);
		
		/// <summary> 
		/// Subscribes to a class of interactions with associated regions.
		/// </summary>
		/// <param name="theClass">the interaction class to subscribe to
		/// </param>
		/// <param name="regions">the regions associated with the interaction class
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is not defined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateServiceInvocationsAreBeingReportedViaMOM">  if federate service
		/// invocations are being reported via the management object model
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeInteractionClassWithRegions(IInteractionClassHandle theClass, IRegionHandleSet regions);
		
		/// <summary> 
		/// Passively subscribes to a class of interactions with associated regions.
		/// </summary>
		/// <param name="theClass">the interaction class to subscribe to
		/// </param>
		/// <param name="regions">the regions associated with the interaction class
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is not defined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateServiceInvocationsAreBeingReportedViaMOM">  if federate service
		/// invocations are being reported via the management object model
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SubscribeInteractionClassPassivelyWithRegions(IInteractionClassHandle theClass, IRegionHandleSet regions);
		
		/// <summary> 
		/// Unsubscribes from a class of interactions with associated regions.
		/// </summary>
		/// <param name="theClass">the interaction class to unsubscribe from
		/// </param>
		/// <param name="regions">the regions associated with the interaction class
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is not defined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  UnsubscribeInteractionClassWithRegions(IInteractionClassHandle theClass, IRegionHandleSet regions);
		
		/// <summary> 
		/// Sends an interaction with associated regions.
		/// </summary>
		/// <param name="theInteraction">the class of interaction to send
		/// </param>
		/// <param name="theParameters">the parameters of the interaction
		/// </param>
		/// <param name="regions">the regions associated with the interaction
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the interaction
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is undefined
		/// </exception>
		/// <exception cref="InteractionClassNotPublished">  if the interaction class is not published
		/// </exception>
		/// <exception cref="InteractionParameterNotDefined"> if one of the interaction parameters was
		/// undefined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions was invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created by
		/// this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SendInteractionWithRegions(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, IRegionHandleSet regions, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Sends an interaction with associated regions.
		/// </summary>
		/// <param name="theInteraction">the class of interaction to send
		/// </param>
		/// <param name="theParameters">the parameters of the interaction
		/// </param>
		/// <param name="regions">the regions associated with the interaction
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the interaction
		/// </param>
		/// <param name="theTime">the logical time of the interaction
		/// </param>
		/// <exception cref="InteractionClassNotDefined">  if the interaction class is undefined
		/// </exception>
		/// <exception cref="InteractionClassNotPublished">  if the interaction class is not published
		/// </exception>
		/// <exception cref="InteractionParameterNotDefined"> if one of the interaction parameters was
		/// undefined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the regions was invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created by
		/// this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="InvalidLogicalTime"> if the specified logical time is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		MessageRetractionReturn SendInteractionWithRegions(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, IRegionHandleSet regions, byte[] userSuppliedTag, ILogicalTime theTime);
		
		/// <summary> 
		/// Requests an attribute value update with associated regions.
		/// </summary>
		/// <param name="theClass">the class with which the attributes are associated
		/// </param>
		/// <param name="attributesAndRegions">the attributes and their associated regions
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the request
		/// </param>
		/// <exception cref="ObjectClassNotDefined"> if the specified object class is undefined
		/// </exception>
		/// <exception cref="AttributeNotDefined"> if one of the specified attributes is undefined
		/// </exception>
		/// <exception cref="InvalidRegion"> if one of the specified regions is invalid
		/// </exception>
		/// <exception cref="RegionNotCreatedByThisFederate"> if one of the regions was not created
		/// by this federate
		/// </exception>
		/// <exception cref="InvalidRegion">Context if the region context is invalid
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RequestAttributeValueUpdateWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Registers a federation synchronization point.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the synchronization point
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the operation
		/// </param>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RegisterFederationSynchronizationPoint(System.String synchronizationPointLabel, byte[] userSuppliedTag);
		
		/// <summary> 
		/// Registers a federation synchronization point.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the synchronization point
		/// </param>
		/// <param name="userSuppliedTag">a user-supplied tag to associate with the operation
		/// </param>
		/// <param name="synchronizationSet">the set of federates to synchronize
		/// </param>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RegisterFederationSynchronizationPoint(System.String synchronizationPointLabel, byte[] userSuppliedTag, IFederateHandleSet synchronizationSet);
		
		/// <summary> 
		/// Notifies the run-time infrastructure that a synchronization point has been
		/// achieved.
		/// </summary>
		/// <param name="synchronizationPointLabel">the label of the achieved synchronization point
		/// </param>
		/// <exception cref="SynchronizationPointLabelNotAnnounced">  if the specified synchronization point
		/// label was not announced
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  SynchronizationPointAchieved(System.String synchronizationPointLabel);
		
		/// <summary> 
		/// Requests that the federation perform a save operation.
		/// </summary>
		/// <param name="label">the label for the save point
		/// </param>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RequestFederationSave(System.String label);
		
		/// <summary> 
		/// Requests that the federation perform a save operation.
		/// </summary>
		/// <param name="label">the label for the save point
		/// </param>
		/// <param name="theTime">the time at which to perform the save operation
		/// </param>
		/// <exception cref="LogicalTimeAlreadyPassed">  if the specified time has already passed
		/// </exception>
		/// <exception cref="InvalidLogicalTime"> if the specified time is invalid
		/// </exception>
		/// <exception cref="FederateUnableToUseTime">  if the federate is unable to use time management
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RequestFederationSave(System.String label, ILogicalTime theTime);
		
		/// <summary> 
		/// Notifies the run-time infrastructure that the federate is beginning a save
		/// operation.
		/// </summary>
        /// <exception cref="SaveNotInitiated">if a federation save operation was not initiated
        /// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  FederateSaveBegun();
		
		/// <summary> 
		/// Notifies the run-time infrastructure that the federate has completed its save
		/// operation.
		/// </summary>
        /// <exception cref="FederateHasNotBegunSave">if the federate never began its save operation
        /// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  FederateSaveComplete();
		
		/// <summary> 
		/// Notifies the run-time infrastructure that the federate's save operation is not
		/// complete.
		/// </summary>
        /// <exception cref="FederateHasNotBegunSave">if the federate never began its save operation
        /// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  FederateSaveNotComplete();
		
		/// <summary> 
		/// Requests information from the run-time infrastructure concerning the federation's
		/// save status.
		/// </summary>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveNotInProgress"> if a save operation is not in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  QueryFederationSaveStatus();
		
		/// <summary> 
		/// Requests a federation restore operation.
		/// </summary>
		/// <param name="label">the label associated with the stored state to restore
		/// </param>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreInProgress"> if a restore operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  RequestFederationRestore(System.String label);
		
		/// <summary> 
		/// Notifies the run-time infrastructure that the federate's restore operation is
		/// complete.
		/// </summary>
        /// <exception cref="RestoreNotRequested">if a restore operation was not requested
        /// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  FederateRestoreComplete();
		
		/// <summary> 
		/// Notifies the run-time infrastructure that the federate's restore operation is
		/// not complete.
		/// </summary>
        /// <exception cref="RestoreNotRequested">if a restore operation was not requested
        /// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  FederateRestoreNotComplete();
		
		/// <summary> 
		/// Requests information from the run-time infrastructure concerning the restore status
		/// of the federation.
		/// </summary>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
		/// </exception>
		/// <exception cref="SaveInProgress"> if a save operation is in progress
		/// </exception>
		/// <exception cref="RestoreNotInProgress">  if a restore operation is not in progress
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  QueryFederationRestoreStatus();
	}
}