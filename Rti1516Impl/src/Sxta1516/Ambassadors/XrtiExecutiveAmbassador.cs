using System;
using System.Collections;
using System.Collections.Generic;

// Import log4net classes.
using log4net;

using Nini.Config;

using Hla.Rti1516;
using Hla.Rti1516.Extensions;

using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Lrc;
using Sxta.Rti1516.Channels;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.LowLevelManagement;
using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.ObjectManagement;
using Sxta.Rti1516.Management;
using Sxta.Rti1516.Time;

namespace Sxta.Rti1516.Ambassadors
{
    /// <summary> 
    /// The principal interface of the run-time infrastructure.
    /// </summary>
    public partial class XrtiExecutiveAmbassador : IRtiAmbassadorExt
    {

        #region IRTIambassador Members

        /// <summary> 
        /// Returns the High Level Architecture specification version to which the
        /// run-time infrastructure conforms.
        /// </summary>
        /// <returns> the High Level Architecture specification version to which the
        /// run-time infrastructure conforms
        /// </returns>
        string IRTIambassador.HLAversion
        {
            get { return HLA_VERSION; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's attribute handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's attribute handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IAttributeHandleFactory IRTIambassador.AttributeHandleFactory
        {
            get { return attributeHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's attribute handle set factory.
        /// </summary>
        /// <returns> the run-time infrastructure's attribute handle set factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        AttributeHandleSetFactory IRTIambassador.AttributeHandleSetFactory
        {
            get { return attributeHandleSetFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's attribute handle parameterValue map factory.
        /// </summary>
        /// <returns> the run-time infrastructure's attribute handle parameterValue map factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IAttributeHandleValueMapFactory IRTIambassador.AttributeHandleValueMapFactory
        {
            get { return attributeHandleValueMapFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's attribute set region set pair list factory.
        /// </summary>
        /// <returns> the run-time infrastructure's attribute set region set pair list factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IAttributeSetRegionSetPairListFactory IRTIambassador.AttributeSetRegionSetPairListFactory
        {
            get { return attributeSetRegionSetPairListFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's dimension handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's dimension handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IDimensionHandleFactory IRTIambassador.DimensionHandleFactory
        {
            get { return dimensionHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's dimension handle set factory.
        /// </summary>
        /// <returns> the run-time infrastructure's dimension handlet set factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IDimensionHandleSetFactory IRTIambassador.DimensionHandleSetFactory
        {
            get { return dimensionHandleSetFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's federate handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's federate handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IFederateHandleFactory IRTIambassador.FederateHandleFactory
        {
            get { return federateHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's federate handle set factory.
        /// </summary>
        /// <returns> the run-time infrastructure's federate handle set factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IFederateHandleSetFactory IRTIambassador.FederateHandleSetFactory
        {
            get { return federateHandleSetFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's interaction class handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's interaction class handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IInteractionClassHandleFactory IRTIambassador.InteractionClassHandleFactory
        {
            get { return interactionClassHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's object class handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's object class handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IObjectClassHandleFactory IRTIambassador.ObjectClassHandleFactory
        {
            get { return objectClassHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's object instance handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's object instance handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IObjectInstanceHandleFactory IRTIambassador.ObjectInstanceHandleFactory
        {
            get { return objectInstanceHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's parameter handle factory.
        /// </summary>
        /// <returns> the run-time infrastructure's parameter handle factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IParameterHandleFactory IRTIambassador.ParameterHandleFactory
        {
            get { return parameterHandleFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's parameter handle parameterValue map factory.
        /// </summary>
        /// <returns> the run-time infrastructure's parameter handle parameterValue map factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IParameterHandleValueMapFactory IRTIambassador.ParameterHandleValueMapFactory
        {
            get { return parameterHandleValueMapFactory; }
        }

        /// <summary> 
        /// Returns the run-time infrastructure's region handle set factory.
        /// </summary>
        /// <returns> the run-time infrastructure's region handle set factory
        /// </returns>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of an execution
        /// </exception>
        IRegionHandleSetFactory IRTIambassador.RegionHandleSetFactory
        {
            get { return regionHandleSetFactory; }
        }

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
        void IRTIambassador.EnableCallbacks()
        {
            VerifyFederateIsExecutionMember();
            state.CheckSaveInProgress();
            state.CheckRestoreInProgress();

            state.CallbacksEnabled = true;
        }

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
        void IRTIambassador.DisableCallbacks()
        {
            VerifyFederateIsExecutionMember();
            state.CheckSaveInProgress();
            state.CheckRestoreInProgress();

            state.CallbacksEnabled = false;
        }

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
        public bool EvokeCallback(double seconds)
        {
            VerifyFederateIsExecutionMember();

            return lrc.TickSingle(seconds * 1000);
        }

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
        public bool EvokeMultipleCallbacks(double minimumTime, double maximumTime)
        {
            VerifyFederateIsExecutionMember();

            return lrc.Tick(minimumTime * 1000, maximumTime * 1000);
        }

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
        public IObjectClassHandle GetObjectClassHandle(string theName)
        {
            VerifyFederateIsExecutionMember();

            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(theName);

            if (ocd == null)
            {
                throw new NameNotFound(theName);
            }
            else
            {
                return ocd.Handle;
            }
        }

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
        public string GetObjectClassName(IObjectClassHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(theHandle);

            if (ocd == null)
            {
                throw new InvalidObjectClassHandle(theHandle.ToString());
            }
            else
            {
                return ocd.Name;
            }
        }

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
        public IAttributeHandle GetAttributeHandle(IObjectClassHandle whichClass, string theName)
        {
            VerifyFederateIsExecutionMember();

            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(whichClass);

            if (ocd == null)
            {
                throw new InvalidObjectClassHandle(whichClass.ToString());
            }
            else
            {
                AttributeDescriptor ad = ocd.GetAttributeDescriptor(theName);

                if (ad == null)
                {
                    throw new NameNotFound(theName);
                }
                else
                {
                    return ad.Handle;
                }
            }
        }

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
        public string GetAttributeName(IObjectClassHandle whichClass, IAttributeHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(whichClass);

            if (ocd == null)
            {
                throw new InvalidObjectClassHandle(whichClass.ToString());
            }
            else
            {
                AttributeDescriptor ad = ocd.GetAttributeDescriptor(theHandle);

                if (ad == null)
                {
                    throw new InvalidAttributeHandle(theHandle.ToString());
                }
                else
                {
                    return ad.Name;
                }
            }
        }

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
        public IInteractionClassHandle GetInteractionClassHandle(string theName)
        {
            VerifyFederateIsExecutionMember();

            InteractionClassDescriptor icd = descriptorManager.GetInteractionClassDescriptor(theName);

            if (icd == null)
            {
                throw new NameNotFound(theName);
            }
            else
            {
                return icd.Handle;
            }

        }

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
        public string GetInteractionClassName(IInteractionClassHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            InteractionClassDescriptor icd = descriptorManager.GetInteractionClassDescriptor(theHandle);

            if (icd == null)
            {
                throw new InvalidInteractionClassHandle(theHandle.ToString());
            }
            else
            {
                return icd.Name;
            }
        }

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
        public IParameterHandle GetParameterHandle(IInteractionClassHandle whichClass, string theName)
        {
            VerifyFederateIsExecutionMember();

            InteractionClassDescriptor icd = descriptorManager.GetInteractionClassDescriptor(whichClass);

            if (icd == null)
            {
                throw new InvalidInteractionClassHandle(whichClass.ToString());
            }
            else
            {
                ParameterDescriptor pd = icd.GetParameterDescriptor(theName);

                if (pd == null)
                {
                    throw new NameNotFound(theName);
                }
                else
                {
                    return pd.Handle;
                }
            }
        }

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
        public string GetParameterName(IInteractionClassHandle whichClass, IParameterHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            InteractionClassDescriptor icd = descriptorManager.GetInteractionClassDescriptor(whichClass);

            if (icd == null)
            {
                throw new InvalidInteractionClassHandle(whichClass.ToString());
            }
            else
            {
                ParameterDescriptor pd = icd.GetParameterDescriptor(theHandle);

                if (pd == null)
                {
                    throw new InvalidParameterHandle(theHandle.ToString());
                }
                else
                {
                    return pd.Name;
                }
            }
        }

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
        public IObjectInstanceHandle GetObjectInstanceHandle(string theName)
        {
            VerifyFederateIsExecutionMember();

            ObjectInstanceDescriptor oid = descriptorManager.GetObjectInstanceDescriptor(theName);

            if (oid == null)
            {
                throw new ObjectInstanceNotKnown(theName);
            }
            else
            {
                return oid.Handle;
            }
        }

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
        public string GetObjectInstanceName(IObjectInstanceHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            ObjectInstanceDescriptor oid = descriptorManager.GetObjectInstanceDescriptor(theHandle);

            if (oid == null)
            {
                throw new ObjectInstanceNotKnown(theHandle.ToString());
            }
            else
            {
                return oid.Name;
            }
        }

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
        IDimensionHandle IRTIambassador.GetDimensionHandle(string theName)
        {
            VerifyFederateIsExecutionMember();

            DimensionDescriptor dd = descriptorManager.GetDimensionDescriptor(theName);

            if (dd == null)
            {
                throw new NameNotFound(theName);
            }
            else
            {
                return dd.Handle;
            }
        }

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
        string IRTIambassador.GetDimensionName(IDimensionHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            DimensionDescriptor dd = descriptorManager.GetDimensionDescriptor(theHandle);

            if (dd == null)
            {
                throw new InvalidDimensionHandle(theHandle.ToString());
            }
            else
            {
                return dd.Name;
            }
        }

        /// <summary>
        ///  Returns the upper bound of the specified dimension.
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
        long IRTIambassador.GetDimensionUpperBound(IDimensionHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            DimensionDescriptor dd = descriptorManager.GetDimensionDescriptor(theHandle);

            if (dd == null)
            {
                throw new InvalidDimensionHandle(theHandle.ToString());
            }
            else
            {
                return dd.UpperBound;
            }
        }

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
        IDimensionHandleSet IRTIambassador.GetAvailableDimensionsForClassAttribute(IObjectClassHandle whichClass, IAttributeHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor(whichClass);

            if (ocd == null)
            {
                throw new InvalidObjectClassHandle(whichClass.ToString());
            }
            else
            {
                AttributeDescriptor ad = ocd.GetAttributeDescriptor(theHandle);

                if (ad == null)
                {
                    throw new InvalidAttributeHandle(theHandle.ToString());
                }
                else
                {
                    return ad.Dimensions;
                }
            }
        }

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
        public IObjectClassHandle GetKnownObjectClassHandle(IObjectInstanceHandle theObject)
        {
            VerifyFederateIsExecutionMember();

            ObjectInstanceDescriptor oid = descriptorManager.GetObjectInstanceDescriptor(theObject);

            if (oid == null)
            {
                throw new ObjectInstanceNotKnown(theObject.ToString());
            }
            else
            {
                return oid.ClassHandle;
            }
        }

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
        IDimensionHandleSet IRTIambassador.GetAvailableDimensionsForInteractionClass(IInteractionClassHandle theHandle)
        {
            VerifyFederateIsExecutionMember();

            InteractionClassDescriptor icd = descriptorManager.GetInteractionClassDescriptor(theHandle);

            if (icd == null)
            {
                throw new InvalidInteractionClassHandle(theHandle.ToString());
            }
            else
            {
                return icd.Dimensions;
            }
        }

        /// <summary> 
        /// Returns the transportation type corresponding to the specified name.
        /// </summary>
        /// <param name="theName">the transportation type name
        /// </param>
        /// <returns> the transportation type corresponding to the specified name
        /// </returns>
        /// <exception cref=""> InvalidTransportationName if the transportation name is invalid
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        public TransportationType GetTransportationType(string theName)
        {
            VerifyFederateIsExecutionMember();

            if (theName.Equals("HLAreliable"))
            {
                return TransportationType.HLA_RELIABLE;
            }
            else if (theName.Equals("HLAbestEffort"))
            {
                return TransportationType.HLA_BEST_EFFORT;
            }
            else
            {
                throw new InvalidTransportationName(theName);
            }
        }

        /// <summary> 
        /// Returns the name corresponding to the specified transportation type.
        /// </summary>
        /// <param name="theType">the transportation type
        /// </param>
        /// <returns> the name corresponding to the specified transportation type
        /// </returns>
        /// <exception cref=""> InvalidTransportationType if the transportation type is invalid
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        public string GetTransportationName(TransportationType theType)
        {
            VerifyFederateIsExecutionMember();

            return theType.ToString();
        }

        /// <summary> 
        /// Returns the order type corresponding to the specified name.
        /// </summary>
        /// <param name="theName">the order type name
        /// </param>
        /// <returns> the order type corresponding to the specified name
        /// </returns>
        /// <exception cref=""> InvalidOrderName if the order name is invalid
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        OrderType IRTIambassador.GetOrderType(string theName)
        {
            VerifyFederateIsExecutionMember();

            if (theName.Equals("Receive"))
            {
                return OrderType.RECEIVE;
            }
            else if (theName.Equals("TimeStamp"))
            {
                return OrderType.TIMESTAMP;
            }
            else
            {
                throw new InvalidOrderName(theName);
            }
        }

        /// <summary> 
        /// Returns the name corresponding to the specified order type.
        /// </summary>
        /// <param name="theType">the order type
        /// </param>
        /// <returns> the name corresponding to the specified order type
        /// </returns>
        /// <exception cref=""> InvalidOrderType if the order type is invalid
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        string IRTIambassador.GetOrderName(OrderType theType)
        {
            VerifyFederateIsExecutionMember();

            return theType.ToString();
        }

        void IRTIambassador.EnableObjectClassRelevanceAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.DisableObjectClassRelevanceAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.EnableAttributeRelevanceAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.DisableAttributeRelevanceAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.EnableAttributeScopeAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.DisableAttributeScopeAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.EnableInteractionRelevanceAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.DisableInteractionRelevanceAdvisorySwitch()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IDimensionHandleSet IRTIambassador.GetDimensionHandleSet(IRegionHandle region)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        RangeBounds IRTIambassador.GetRangeBounds(IRegionHandle region, IDimensionHandle dimension)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SetRangeBounds(IRegionHandle region, IDimensionHandle dimension, RangeBounds bounds)
        {
            throw new Exception("The method or operation is not implemented.");
        }

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
        long IRTIambassador.NormalizeFederateHandle(IFederateHandle federateHandle)
        {
            VerifyFederateIsExecutionMember();
            try
            {
                return ((XRTIFederateHandle)federateHandle).Identifier;
            }
            catch (System.InvalidCastException)
            {
                throw new InvalidFederateHandle("handle must be XRTIFederateHandle");
            }
        }

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
        long IRTIambassador.NormalizeServiceGroup(ServiceGroup group)
        {
            VerifyFederateIsExecutionMember();

            if (group.Equals(ServiceGroup.FEDERATION_MANAGEMENT))
            {
                return 1;
            }
            else if (group.Equals(ServiceGroup.DECLARATION_MANAGEMENT))
            {
                return 2;
            }
            else if (group.Equals(ServiceGroup.OBJECT_MANAGEMENT))
            {
                return 3;
            }
            else if (group.Equals(ServiceGroup.OWNERSHIP_MANAGEMENT))
            {
                return 4;
            }
            else if (group.Equals(ServiceGroup.TIME_MANAGEMENT))
            {
                return 5;
            }
            else if (group.Equals(ServiceGroup.DATA_DISTRIBUTION_MANAGEMENT))
            {
                return 6;
            }
            // group.Equals(ServiceGroup.SUPPORT_SERVICES)
            else
            {
                return 7;
            }
        }

        void IRTIambassador.PublishObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnpublishObjectClass(IObjectClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnpublishObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.PublishInteractionClass(IInteractionClassHandle theInteraction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnpublishInteractionClass(IInteractionClassHandle theInteraction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeObjectClassAttributesPassively(IObjectClassHandle theClass, IAttributeHandleSet attributeList)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnsubscribeObjectClass(IObjectClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnsubscribeObjectClassAttributes(IObjectClassHandle theClass, IAttributeHandleSet attributeList)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeInteractionClass(IInteractionClassHandle theClass)
        {
            //TODO throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeInteractionClassPassively(IInteractionClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnsubscribeInteractionClass(IInteractionClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ReserveObjectInstanceName(string theObjectName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public IObjectInstanceHandle RegisterObjectInstance(IObjectClassHandle theClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public IObjectInstanceHandle RegisterObjectInstance(IObjectClassHandle theClass, string theObjectName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private static long interactionCounter = 0;

        private static long InteractionCounter
        {
            get
            {
                if (interactionCounter == long.MaxValue)
                {
                    interactionCounter = 0;
                }
                else
                {
                    interactionCounter++;
                }

                return interactionCounter;
            }
        }

        /// <summary>
        /// TODO.
        /// Esta funcion no existe en el standard. Unicamente se llama desde el federado. 
        /// Esta funcion no debe ser publica ya que al rti solo debemos llamarlo desde las 
        /// capas altas usando el API definido en IRTIAmbassador
        /// Debemos hacer que el federado llame a RegisterObjectInstance unicamente
        /// como dice el estandar
        /// </summary>
        /// <param name="obj"></param>
        public void RegisterObjectInstance(HLAobjectRoot obj)
        {
            HLAregisterObjectInstanceMessage msg;

            Boolean joined = obj.OwnFederate != null && ((Sxtafederate)obj.OwnFederate).HLAisJoined;
            if (!(joined && obj.OwnFederate.HLAtimeRegulating))
            {
                msg = new HLAregisterObjectInstanceMessage();
            }
            else
            {
                msg = new HLAregisterObjectInstanceWithTimeMessage();

                ILogicalTime logicalTime = obj.OwnFederate.HLAlogicalTime.Add(obj.OwnFederate.HLAlookahead);

                ((HLAregisterObjectInstanceWithTimeMessage)msg).LogicalTime = new byte[logicalTime.EncodedLength()];
                logicalTime.Encode(((HLAregisterObjectInstanceWithTimeMessage)msg).LogicalTime, 0);
            }

            msg.FederationExecutionHandle = obj.OwnFederationExecutionHandle;
            msg.ObjectInstanceHandle = ((XRTIObjectInstanceHandle)obj.InstanceHandle).Identifier;
            msg.ObjectClassHandle = ((XRTIObjectClassHandle)obj.ClassHandle).Identifier;
            msg.ObjectName = obj.ObjectName;

            if (obj.OwnFederationExecutionHandle != HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE)
            {
                msg.FederateHandle = federate.HLAfederateHandle.data;
                msg.InteractionIndex = InteractionCounter;
            }

            interactionManager.SendInteraction(msg);
        }

        public void UpdateAttributeValues(IObjectInstanceHandle instanceHandle, HLAattributeHandleValuePair[] attributeHandleValuePair, byte[] pUserSuppliedTag)
        {
            HLAupdateAttributeValuesReliableMessage msg = new HLAupdateAttributeValuesReliableMessage();
            msg.ObjectInstanceHandle = ((XRTIObjectInstanceHandle)instanceHandle).Identifier;
            msg.AttributeHandleValuePairList = attributeHandleValuePair;

            ObjectInstanceDescriptor oid = descriptorManager.GetObjectInstanceDescriptor(instanceHandle);
            msg.FederationExecutionHandle = oid.FederationExecutionHandle;
            msg.UserSuppliedTag = pUserSuppliedTag;

            interactionManager.SendInteraction(msg);
        }

        public void UpdateAttributeValues(IObjectInstanceHandle instanceHandle, HLAattributeHandleValuePair[] attributeHandleValuePair, byte[] pUserSuppliedTag, ILogicalTime time)
        {
            if (time == null || time.CompareTo(federate.HLAlogicalTime.Add(federate.HLAlookahead)) < 0)
            {
                throw new InvalidLogicalTime("Time invalid");
            }

            HLAupdateAttributeValuesReliableWithTimeMessage msg = new HLAupdateAttributeValuesReliableWithTimeMessage();
            msg.ObjectInstanceHandle = ((XRTIObjectInstanceHandle)instanceHandle).Identifier;
            msg.AttributeHandleValuePairList = attributeHandleValuePair;
     
            msg.LogicalTime = new byte[time.EncodedLength()];
            time.Encode(msg.LogicalTime, 0);

            ObjectInstanceDescriptor oid = descriptorManager.GetObjectInstanceDescriptor(instanceHandle);

            msg.FederationExecutionHandle = oid.FederationExecutionHandle;
            msg.UserSuppliedTag = pUserSuppliedTag;

            if (oid.FederationExecutionHandle != HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE)
            {
                msg.FederateHandle = federate.HLAfederateHandle.data;
                msg.InteractionIndex = InteractionCounter;

                //if (federate != null && federate.HLAtimeConstrained)
                //{
                    interactionManager.ReceiveInteraction(msg);
                //}
            }

            interactionManager.SendInteraction(msg);
        }

        void IRTIambassador.UpdateAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        MessageRetractionReturn IRTIambassador.UpdateAttributeValues(IObjectInstanceHandle theObject, IAttributeHandleValueMap theAttributes, byte[] userSuppliedTag, ILogicalTime theTime)
        {
            /*
            if (time == null || time.CompareTo(federate.HLAlogicalTime.Add(federate.HLAlookahead)) < 0)
            {
                throw new InvalidLogicalTime("Time invalid");
            }
            */
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SendInteraction(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, byte[] userSuppliedTag)
        {
            HLAparameterHandleValuePair[] parameterHandleValuePairs = new HLAparameterHandleValuePair[theParameters.Count];
            try
            {

                int i = 0;
                foreach (System.Collections.Generic.KeyValuePair<IParameterHandle, byte[]> entry in theParameters)
                {
                    parameterHandleValuePairs[i] = new HLAparameterHandleValuePair(((XRTIParameterHandle)entry.Key).Identifier, entry.Value);
                    i++;
                }

                //TODO 
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.ToString());
            }
            throw new Exception("The method or operation is not implemented.");
        }


        MessageRetractionReturn IRTIambassador.SendInteraction(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, byte[] userSuppliedTag, ILogicalTime theTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SendInteraction(BaseInteractionMessage msg)
        {
            interactionManager.SendInteraction(msg);
        }

        void IRTIambassador.DeleteObjectInstance(IObjectInstanceHandle objectHandle, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        MessageRetractionReturn IRTIambassador.DeleteObjectInstance(IObjectInstanceHandle objectHandle, byte[] userSuppliedTag, ILogicalTime theTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.localDeleteObjectInstance(IObjectInstanceHandle objectHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.ChangeAttributeTransportationType(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, TransportationType theType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.ChangeInteractionTransportationType(IInteractionClassHandle theClass, TransportationType theType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RequestAttributeValueUpdate(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RequestAttributeValueUpdate(IObjectClassHandle theClass, IAttributeHandleSet theAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnconditionalAttributeOwnershipDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.NegotiatedAttributeOwnershipDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.ConfirmDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.AttributeOwnershipAcquisition(IObjectInstanceHandle theObject, IAttributeHandleSet desiredAttributes, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.AttributeOwnershipAcquisitionIfAvailable(IObjectInstanceHandle theObject, IAttributeHandleSet desiredAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IAttributeHandleSet IRTIambassador.AttributeOwnershipDivestitureIfWanted(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.CancelNegotiatedAttributeOwnershipDivestiture(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.CancelAttributeOwnershipAcquisition(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.QueryAttributeOwnership(IObjectInstanceHandle theObject, IAttributeHandle theAttribute)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool IRTIambassador.IsAttributeOwnedByFederate(IObjectInstanceHandle theObject, IAttributeHandle theAttribute)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.EnableAsynchronousDelivery()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.DisableAsynchronousDelivery()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.ChangeAttributeOrderType(IObjectInstanceHandle theObject, IAttributeHandleSet theAttributes, OrderType theType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.ChangeInteractionOrderType(IInteractionClassHandle theClass, OrderType theType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IRegionHandle IRTIambassador.CreateRegion(IDimensionHandleSet dimensions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.CommitRegionModifications(IRegionHandleSet regions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.DeleteRegion(IRegionHandle theRegion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IObjectInstanceHandle IRTIambassador.RegisterObjectInstanceWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IObjectInstanceHandle IRTIambassador.RegisterObjectInstanceWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions, string theObject)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.AssociateRegionsForUpdates(IObjectInstanceHandle theObject, IAttributeSetRegionSetPairList attributesAndRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnassociateRegionsForUpdates(IObjectInstanceHandle theObject, IAttributeSetRegionSetPairList attributesAndRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeObjectClassAttributesWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeObjectClassAttributesPassivelyWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnsubscribeObjectClassAttributesWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeInteractionClassWithRegions(IInteractionClassHandle theClass, IRegionHandleSet regions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SubscribeInteractionClassPassivelyWithRegions(IInteractionClassHandle theClass, IRegionHandleSet regions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.UnsubscribeInteractionClassWithRegions(IInteractionClassHandle theClass, IRegionHandleSet regions)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SendInteractionWithRegions(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, IRegionHandleSet regions, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        MessageRetractionReturn IRTIambassador.SendInteractionWithRegions(IInteractionClassHandle theInteraction, IParameterHandleValueMap theParameters, IRegionHandleSet regions, byte[] userSuppliedTag, ILogicalTime theTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RequestAttributeValueUpdateWithRegions(IObjectClassHandle theClass, IAttributeSetRegionSetPairList attributesAndRegions, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RegisterFederationSynchronizationPoint(string synchronizationPointLabel, byte[] userSuppliedTag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RegisterFederationSynchronizationPoint(string synchronizationPointLabel, byte[] userSuppliedTag, IFederateHandleSet synchronizationSet)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.SynchronizationPointAchieved(string synchronizationPointLabel)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RequestFederationSave(string label)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RequestFederationSave(string label, ILogicalTime theTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.FederateSaveBegun()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.FederateSaveComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.FederateSaveNotComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.QueryFederationSaveStatus()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.RequestFederationRestore(string label)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.FederateRestoreComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.FederateRestoreNotComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IRTIambassador.QueryFederationRestoreStatus()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IRtiAmbassadorExt Members

        void IRtiAmbassadorExt.MergeFdd(Uri fdd)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region InitRti region

        private static MetaFederateAmbassador metaFederateAmbassador;

        public IFederateAmbassador MetaFederateAmbassador
        {
            get { return metaFederateAmbassador; }
        }
        // END PATCH

        private ILogicalTimeFactory metafederateLogicalTimeFactory;
        private ILogicalTimeIntervalFactory metafederateLogicalTimeIntervalFactory;

        /*
        public LrcState metaState;
        protected Sxta.Rti1516.Lrc.Lrc metaLrc;
        */

        /// <summary> Constructor.</summary>
        public XrtiExecutiveAmbassador(IConfigSource cfg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Creating XrtiExecutiveAmbassador");

            sourceConfig = cfg;

            debugModeEnabled = true;

            InitializeRTI();

            // Inicialización de la Metafederación
            this.federationName = HLA_META_FEDERATION_EXECUTION;

            metaFederateAmbassador = new MetaFederateAmbassador(this);
            HLAobjectRoot.DefaultFederateAmbassador = metaFederateAmbassador;

            /*
            Sxtafederate metaFederate = Sxtafederate.NewSxtafederate();
            metaFederate.HLAfederateType = "metaFederate";
            metaFederate.HLAfederationNameJoined = this.federationName;
            metaFederate.FederateAmbassador = metaFederateAmbassador;

            metaState = new LrcState(new LrcQueue(), metaFederate);
            metaLrc = new Sxta.Rti1516.Lrc.Lrc(metaState, metaState.CallbackQueue);
           
            HLAobjectRoot.DefaultFederate = metaFederate;
            */

            HLAobjectRoot.DefaultFederate = null;

            HLAobjectRoot.DefaultFederationExecutionHandle = HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE;

            this.metafederateLogicalTimeFactory = new OpaqueLogicalTimeFactory();
            this.metafederateLogicalTimeIntervalFactory = new OpaqueLogicalTimeIntervalFactory();

            XrtiSerializerManager sm = interactionManager.SerializerManager;
            sm.RegisterSerializer(typeof(ILogicalTime), -1000, new ILogicalTimeXrtiSerializer(sm, metafederateLogicalTimeFactory));
            sm.RegisterSerializer(typeof(ILogicalTimeInterval), -2000, new ILogicalTimeIntervalXrtiSerializer(sm, metafederateLogicalTimeIntervalFactory));

            PublishAndSubscribeMetaFederationObjectClass();
        }

        private void PublishAndSubscribeMetaFederationObjectClass()
        {
            // TODO ANGEL: Que federateHandle tiene el metafederado??
            HLAfederateHandle federateHandle = new HLAfederateHandle();
            
            ObjectClassDescriptor ocd = descriptorManager.GetObjectClassDescriptor("HLAfederation");
            IList<String> propertiesName = new List<String>();
            propertiesName.Add("HLAfederationName");
            propertiesName.Add("HLAFDDID");

            CreateAndSendSubscribeMessage_TODO(HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE, federateHandle, propertiesName, ocd);

            propertiesName.Clear();
            propertiesName.Add("HLAfederateHandle");
            propertiesName.Add("HLAfederateHost");
            propertiesName.Add("HLAfederateType");
            propertiesName.Add("HLAfederationNameJoined");
            propertiesName.Add("HLAisJoined");

            propertiesName.Add("HLAtimeConstrained");
            propertiesName.Add("HLAtimeRegulating");
            propertiesName.Add("HLAtimeManagerState");
            propertiesName.Add("HLAlogicalTime");
            propertiesName.Add("HLAlookahead");
            propertiesName.Add("HLApendingTime");
            propertiesName.Add("HLAGALT");
            propertiesName.Add("HLALITS");

            ocd = descriptorManager.GetObjectClassDescriptor("Sxtafederate");

            CreateAndSendSubscribeMessage_TODO(HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE, federateHandle, propertiesName, ocd);

        }

        // TODO ANGEL: Este método está para solo enviar el interés por las propiedades de las que existe serializador a este instante
        //             En un futuro este método habría que borrarlo
        private void CreateAndSendSubscribeMessage_TODO(long federationExecutionHandle, HLAfederateHandle federateHandle, IList<String> propertiesName, ObjectClassDescriptor ocd)
        {
            Sxta.Rti1516.Management.HLAsubscribeObjectClassAttributesMessage msg = new Sxta.Rti1516.Management.HLAsubscribeObjectClassAttributesMessage();
            msg.HLAobjectClass = ocd.Handle;

            msg.HLAfederate = federateHandle;

            msg.FederationExecutionHandle = federationExecutionHandle;
            msg.UserSuppliedTag = new byte[1];

            msg.HLAattributeList = this.attributeHandleSetFactory.Create();

            foreach (IAttributeHandle attribute in ocd.AttributeHandles)
            {
                AttributeDescriptor ad = descriptorManager.GetAttributeDescriptor(attribute);
                if (ad != null)
                {
                    if (propertiesName.Contains(ad.Name))
                    //if (ad.Name == "HLAfederateHandle" || ad.Name == "HLAfederateHost" || ad.Name == "HLAfederateType"
                    //        || ad.Name == "HLAfederationNameJoined" || ad.Name == "HLAisJoined") 
                    {
                        msg.HLAattributeList.Add(attribute);
                    }
                }
            }

            interactionManager.SendInteraction(msg);

            if (log.IsDebugEnabled)
                log.Debug("Sent " + msg);
        }

        private void CreateAndSendSubscribeMessage(long federationExecutionHandle, HLAfederateHandle federateHandle, ObjectClassDescriptor ocd)
        {
            Sxta.Rti1516.Management.HLAsubscribeObjectClassAttributesMessage msg = new Sxta.Rti1516.Management.HLAsubscribeObjectClassAttributesMessage();
            msg.HLAobjectClass = ocd.Handle;

            msg.HLAfederate = federateHandle;

            msg.FederationExecutionHandle = federationExecutionHandle;
            msg.UserSuppliedTag = new byte[1];

            msg.HLAattributeList = this.attributeHandleSetFactory.Create();

            foreach (IAttributeHandle attribute in ocd.AttributeHandles) 
                msg.HLAattributeList.Add(attribute);

            interactionManager.SendInteraction(msg);

            if (log.IsDebugEnabled)
                log.Debug("Sent " + msg);
        }

        /// <summary>
        /// Destructor - stops the thread
        /// </summary>
        ~XrtiExecutiveAmbassador()
        {
            Stop();
        }

        public void Stop()
        {
            if (!channelManager.IsClosed)
            {
                channelManager.Close();
            }
        }

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

        /// <summary> Whether or not the run-time ambassador has been initialized.</summary>
        private bool initialized = false;

        /// <summary> 
        /// Prints the specified message to the error stream if Debug mode is
        /// enabled.
        /// </summary>
        /// <param name="message">the message to print
        /// </param>
        private void Debug(System.String message)
        {
            if (debugModeEnabled && log.IsDebugEnabled)
            {
                log.Debug("XrtiExecutiveAmbassador: " + message);
            }
        }

        // PATCH ANGEL
        public InteractionDispatcher dispatcher;
        // END PATCH

        public void InitRtiLayers()
        {
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.LowLevelManagementObjectModel);
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.MetaFederationObjectModel);
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.ManagementObjectModel);

            // TODO ANGEL. Deberiamos buscar una forma de no tener que actualizar el codigo cada vez
            // que se cree un nuevo fichero. Quizas usando el meta-federado??
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.SxtaObjectModel);

            interactionManager.RegisterHelperClass(Sxta.Rti1516ResourcesNames.LowLevelManagementObjectModel);
            interactionManager.RegisterHelperClass(Sxta.Rti1516ResourcesNames.MetaFederationObjectModel);
            interactionManager.RegisterHelperClass(Sxta.Rti1516ResourcesNames.ManagementObjectModel);
            interactionManager.RegisterHelperClass(Sxta.Rti1516ResourcesNames.SxtaObjectModel);

            objectManager = new ObjectManager(descriptorManager);

            dispatcher = new InteractionDispatcher(interactionManager);

            MetaFederationLowLevelManagementInteractionListener metafederationLLMInteractionListener = new MetaFederationLowLevelManagementInteractionListener(this, "Metafederation");
            dispatcher.AddListener(HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE, metafederationLLMInteractionListener);

            MetaFederationManagementObjectModelInteractionListener metafederationMObjectModelInteractionListener = new MetaFederationManagementObjectModelInteractionListener(this, "Metafederation");
            dispatcher.AddListener(HLAobjectRoot.METAFEDERATION_EXECUTION_HANDLE, metafederationMObjectModelInteractionListener);
        }

        /// <summary> 
        /// Initializes the run-time infrastructure.
        /// </summary>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        private void InitializeRTI()
        {
            try
            {
                CreateChannelManager();
                InitRtiLayers();
                initialized = true;
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.Message);
            }
        }

        /// <summary> Creates and starts the channel manager that read incoming messages from
        /// the channels.
        /// </summary>
        protected internal virtual void CreateChannelManager()
        {
            Debug("XRTIAmbassador in CreateChannelManager");
            // Mirar el código que hay actualmente en el WinMain.OnStartCommunications

            if (!channelManager.IsClosed)
            {
                if (log.IsWarnEnabled)
                    log.Warn("Server is already started.");
                return;
            }
            if (channelManager.IsClosed)
            {
                if (interactionManager == null)
                {
                    descriptorManager = new DescriptorManager();
                    interactionManager = new InteractionManager(descriptorManager, channelManager);
                    //interactionManager.PeerName = peerName;
                    //interactionManager.PeerDescription = peerDescription;
                }
                channelManager.Start();
            }

            bool isListener = bool.Parse(sourceConfig.Configs["Channels"].Get("TcpEnable", "false")) ||
                              bool.Parse(sourceConfig.Configs["Channels"].Get("UdpEnable", "false"));

            if (isListener)
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();
                connectionInfo.Addr = sourceConfig.Configs["Channels"].Get("DefaultAddr");
                connectionInfo.Port = sourceConfig.Configs["Channels"].GetInt("TcpPort");

                channelManager.StartNewListener(connectionInfo);

                connectionInfo.Addr = sourceConfig.Configs["Channels"].Get("DefaultAddr");
                connectionInfo.Port = sourceConfig.Configs["Channels"].GetInt("UdpPort");

                channelManager.StartNewUDPLocalChannel(connectionInfo);
            }

            string result = sourceConfig.Configs["RendezVous"].Get("ReliableAddrs");
            if (!string.IsNullOrEmpty(result))
            {
                foreach (string uri in result.Split('|'))
                {
                    try
                    {
                        channelManager.StartNewConnection(new Uri(uri));
                    }
                    catch (Exception ex)
                    {
                        if (log.IsErrorEnabled)
                            log.Error("Error connecting " + uri + ". Exception: " + ex.Message);
                    }
                }
            }
            result = sourceConfig.Configs["RendezVous"].Get("BestEffortAddrs");
            if (!string.IsNullOrEmpty(result))
            {
                foreach (string uri in result.Split('|'))
                {
                    try
                    {
                        channelManager.StartNewConnection(new Uri(uri));
                    }
                    catch (Exception ex)
                    {
                        if (log.IsErrorEnabled)
                            log.Error("Error connecting " + uri + ". Exception: " + ex.Message);
                    }
                }
            }
        }

        /// <summary> 
        /// Throws an exception if the federate is not a member of an execution.
        /// </summary>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an
        /// execution
        /// </exception>
        protected internal virtual void VerifyFederateIsExecutionMember()
        {
            //state.CheckJoined();
            Boolean joined = federate != null && ((Sxtafederate)federate).HLAisJoined;
            if (!joined)
            {
                throw new FederateNotExecutionMember("Isn't a member of any federation");
            }
        }

        #endregion

        #region protected and private fields

        /// <summary> The High Level Architecture specification to which the XRTI conforms.</summary>
        private const System.String HLA_VERSION = "1516.1";

        /// <summary> An empty user-supplied tag.</summary>
        private static readonly byte[] EMPTY_TAG = new byte[] { };

        /// <summary> The version attribute.</summary>
        private const System.String VERSION = "version";

        /// <summary> The attribute handle factory.</summary>
        private IAttributeHandleFactory attributeHandleFactory = new XRTIAttributeHandleFactory();

        /// <summary> The attribute handle set factory.</summary>
        private AttributeHandleSetFactory attributeHandleSetFactory = new XRTIAttributeHandleSetFactory();

        /// <summary> The attribute handle parameterValue map factory.</summary>
        private IAttributeHandleValueMapFactory attributeHandleValueMapFactory = new XRTIAttributeHandleValueMapFactory();

        /// <summary> The attribute set region set pair list factory.</summary>
        private IAttributeSetRegionSetPairListFactory attributeSetRegionSetPairListFactory = new XRTIAttributeSetRegionSetPairListFactory();

        /// <summary> The dimension handle factory.</summary>
        private IDimensionHandleFactory dimensionHandleFactory = new XRTIDimensionHandleFactory();

        /// <summary> The dimension handle set factory.</summary>
        private IDimensionHandleSetFactory dimensionHandleSetFactory = new XRTIDimensionHandleSetFactory();

        /// <summary> The federate handle factory.</summary>
        private IFederateHandleFactory federateHandleFactory = new XRTIFederateHandleFactory();

        /// <summary> The federate handle set factory.</summary>
        private IFederateHandleSetFactory federateHandleSetFactory = new XRTIFederateHandleSetFactory();

        /// <summary> The interaction class handle factory.</summary>
        private IInteractionClassHandleFactory interactionClassHandleFactory = new XRTIInteractionClassHandleFactory();

        /// <summary> The logical time factory.</summary>
        private ILogicalTimeFactory logicalTimeFactory;

        public ILogicalTimeFactory LogicalTimeFactory
        {
            get { return logicalTimeFactory; }
        }

        /// <summary> The logical time interval factory.</summary>
        private ILogicalTimeIntervalFactory logicalTimeIntervalFactory;

        public ILogicalTimeIntervalFactory LogicalTimeIntervalFactory
        {
            get { return logicalTimeIntervalFactory; }
        }

        /// <summary> The object class handle factory.</summary>
        private IObjectClassHandleFactory objectClassHandleFactory = new XRTIObjectClassHandleFactory();

        /// <summary> The object instance handle factory.</summary>
        private IObjectInstanceHandleFactory objectInstanceHandleFactory = new XRTIObjectInstanceHandleFactory();

        /// <summary> The parameter handle factory.</summary>
        private IParameterHandleFactory parameterHandleFactory = new XRTIParameterHandleFactory();

        /// <summary> The parameter handle parameterValue map factory.</summary>
        private IParameterHandleValueMapFactory parameterHandleValueMapFactory = new XRTIParameterHandleValueMapFactory();

        /// <summary> The region handle set factory.</summary>
        private IRegionHandleSetFactory regionHandleSetFactory = new XRTIRegionHandleSetFactory();

        /// <summary> Whether or not Debug mode is enabled.</summary>
        private bool debugModeEnabled;

        /// <summary> The proxy ambassador.</summary>
        protected internal XrtiFederateAmbassador proxyAmbassador;

        /// <summary> The federate ambassador.</summary>
        private IFederateAmbassador federateAmbassador;

        public IFederateAmbassador FederateAmbassador
        {
            get { return federateAmbassador; }
        }

        protected internal Lrc.LrcState state;

        public Lrc.LrcState State
        {
            get { return state; }
        }

        protected internal Lrc.Lrc lrc;

        private IHLAfederate federate;

        public IHLAfederate Federate
        {
            get { return federate; }
            set { federate = value; }
        }

        /// <summary> 
        /// The name of the federation execution to which the federate is joined (or
        /// <code>null</code> for MetaFederation).
        /// </summary>
        protected System.String joinedFederationExecutionName;

        /// <summary> The federate handle.</summary>
        private XRTIFederateHandle federateHandle;

        /// <summary> The handle of the federation execution to which the federate is joined.</summary>
        protected long joinedFederationExecutionHandle;

        /// <summary> The name of the meta-federation execution.</summary>
        protected internal const System.String HLA_META_FEDERATION_EXECUTION = "HLAmetaFederationExecution";

        //TODO make it protected or private
        public InteractionManager interactionManager;

        //TODO make it protected or private
        public DescriptorManager descriptorManager;

        /// <summary> The channels manager to XRTI executives.</summary>
        protected internal ChannelsManager channelManager = new ChannelsManager();

        //TODO make it protected or private
        public ObjectManager objectManager;

        private IConfigSource sourceConfig;

        private String federationName;

        public String FederationName
        {
            get { return federationName; }
        }

        private IObjectInstanceHandle federationHandle;

        public IObjectInstanceHandle FederationHandle
        {
            get { return federationHandle; }
        }

        #endregion
    }

    public class XrtiExecutiveAmbassadorImpl
    {
        string GetObjectClassName(IObjectClassHandle theHandle)
        {
            return "";
        }
    }
}