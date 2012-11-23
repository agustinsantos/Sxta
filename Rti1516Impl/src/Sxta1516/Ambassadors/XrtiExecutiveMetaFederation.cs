using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Hla.Rti1516.Extensions;

using Sxta.Rti1516.Management;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.LowLevelManagement;
using Sxta.Rti1516.MetaFederation;
using Sxta.Rti1516.XrtiHandles;

namespace Sxta.Rti1516.Ambassadors
{
    /// <summary> 
    /// The principal interface of the run-time infrastructure.
    /// </summary>
    public partial class XrtiExecutiveAmbassador : IRtiAmbassadorExt
    {
        // PATCH AGUSTIN
        public void RegisterAssembly(Assembly asm)
        {
            descriptorManager.RegisterAssembly(asm);
            interactionManager.RegisterAssemblyHelpers(asm);
        }
        // END PATCH

        #region IRTIambassador Members

        /// <summary>
        ///  Creates a new federation execution.
        /// </summary>
        /// <param name="federationExecutionName">
        /// the name of the new federation execution
        /// </param>
        /// <param name="fdd">
        /// the location of the federation description document
        /// </param>
        /// <exception cref="FederationExecutionAlreadyExists"> 
        /// if the execution already exists
        /// </exception>
        /// <exception cref="CouldNotOpenFDD"> 
        /// if the federation description document could not
        /// be opened
        /// </exception>
        /// <exception cref="ErrorReadingFDD"> 
        /// if an error occurred while reading the federation
        /// description document
        /// </exception>
        /// <exception cref="RTIinternalError"> 
        /// if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        public void CreateFederationExecution(string federationExecutionName, Uri fdd)
        {
            if (!initialized)
            {
                InitializeRTI();
            }

            lock (this)
            {
                if (string.IsNullOrEmpty(federationExecutionName))
                {
                    throw new RTIinternalError("Can't create federation: Name was empty or null");
                }
                if (federationsMap.ContainsKey(federationExecutionName))
                {
                    throw new FederationExecutionAlreadyExists(federationExecutionName);
                }

                if (fdd == null)
                    throw new CouldNotOpenFDD("The URI for the federation description documentis null");
                try
                {
                    string uri = System.Net.WebRequest.Create(fdd).GetResponse().ResponseUri.AbsolutePath;

                    interactionManager.RegisterHelperClass(uri); //PREGUNTAR A ANGEL SI PUEDO MOVERLO AL JOIN ??

                    //HLAfederation tmp = new HLAfederation();
                    HLAfederation tmp = HLAfederation.NewHLAfederation();

                    tmp.HLAfederationName = federationExecutionName;
                    tmp.HLAFDDID = uri;

                    federationsMap.Add(federationExecutionName, tmp);

                    Debug("Created execution " + federationExecutionName);
                }
                catch (System.Exception e)
                {
                    throw new RTIinternalError(e.ToString());
                }

            }
        }

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
        public void DestroyFederationExecution(string federationExecutionName)
        {
            if (!initialized)
            {
                InitializeRTI();
            }

            if (federationsMap.ContainsKey(federationExecutionName))
            {
                HLAfederation federation = federationsMap[federationExecutionName];

                //if federates are still participating in the execution
                if (federation.HLAfederatesinFederation.Count > 0)
                {
                    throw new FederatesCurrentlyJoined(federation.HLAfederatesinFederation.Count + " federates are still participating in " + federation.HLAfederationName);
                }
            }
            // if the federation execution does not exits
            else
            {
                throw new FederationExecutionDoesNotExist(federationExecutionName);
            }

            try
            {
                // Creates and sends the destroy federation message
                /*
                HLAdestroyFederationExecutionMessage destroyFederationMsg = new HLAdestroyFederationExecutionMessage();
                destroyFederationMsg.FederationExecutionName = federationExecutionName;
                destroyFederationMsg.FederationExecutionHandle = this.joinedFederationExecutionHandle;
                interactionManager.SendInteraction(destroyFederationMsg);
                */
            }
            catch (RTIexception rtie)
            {
                throw new RTIinternalError(rtie.ToString());
            }
        }

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
        public IFederateHandle JoinFederationExecution(string federateType,
                                                        string federationExecutionName,
                                                        IFederateAmbassador federateReference,
                                                        MobileFederateServices serviceReferences)
        {
            if (!initialized)
            {
                InitializeRTI();
            }

            // TODO : + Checks if a save operation is in progress
            //        + Checks if a restore operation is in progress

            // if the federate is already a member of an execution
            if (!this.federationName.Equals(HLA_META_FEDERATION_EXECUTION))
            {
                throw new FederateAlreadyExecutionMember("joined to " + this.federationName);
            }

            // if the federation execution does not exits
            if (!federationsMap.ContainsKey(federationExecutionName))
            {
                throw new FederationExecutionDoesNotExist(federationExecutionName);
            }

            HLAfederation federation = federationsMap[federationExecutionName];

            if (serviceReferences != null)
            {
                this.logicalTimeFactory = serviceReferences._timeFactory;
                this.logicalTimeIntervalFactory = serviceReferences._intervalFactory;

                // Sets time's factory to the federation 
                federation.LogicalTimeFactory = serviceReferences._timeFactory;
                federation.LogicalTimeIntervalFactory = serviceReferences._intervalFactory;

                /*
                XrtiSerializerManager serializerMngr = this.interactionManager.SerializerManager;

                Type objType = typeof(ILogicalTime);
                long handle = -1000;
                serializerMngr.RegisterSerializer(objType, handle, new ILogicalTimeXrtiSerializer(serializerMngr, logicalTimeFactory));

                objType = typeof(ILogicalTimeInterval);
                handle = -2000;
                serializerMngr.RegisterSerializer(objType, handle, new ILogicalTimeIntervalXrtiSerializer(serializerMngr, logicalTimeIntervalFactory));
                */
            }

            Sxtafederate sxtaFederate;

            try
            {
                lock (this)
                {
                    // Creates the federate;
                    this.federate = Sxtafederate.NewSxtafederate();

                    sxtaFederate = this.federate as Sxtafederate;

                    // Sets the federate's ambassador
                    sxtaFederate.FederateAmbassador = federateReference;

                    // Sets the federation
                    sxtaFederate.Federation = federation;

                    // Updates the federate's properties
                    sxtaFederate.HLAisJoined = true;

                    sxtaFederate.HLAfederationNameJoined = federationExecutionName;

                    federate.HLAfederateType = federateType;

                    String nameHost = Dns.GetHostName();
                    System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(nameHost);
                    sxtaFederate.HLAfederateHost = hostEntry.AddressList[0].ToString();

                    this.federateHandle = new XRTIFederateHandle();
                    federate.HLAfederateHandle = (HLAfederateHandle)this.federateHandle;
                    //federate.HLAfederateState = HLAfederateState.ActiveFederate;

                    // Init time management's properties
                    federate.HLAtimeConstrained = false;
                    federate.HLAtimeRegulating = false;
                    federate.HLAtimeManagerState = HLAtimeState.TimeGranted;

                    // TODO ANGEL: Posiblemente NO sea el lugar adecuado para inicializar
                    federate.HLAlogicalTime = logicalTimeFactory.MakeInitial();
                    federate.HLAGALT = logicalTimeFactory.MakeInitial();
                    federate.HLALITS = logicalTimeFactory.MakeFinal();
                    sxtaFederate.HLApendingTime = logicalTimeFactory.MakeInitial();

                    federate.HLAlookahead = logicalTimeIntervalFactory.MakeZero();
                }
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.ToString());
            }

