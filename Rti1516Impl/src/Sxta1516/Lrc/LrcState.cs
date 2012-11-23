using System;
using System.Collections.Generic;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.Time;
using Sxta.Rti1516.Management;
using Sxta.Rti1516.Ambassadors;

namespace Sxta.Rti1516.Lrc
{
	
	/// <summary> 
    /// Contains all the state information for a particular LRC.
    /// </summary>
	public class LrcState
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

        #region NECESARIO?
        /*
        private System.String federateName;
        private XRTIFederateHandle federateHandle = new XRTIFederateHandle();
        private System.String federationName;
        private XRTIObjectInstanceHandle federationExecutionHandle = new XRTIObjectInstanceHandle(-1);

        public System.String FederateName
        {
            get { return federateName; }
            set { this.federateName = value; }
        }

        public XRTIFederateHandle FederateHandle
        {
            get { return this.federateHandle; }
            set { this.federateHandle = value; }
        }

        public XRTIObjectInstanceHandle FederationExecutionHandle
        {
            get { return this.federationExecutionHandle; }
            set { this.federationExecutionHandle = value; }
        }

        /// <summary> 
        /// The name of the federation execution to which the federate is joined (or
        /// <code>HLAmetaFederationExecution</code> for MetaFederation).
        /// </summary>
        public System.String FederationName
        {
            get { return federationName; }
            set { this.federationName = value; }
        }
        */
        #endregion

        public ILogicalTime TimeRequest
		{
			get	
            { 
                // Federate in time advancing => TimeRequest = min{pendingTime, GALT}
                if (federate.HLAtimeManagerState == HLAtimeState.TimeAdvancing)
                {
                    if (federate.HLAGALT.CompareTo(federate.HLApendingTime) < 0)
                    {
                        return federate.HLAGALT;
                    }
                    else
                    {
                        return federate.HLApendingTime;
                    }
                }
                // Federate granted => TimeRequest = currentTime
                //  NO es correcto: (GALT - epsilon) ya que podría darse el caso de la aparición de un 
                //                  nuevo federado regulador que disminuyera el GALT actual del federado
                else
                {
                    return federate.HLAlogicalTime;
                    //return federate.HLAGALT.Subtract(federate.Federation.LogicalTimeIntervalFactory.MakeEpsilon());
                }
            }						
		}

        private XrtiExecutiveAmbassador.FederateStatus federateStatus;

		public bool Advancing
		{
            get { return federateStatus != XrtiExecutiveAmbassador.FederateStatus.GRANT; }			
		}

        private bool ticking;

		public bool Ticking
		{
			get	{ return this.ticking; }
			set	{ this.ticking = value; }
		}

        // Time related settings //
        private XrtiExecutiveAmbassador.Status bTimeConstrained;

        /// <summary>
        /// Enable/Disable time constrained. Note that this will notify the LrcQueue (as it is
        /// caching this value), so there is no need to notify it directly.
        /// </summary>
        /// <param name="status"></param>
        public XrtiExecutiveAmbassador.Status TimeConstrained
        {
            get
            {
                return this.bTimeConstrained;
            }

            set
            {
                this.bTimeConstrained = value;
                if (value == XrtiExecutiveAmbassador.Status.ON)
                {
                    this.queue.Constrained = true;
                }
                else if (value == XrtiExecutiveAmbassador.Status.OFF)
                {
                    this.queue.Constrained = false;
                }
            }
        }

        public bool IsConstrained()
        {
            return this.bTimeConstrained == XrtiExecutiveAmbassador.Status.ON;
        }

        private XrtiExecutiveAmbassador.Status bTimeRegulating;

        public XrtiExecutiveAmbassador.Status TimeRegulating
        {
            get { return this.bTimeRegulating; }
            set { this.bTimeRegulating = value; }
        }

        public bool IsRegulating()
        {
            return this.bTimeRegulating == XrtiExecutiveAmbassador.Status.ON;
        }

        /// <summary> Whether or not callbacks are enabled.</summary>
        private bool callbacksEnabled;

        /// <summary> Whether or not callbacks are enabled.</summary>
        public bool CallbacksEnabled
        {
            get { return this.callbacksEnabled; }
            set { this.callbacksEnabled = value; }
        }

        protected Sxtafederate federate;
        protected LrcQueue queue;

		//----------------------------------------------------------
		//                      CONSTRUCTORS
		//----------------------------------------------------------
        public LrcState(LrcQueue queue, Sxtafederate federate)
        {
            this.federate = federate;
            this.queue = queue;

            bTimeRegulating = XrtiExecutiveAmbassador.Status.OFF;
            bTimeConstrained = XrtiExecutiveAmbassador.Status.OFF;
            federateStatus = XrtiExecutiveAmbassador.FederateStatus.GRANT;
        }
     
        # region Check's Methods

        ///<summary>
        /// Check to see if we are currently ticking (and thus not able to make an RTI callback). If
		/// we are currently ticking, a {@link JConcurrentAccessAttempted}
		///</summary>
		public void CheckAccess()
		{
			if (ticking)
			{
				throw new RTIexception("ConcurrentAccessAttempted: Currently ticking");
			}
		}
		
		/// <summary> Check to see if we are advancing. If we are, throw an exception. </summary>
		public void CheckAdvancing()
		{
			if (Advancing)
			{
				throw new InTimeAdvancingState("The federate is in time advancing state");
			}
		}
		
