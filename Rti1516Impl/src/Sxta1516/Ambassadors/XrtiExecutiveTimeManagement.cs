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
        #region Vars

        public enum Status { OFF, PENDING, ON };
        public enum FederateStatus { TAR, TARA, NMR, NMRA, GRANT };

        private Status bTimeConstrained = Status.OFF;
        private Status bTimeRegulating = Status.OFF;
        private Status bTimeAdvanceRequest = Status.OFF;
        private Status bNextMessageRequest = Status.OFF;
        private Status bTimeAdvanceRequestAvailable = Status.OFF;
        private Status bNextMessageRequestAvailable = Status.OFF;

        #endregion

        #region Checks

        /// <summary> Check to see if there is a time regulation enable pending. If there is, throw an exception </summary>
        private void CheckTimeRegulationPending()
        {
            if (bTimeRegulating == Status.PENDING)
            {
                throw new RequestForTimeRegulationPending("The federate is in pending on calling the enableTimeRegulating service");
            }
        }

        private void CheckIsRegulating()
        {
            if (this.bTimeRegulating == Status.ON)
                throw new TimeRegulationAlreadyEnabled("Time regulation is already enabled");
        }

        /// <summary> Check to see if there is a time constrained enable pending. If there is, throw an exception </summary>
        private void CheckTimeConstrainedPending()
        {
            if (bTimeConstrained == Status.PENDING)
            {
                throw new RequestForTimeConstrainedPending("The federate is in pending on calling the enableTimeConstrained service");
            }
        }

        private void CheckIsConstrained()
        {
            if (this.bTimeConstrained == Status.ON)
                throw new TimeConstrainedAlreadyEnabled("Time constrained is already enabled");
        }

        private void CheckValidLookahead(ILogicalTimeInterval theLookahead)
        {
            if (theLookahead.CompareTo(logicalTimeIntervalFactory.MakeZero()) == -1)
                throw new InvalidLookahead("The lookahead's value is invalid");
        }

        /// <summary> Check to see if we are advancing. If we are, throw an exception. </summary>
        public void CheckAdvancing()
        {
            if (this.federate.HLAtimeManagerState == HLAtimeState.TimeAdvancing)
            {
                throw new InTimeAdvancingState("The federate is in time advancing state");
            }
        }

        private void CheckValidTime(ILogicalTime time)
        {
            if (time.CompareTo(logicalTimeFactory.MakeInitial()) < 0)
            {
                throw new InvalidLogicalTime("Invalid time [" + time + "]");
            }

            if (time.CompareTo(federate.HLAlogicalTime) < 0)
            {
                throw new LogicalTimeAlreadyPassed("Time is less than the calling federate's logical time");
            }
        }

        #endregion

        #region Properties

        private ILogicalTime PendingTime
        {
            get
            {
                return ((Sxtafederate)this.federate).HLApendingTime;
            }
            set
            {
                ((Sxtafederate)this.federate).HLApendingTime = value;
            }
        }

        private Status TimeConstrained
        {
            get
            {
                return bTimeConstrained;
            }
            set
            {
                this.bTimeConstrained = value;
                this.state.TimeConstrained = value;

                if (value == Status.ON)
                {
                    this.federate.HLAtimeConstrained = true;
                }
                else if (value == Status.OFF)
                {
                    this.federate.HLAtimeConstrained = false;
                }
            }
        }

        private Status TimeRegulating
        {
            get
            {
                return bTimeRegulating;
            }
            set
            {
                this.bTimeRegulating = value;
                if (value == Status.ON)
                {
                    this.federate.HLAtimeRegulating = true;
                }
                else if (value == Status.OFF)
                {
                    this.federate.HLAtimeRegulating = false;
                }
            }
        }

        private Status TimeAdvanceRequestProperty
        {
            get { return bTimeAdvanceRequest; }
            set
            {
                bTimeAdvanceRequest = value;

                if (value == Status.PENDING)
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeAdvancing;
                else
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeGranted;
            }
        }

        private Status NextMessageRequestProperty
        {
            get { return bNextMessageRequest; }
            set
            {
                bNextMessageRequest = value;

                if (value == Status.PENDING)
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeAdvancing;
                else
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeGranted;
            }
        }

        private Status TimeAdvanceRequestAvailableProperty
        {
            get { return bTimeAdvanceRequestAvailable; }
            set
            {
                bTimeAdvanceRequestAvailable = value;

                if (value == Status.PENDING)
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeAdvancing;
                else
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeGranted;
            }
        }

        private Status NextMessageRequestAvailableProperty
        {
            get { return bNextMessageRequestAvailable; }
            set
            {
                bNextMessageRequestAvailable = value;

                if (value == Status.PENDING)
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeAdvancing;
                else
                    this.federate.HLAtimeManagerState = HLAtimeState.TimeGranted;
            }
        }

        #endregion

        // ¿Y sí el federado no es regulado? NO se emplea esta función NUNCA en esa situación
        // ¿Y sí alguno de los otros federados no genera mensajes TSO (no es regulador), se le tiene que tener en cuenta?
        //  Se tiene en cuenta igual ya que podría pasar a ser regulador "sin pararse"
        private void ComputeGALT()
        {
            ILogicalTime GALT = logicalTimeFactory.MakeFinal();

            String federationName = ((Sxtafederate)this.federate).HLAfederationNameJoined;
            IList<Sxtafederate> federates = metaFederateAmbassador.GetFederates(federationName);

            ILogicalTime federateStature;
            foreach (Sxtafederate aFederate in federates)
            {
                // It is not the calling federate
                if (aFederate != this.federate)
                {
                    if (CheckTimeManagementProperties(aFederate))
                    {
                        // COMMENT ANGEL: Ojo! Debe estar inicializado el currentTime, lookahead y LITS del federado
                        if (aFederate.HLAtimeManagerState == HLAtimeState.TimeGranted)
                        {
                            // S(i) = T(i) + L(i)
                            federateStature = aFederate.HLAlogicalTime.Add(aFederate.HLAlookahead);
                        }
                        else
                        {
                            // COMMENT ANGEL: La implementación puede que no sea del todo correcta
                            // aFederate.HLAtimeManagerState --> Granted or Advancing
                            // aFederate.HLALITS == LETS

                            // S(i) = min{T(i){PendingTime}, LITS(i)} + L(i)
                            if (aFederate.HLApendingTime.CompareTo(aFederate.HLALITS) < 0)
                            {
                                federateStature = aFederate.HLApendingTime;
                            }
                            else
                            {
                                federateStature = aFederate.HLALITS;
                            }

                            federateStature = federateStature.Add(aFederate.HLAlookahead);
                        }

                        if (federateStature.CompareTo(GALT) <= 0)
                        {
                            GALT = federateStature;
                        }
                    }
                }
            }

            // NOTE: Updates property's value only when it changes
            if (this.federate.HLAGALT == null)
            {
                this.federate.HLAGALT = GALT;
            }
            else
            {
                if (GALT.CompareTo(this.federate.HLAGALT) != 0)
                {
                    this.federate.HLAGALT = GALT;
                }
            }
        }

        private Boolean CheckTimeManagementProperties(Sxtafederate federate)
        {
            return federate.HLAlookahead != null && federate.HLAlogicalTime != null 
                && federate.HLApendingTime != null && federate.HLALITS != null;
        }

        private void ComputeLITS()
        {
            ILogicalTime LITS = lrc.GetLITS();

            if (LITS == null)
            {
                LITS = LogicalTimeFactory.MakeFinal();
            }

            if (this.federate.HLALITS == null)
            {
                this.federate.HLALITS = LITS;
            }
            else
            {
                if (LITS.CompareTo(this.federate.HLALITS) != 0)
                {
                    this.federate.HLALITS = LITS;
                }
            }
        }

        public void EnableTimeRegulation(ILogicalTimeInterval theLookahead)
        {
            lock (this)
            {
                /* 1. Process exceptions */

                VerifyFederateIsExecutionMember();

                CheckIsRegulating();

                // If the calling federate is in pending on calling the enabledTimeRegulation service
                CheckTimeRegulationPending();

                CheckValidLookahead(theLookahead);

                // if the calling federate is in time advancing state
                CheckAdvancing();

                /* 2. Compute logical time */

                ILogicalTime maxTime = federate.HLAlogicalTime.Add(theLookahead);

                String federationName = ((Sxtafederate)this.federate).HLAfederationNameJoined;
                IList<Sxtafederate> constrainedFederates = metaFederateAmbassador.GetConstrainedFederates(federationName);

                foreach (Sxtafederate constrainedFederate in constrainedFederates)
                {
                    // it is not the calling federate
                    if (constrainedFederate != this.federate)
                    {
                        if (constrainedFederate.HLAlogicalTime.CompareTo(maxTime) >= 0)
                        {
                            maxTime = constrainedFederate.HLAlogicalTime;

                            // COMMENT ANGEL: He omitido lo del EPSILON porque no entiendo que función tiene
                            // CONTESTACIÓN: Debe añadirse una cantidad suficientemente peq para que no sea justamente el tiempo lógico del regulado
                            maxTime = maxTime.Add(logicalTimeIntervalFactory.MakeEpsilon());
                        }
                    }
                }

                PendingTime = maxTime.Subtract(theLookahead);

                /* 3. Set lookahead and pending flag */

                federate.HLAlookahead = theLookahead;
                TimeRegulating = Status.PENDING;

                /* 4. Send all RO messages to the calling federate */

                lrc.TickRO();

                /* 5. Judge whether the calling federate is granted to be regulating */

                if (this.federate.HLAtimeConstrained)
                {
                    // The invocation of this service shall be considered an implicit TARA service invocation
                    bTimeAdvanceRequestAvailable = Status.PENDING;

                    ComputeGALT();

                    if (PendingTime.CompareTo(federate.HLAGALT) <= 0)
                    {
                        // All messages with time stamp <= pending time in the calling 
                        // federate's TSO queue are sent to the federate
                        lrc.TickTSO(PendingTime);                      

                        bTimeAdvanceRequestAvailable = Status.ON;
                        TimeRegulating = Status.ON;

                        this.federate.HLAlogicalTime = PendingTime;
                        this.federateAmbassador.TimeRegulationEnabled(federate.HLAlogicalTime);
                    }
                    else
                    {
                        // All messages with time stamp <= GALT in the calling 
                        // federate's TSO queue are sent to the federate
                        lrc.TickTSO(federate.HLAGALT);
                    }
                }
                else
                {
                    bTimeAdvanceRequestAvailable = Status.ON;
                    TimeRegulating = Status.ON;

                    this.federate.HLAlogicalTime = PendingTime;
                    this.federateAmbassador.TimeRegulationEnabled(federate.HLAlogicalTime);
                }
            }
        }

        public void EnableTimeConstrained()
        {
            lock (this)
            {
                /* 1. Process exceptions */

                VerifyFederateIsExecutionMember();

                CheckIsConstrained();

                // if the calling federate is in pending on calling the enabledTimeConstrained service
                CheckTimeConstrainedPending();

                // if the calling federate is in time advancing state
                CheckAdvancing();

                /* 2. Set pending flag */

                TimeConstrained = Status.PENDING;

                /* 3. Compute GALT */

                ComputeGALT();

                /* 4. Judge whether the calling federate is granted to be constrained */

                if (this.federate.HLAtimeRegulating)
                {
                    if (this.federate.HLAlogicalTime.CompareTo(this.federate.HLAGALT) <= 0)
                    {
                        TimeConstrained = Status.ON;

                        this.federateAmbassador.TimeConstrainedEnabled(this.federate.HLAlogicalTime);
                    }
                    else
                    {
                        bTimeAdvanceRequestAvailable = Status.PENDING;
                        PendingTime = this.federate.HLAlogicalTime;
                    }
                }
                else
                {
                    if (!this.federate.HLAGALT.IsFinal())
                    {
                        if (this.federate.HLAlogicalTime.CompareTo(this.federate.HLAGALT) <= 0)
                        {
                            //this.federate.HLAlogicalTime = this.federate.HLAGALT;

                            TimeConstrained = Status.ON;

                            this.federateAmbassador.TimeConstrainedEnabled(this.federate.HLAlogicalTime);
                        }
                    }
                    else
                    {
                        TimeConstrained = Status.ON;

                        this.federateAmbassador.TimeConstrainedEnabled(this.federate.HLAlogicalTime);
                    }
                }
            }
        }

        public void DisableTimeRegulation()
        {
            TimeRegulating = Status.OFF;
        }

        // Mensajes TSO --> Mensajes RO
        // Si algún método PENDING --> GRANT
        public void DisableTimeConstrained()
        {
            TimeConstrained = Status.OFF;
        }

        public void TimeAdvanceRequest(ILogicalTime theTime)
        {
            lock (this)
            {
                /* 1. Process exceptions */

                VerifyFederateIsExecutionMember();

                // if the calling federate is in time advancing state
                CheckAdvancing();

                CheckValidTime(theTime);

                // if the calling federate is in pending on calling the enabledTimeConstrained service
                CheckTimeConstrainedPending();

                // if the calling federate is in pending on calling the enabledTimeRegulation service
                CheckTimeRegulationPending();

                /* 2. Set pending flag and pendingTime */

                TimeAdvanceRequestProperty = Status.PENDING;
                PendingTime = theTime;

                /* 3. Send all RO messages to the calling federate */

                lrc.TickRO();

                /* 4. Judge whether the TAR request is granted */

                if (this.federate.HLAtimeConstrained)
                {
                    // Compute GALT
                    ComputeGALT();

                    if (theTime.CompareTo(federate.HLAGALT) < 0)
                    {
                        GrantFederateAdvancing(theTime);
                    }
                    else
                    {
                        // All messages with time stamp <= GALT are sent to the calling federate
                        lrc.TickTSO(federate.HLAGALT);
                    }
                }
                else
                {
                    GrantFederateAdvancing(theTime);
                }
            }
        }

        public void NextMessageRequest(ILogicalTime theTime)
        {
            lock(this)
            {
                /* 1. Process exceptions */

                VerifyFederateIsExecutionMember();

                // if the calling federate is in time advancing state
                CheckAdvancing();

                CheckValidTime(theTime);

                // if the calling federate is in pending on calling the enabledTimeConstrained service
                CheckTimeConstrainedPending();

                // if the calling federate is in pending on calling the enabledTimeRegulation service
                CheckTimeRegulationPending();

                /* 2. Set pending flag and pendingTime */

                NextMessageRequestProperty = Status.PENDING;
                PendingTime = theTime;

                /* 3. Send all RO messages to the calling federate */

                lrc.TickRO();

                /* 4. Judge whether the NMR request is granted */

                if (this.federate.HLAtimeConstrained)
                {
                    // Compute GALT
                    ComputeGALT();

                    // LETS = min{TSO}. LETS equals to infinity for an empty TSO queue
                    ComputeLITS();

                    if (this.federate.HLALITS.CompareTo(theTime) <= 0
                            && this.federate.HLALITS.CompareTo(federate.HLAGALT) < 0)
                    {
                        GrantFederateAdvancing(this.federate.HLALITS);
                    }
                    else if (theTime.CompareTo(federate.HLAGALT) < 0)
                    {
                        GrantFederateAdvancing(theTime);
                    }
                }
                else
                {
                    GrantFederateAdvancing(theTime);
                }
            }
        }

        public void TimeAdvanceRequestAvailable(ILogicalTime theTime)
        {
            lock (this)
            {
                /* 1. Process exceptions */

                VerifyFederateIsExecutionMember();

                // if the calling federate is in time advancing state
                CheckAdvancing();

                CheckValidTime(theTime);

                // if the calling federate is in pending on calling the enabledTimeConstrained service
                CheckTimeConstrainedPending();

                // if the calling federate is in pending on calling the enabledTimeRegulation service
                CheckTimeRegulationPending();

                /* 2. Set pending flag and pendingTime */

                TimeAdvanceRequestAvailableProperty = Status.PENDING;
                PendingTime = theTime;

                /* 3. Send all RO messages to the calling federate */

                lrc.TickRO();

                /* 4. Judge whether the TARA request is granted */

                if (this.federate.HLAtimeConstrained)
                {
                    // Compute GALT
                    ComputeGALT();

                    if (theTime.CompareTo(federate.HLAGALT) <= 0)
                    {
                        GrantFederateAdvancing(theTime);
                    }
                    else
                    {
                        // All messages with time stamp <= GALT are sent to the calling federate
                        lrc.TickTSO(federate.HLAGALT);
                    }
                }
                else
                {
                    GrantFederateAdvancing(theTime);
                }
            }
        }

        public void NextMessageRequestAvailable(ILogicalTime theTime)
        {
            lock (this)
            {
                /* 1. Process exceptions */

                VerifyFederateIsExecutionMember();

                // if the calling federate is in time advancing state
                CheckAdvancing();

                CheckValidTime(theTime);

                // if the calling federate is in pending on calling the enabledTimeConstrained service
                CheckTimeConstrainedPending();

                // if the calling federate is in pending on calling the enabledTimeRegulation service
                CheckTimeRegulationPending();

                /* 2. Set pending flag and pendingTime */

                NextMessageRequestAvailableProperty = Status.PENDING;
                PendingTime = theTime;

                /* 3. Send all RO messages to the calling federate */

                lrc.TickRO();

                /* 4. Judge whether the NMR request is granted */

                if (this.federate.HLAtimeConstrained)
                {
                    // Compute GALT
                    ComputeGALT();

                    // LETS = min{TSO}. LETS equals to infinity for an empty TSO queue
                    ComputeLITS();

                    if (this.federate.HLALITS.CompareTo(theTime) <= 0
                            && this.federate.HLALITS.CompareTo(federate.HLAGALT) <= 0)
                    {
                        GrantFederateAdvancing(this.federate.HLALITS);
                    }
                    else if (theTime.CompareTo(federate.HLAGALT) <= 0)
                    {
                        GrantFederateAdvancing(theTime);
                    }
                }
                else
                {
                    GrantFederateAdvancing(theTime);
                }
            }
        }

        private void GrantFederateAdvancing(ILogicalTime theTime)
        {
            /* 
             * 1. Look up if the calling federate is in lookahead-pending state, 
             * the actual lookahead should be recomputed as stated in Carothers, Weatherly, Fujimoto, and Wilson (1997)
             */

            /* 2. Send all TSO messages with time stamp <= to the granted time to the calling federate */
            lrc.TickTSO(theTime);

            /* 3. Call the TAG service to grant the federate to advance its logical time */
            if (TimeAdvanceRequestProperty == Status.PENDING)
                TimeAdvanceRequestProperty = Status.ON;

            if (NextMessageRequestProperty == Status.PENDING)
                NextMessageRequestProperty = Status.ON;

            if (TimeAdvanceRequestAvailableProperty == Status.PENDING)
                TimeAdvanceRequestAvailableProperty = Status.ON;

            if (NextMessageRequestAvailableProperty == Status.PENDING)
                TimeAdvanceRequestAvailableProperty = Status.ON;

            federate.HLAlogicalTime = theTime;
            this.federateAmbassador.TimeAdvanceGrant(theTime);
        }

        /* Method that helps to pass a GRANT status */
        public void UpdateFederate()
        {
            // If it is a joined federate
            if (this.federate != null && ((Sxtafederate)this.federate).HLAisJoined)
            {
                // Compute GALT
                ComputeGALT();

                if (this.federate.HLAtimeManagerState == HLAtimeState.TimeAdvancing)
                {
                    if (log.IsDebugEnabled)
                    {
                        log.Debug("Execute UpdateFederate");
                    }

                    if (TimeAdvanceRequestProperty == Status.PENDING)
                    {
                        if (PendingTime.CompareTo(federate.HLAGALT) < 0)
                        {
                            GrantFederateAdvancing(PendingTime);
                        }
                        else
                        {
                            // All messages with time stamp <= GALT are sent to the calling federate
                            lrc.TickTSO(federate.HLAGALT);
                        }
                    }
                    else if (TimeAdvanceRequestAvailableProperty == Status.PENDING)
                    {
                        if (PendingTime.CompareTo(federate.HLAGALT) <= 0)
                        {
                            if (bTimeRegulating == Status.PENDING)
                            {
                                TimeRegulating = Status.ON;
                                federateAmbassador.TimeRegulationEnabled(PendingTime);
                            }

                            if (bTimeConstrained == Status.PENDING)
                            {
                                TimeConstrained = Status.ON;
                                federateAmbassador.TimeConstrainedEnabled(PendingTime);
                            }

                            GrantFederateAdvancing(PendingTime);
                        }
                        else
                        {
                            // All messages with time stamp <= GALT are sent to the calling federate
                            lrc.TickTSO(federate.HLAGALT);
                        }
                    }
                    else if (NextMessageRequestProperty == Status.PENDING)
                    {
                        ComputeLITS();

                        if (this.federate.HLALITS.CompareTo(PendingTime) <= 0
                                && this.federate.HLALITS.CompareTo(federate.HLAGALT) < 0)
                        {
                            GrantFederateAdvancing(this.federate.HLALITS);
                        }
                        else if (PendingTime.CompareTo(federate.HLAGALT) < 0)
                        {
                            GrantFederateAdvancing(PendingTime);
                        }
                    }
                    else if (NextMessageRequestAvailableProperty == Status.PENDING)
                    {
                        ComputeLITS();

                        if (this.federate.HLALITS.CompareTo(PendingTime) <= 0
                                && this.federate.HLALITS.CompareTo(federate.HLAGALT) <= 0)
                        {
                            GrantFederateAdvancing(this.federate.HLALITS);
                        }
                        else if (PendingTime.CompareTo(federate.HLAGALT) <= 0)
                        {
                            GrantFederateAdvancing(PendingTime);
                        }
                    }
                }
            }
        }

        void IRTIambassador.FlushQueueRequest(ILogicalTime theTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        TimeQueryReturn IRTIambassador.QueryGALT()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ILogicalTime QueryLogicalTime()
        {
            return this.federate.HLAlogicalTime;
        }

        TimeQueryReturn IRTIambassador.QueryLITS()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ModifyLookahead(ILogicalTimeInterval theLookahead)
        {
            // TODO ANGEL: Ojo! Si el lookahead es menor no sería correcto
            this.federate.HLAlookahead = theLookahead;
        }

        public ILogicalTimeInterval QueryLookahead()
        {
            return this.federate.HLAlookahead;
        }

        void IRTIambassador.Retract(IMessageRetractionHandle theHandle)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