            // Initializes Federate's State
            Lrc.LrcQueue queue = new Lrc.LrcQueue();
            state = new Lrc.LrcState(queue, sxtaFederate);
            lrc = new Lrc.Lrc(state, queue);

            // Sets federate ambassador
            this.federateAmbassador = federateReference;

            // Adds the new federate to the execution
            federation.HLAfederatesinFederation.Add(federate.HLAfederateHandle);

            this.federationHandle = federationsMap[federationExecutionName].InstanceHandle;
            this.joinedFederationExecutionHandle = ((XRTIObjectInstanceHandle)this.federationHandle).Identifier;
            this.federationName = federationExecutionName;

            // Set-up HLAobjectRoot static properties
            HLAobjectRoot.DefaultFederate = federate;
            HLAobjectRoot.DefaultFederateAmbassador = federateReference;
            HLAobjectRoot.DefaultFederationExecutionHandle = this.joinedFederationExecutionHandle;

            // Adds listeners to process federation's interactions
            FederationLowLevelManagementObjectModelInteractionListener federationLLMObjectModelInteractionListener = new FederationLowLevelManagementObjectModelInteractionListener(this, federationExecutionName);
            dispatcher.AddListener(joinedFederationExecutionHandle, federationLLMObjectModelInteractionListener);