		/// <summary> Check to see if there is a time regulation enable pending. If there is, throw an exception </summary>
		public void CheckTimeRegulationPending()
		{
			if (bTimeRegulating == XrtiExecutiveAmbassador.Status.PENDING)
			{
			    throw new RequestForTimeConstrainedPending("The federate is in pending on calling the enableTimeRegulating service");
			}
		}
		
		/// <summary> Check to see if there is a time constrained enable pending. If there is, throw an exception </summary>
		public void CheckTimeConstrained()
		{
			if (bTimeConstrained == XrtiExecutiveAmbassador.Status.PENDING)
			{
				throw new RTIexception("EnableTimeConstrainedPending");
			}
		}
		
		///<summary> 
        /// Validate that the given time is valid for the current state (that it is equal to or greater
		/// than the current LBTS for <b>this federate</b>).
		///</summary>
		//public void CheckValidTime(LongValuedLogicalTime time)
		//{
		//	// check that the time is greater than or equal to the current LBTS of this federate
		//	if (time.CompareTo(this.LBTS) < 0)
		//	{
		//		throw new RTIexception("InvalidFederationTime Time [" + time + "] has already passed (lbts:" + this.LBTS + ")");
		//	}
        //}

        # endregion

        #region Management Save&Restore

        /// <summary> 
        /// If the federation is undergoing a save operation, the save label (otherwise,
        /// <code>null</code>).
        /// </summary>
        private System.String saveLabel;

        /// <summary> 
        /// If the federation is undergoing a restore operation, the restore label (otherwise,
        /// <code>null</code>).
        /// </summary>
        private System.String restoreLabel;

        public void CheckSaveInProgress()
        {

        }

        public void CheckRestoreInProgress()
        {

        }

        /// <summary> 
        /// Throws an exception if a save operation is in progress.
        /// </summary>
        /// <exception cref="SaveInProgress"> if a save operation is in progress
        /// </exception>
        protected virtual void VerifyNoSaveInProgress()
        {
            if (saveLabel != null)
            {
                throw new SaveInProgress("under label " + saveLabel);
            }
        }

        /// <summary> 
        /// Throws an exception if a restore operation is in progress.
        /// </summary>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress
        /// </exception>
        protected virtual void VerifyNoRestoreInProgress()
        {
            if (restoreLabel != null)
            {
                throw new RestoreInProgress("under label " + restoreLabel);
            }
        }

        #endregion

        #region Management SyncPoints

        // Sync point settings //
        private Dictionary<string, bool> syncPoints = new Dictionary<string, bool>();

        ///<summary> 
        /// Register the given sync point with the LRC. A point should only be registered when the RTI
        /// announces it to us (not on the actual register request). This should only be called by
        /// handlers in the callback sink. <b>NOTE:</b> This will overwrite any previous sync point that
        /// existed in the LRC with the same name (thus, re-announcement of a sync-point should work)
        ///</summary>
        public void RegisterSyncPoint(System.String label)
        {
            this.syncPoints[label] = false;
        }

        ///<summary>
        /// Mark the given sync point as achieved by this federate. When the FEDERATION has achieved the
        /// point, it will be removed (not marked as achieved). This should only be called by handlers
        /// in the request sink.
        ///</summary>
        public void AchieveSyncPoint(System.String label)
        {
            // has the point been announced
            if (this.syncPoints.ContainsKey(label) == false)
            {
                throw new RTIexception("SynchronizationLabelNotAnnounced " + label);
            }

            // mark it as achieved
            this.syncPoints[label] = true;
        }

        ///<summary> 
        /// Remove the given sync point from the store. This should only occur when the federation
        /// becomes synchronized (and thus, only be called from handlers in the callback sink) 
        ///</summary>
        public void FederationSynchronized(System.String label)
        {
            this.syncPoints.Remove(label);
        }

        /// <summary> Checks to see if the given synchronization point label has been announced </summary>
        public void CheckSyncAnnounced(System.String label)
        {
            if (this.syncPoints.ContainsKey(label) == false)
            {
                throw new RTIexception("SynchronizationLabelNotAnnounced " + label);
            }
        }

        # endregion

        # region Management Properties 

        private Dictionary<String, Object> properties = new Dictionary<string, object>();

        ///<summary> 
        /// Add a property to the state. This facility is meant to be used by handlers/plugins that
		/// need to a place to store information. If the key already exists, it will <b>overwrite</b>
		/// any parameterValue that existed with the given parameterValue.
		///</summary>
		public void SetProperty(System.String key, System.Object val)
		{
			this.properties[key] = val;
		}
		
		/// <summary> 
        /// Fetch the parameterValue of a previously bound property. If there is no property for that key,
		/// null will be returned.
		/// </summary>
		public System.Object GetProperty(System.String key)
		{
			return this.properties[key];
		}
		
		///<summary> 
        /// This is the same as {@link #getProperty(String)} except that you can specify the type which
		/// the contained parameterValue should be. If there is no parameterValue for that key or the type of the parameterValue
		/// does not match up with the given parameterValue, null is returned. Otherwise, the parameterValue is cast to
		/// the given type and returned.
		///</summary>
		//public < X > X getProperty(String key, Class < X > type)
		
		/// <summary> Return true if there is a contained property for the given key, false otherwise.</summary>
		public bool HasProperty(System.String key)
		{
			return this.properties.ContainsKey(key);
		}

        #endregion

	}
}