            FederationManagementObjectModelInteractionListener federationMObjectModelInteractionListener = new FederationManagementObjectModelInteractionListener(this, federationExecutionName);
            dispatcher.AddListener(joinedFederationExecutionHandle, federationMObjectModelInteractionListener);

            PublishAndSubscribeFederationObjectClass(federation.HLAFDDID, this.joinedFederationExecutionHandle);

            Debug("Joined execution " + this.federationName + " [" + this.federationHandle + ")]");

            return this.federateHandle;
        }

        private void PublishAndSubscribeFederationObjectClass(string fddFederationFOM, long federationExecutionHandle)
        {

            DescriptorManager federationDescriptorManager = new DescriptorManager(fddFederationFOM);
            foreach (ObjectClassDescriptor ocd in federationDescriptorManager.ObjectClassDescriptors)
            {
                if (ocd.Name != "HLAobjectRoot")
                {
                    ObjectClassDescriptor ocdReal = descriptorManager.GetObjectClassDescriptor(ocd.Name);
                    CreateAndSendSubscribeMessage(federationExecutionHandle, this.federate.HLAfederateHandle, ocdReal);
                }
            }
        }
        
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
        public void ResignFederationExecution(ResignAction resignAction)
        {
            // Removes the federate to the execution
            federationsMap[this.federationName].HLAfederatesinFederation.Remove(Federate.HLAfederateHandle);

            // Resigns to the execution
            ((Sxtafederate)this.federate).HLAisJoined = false;

            // Removes reference to current federate
            HLAobjectRoot.DefaultFederate = null;
            HLAobjectRoot.DefaultFederateAmbassador = metaFederateAmbassador;
            HLAobjectRoot.DefaultFederationExecutionHandle = -1;
            
            this.federate = null;
            this.state = null;
            this.lrc = null;

            if (resignAction == ResignAction.DELETE_OBJECTS)
            {
                throw new Exception("The method or operation is not implemented.");
            }
            else if (resignAction == ResignAction.DELETE_OBJECTS_THEN_DIVEST)
            {
                throw new Exception("The method or operation is not implemented.");
            }
            else if (resignAction == ResignAction.CANCEL_PENDING_OWNERSHIP_ACQUISITIONS)
            {
                throw new Exception("The method or operation is not implemented.");
            }
            else if (resignAction == ResignAction.CANCEL_THEN_DELETE_THEN_DIVEST)
            {
                throw new Exception("The method or operation is not implemented.");
            }
            else if (resignAction == ResignAction.UNCONDITIONALLY_DIVEST_ATTRIBUTES)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion


        #region protected and private fields

        /// <summary>
        /// A hastable of known federations. Keys are strings (federations names).
        /// </summary>
        protected internal IDictionary<string, HLAfederation> federationsMap = new Dictionary<string, HLAfederation>();

        #endregion
    }

}
