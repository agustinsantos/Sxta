namespace Sxta.Rti1516.Management
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    //using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Ambassadors;
    //using Sxta.Rti1516.Interactions;

    ///<summary>
    ///This object class shall contain RTI state variables relating to a joined federate.
    ///The RTI shall publish it and shall register one object instance for each joined
    ///federate in a federation. Dynamic attributes that shall be contained in an
    ///object instance shall be updated periodically, where the period should be
    ///determined by an interaction of the class HLAmanager. HLAfederate.HLAadjust.HLAsetTiming.
    ///If this value is never set or is set to zero, no periodic up-date shall be
    ///performed by the RTI. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Management Object Model </author>
    [HLAObjectClass(Name = "HLAfederate",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                    Semantics = "This object class shall contain RTI state variables relating to a joined federate. The RTI shall publish it and shall register one object instance for each joined federate in a federation. Dynamic attributes that shall be contained in an object instance shall be updated periodically, where the period should be determined by an interaction of the class HLAmanager. HLAfederate.HLAadjust.HLAsetTiming. If this value is never set or is set to zero, no periodic up-date shall be performed by the RTI.")]
    public class HLAfederate : HLAobjectRoot, IHLAfederate
    {

        protected HLAfederate() : base() { }

        #region Constructor
        // Create an instance of HLAfederate
        protected static Type myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        public static HLAfederate NewFederate()
        {
            return (HLAfederate)NewInstance(myCallType);
        }

        #endregion

        ///<summary>
        ///Attribute #HLAfederateHandle. 
        ///</summary>
        private HLAfederateHandle HLAfederateHandle_;

        ///<summary>
        ///Attribute #HLAfederateType. 
        ///</summary>
        private String HLAfederateType_;

        ///<summary>
        ///Attribute #HLAfederateHost. 
        ///</summary>
        private String HLAfederateHost_;

        ///<summary>
        ///Attribute #HLARTIversion. 
        ///</summary>
        private String HLARTIversion_;

        ///<summary>
        ///Attribute #HLAFDDID. 
        ///</summary>
        private String HLAFDDID_;

        ///<summary>
        ///Attribute #HLAtimeConstrained. 
        ///</summary>
        private bool HLAtimeConstrained_;

        ///<summary>
        ///Attribute #HLAtimeRegulating. 
        ///</summary>
        private bool HLAtimeRegulating_;

        ///<summary>
        ///Attribute #HLAasynchronousDelivery. 
        ///</summary>
        private bool HLAasynchronousDelivery_;

        ///<summary>
        ///Attribute #HLAfederateState. 
        ///</summary>
        private HLAfederateState HLAfederateState_;

        ///<summary>
        ///Attribute #HLAtimeManagerState. 
        ///</summary>
        private HLAtimeState HLAtimeManagerState_;

        ///<summary>
        ///Attribute #HLAlogicalTime. 
        ///</summary>
        private ILogicalTime HLAlogicalTime_;

        ///<summary>
        ///Attribute #HLAlookahead. 
        ///</summary>
        private ILogicalTimeInterval HLAlookahead_;

        ///<summary>
        ///Attribute #HLAGALT. 
        ///</summary>
        private ILogicalTime HLAGALT_;

        ///<summary>
        ///Attribute #HLALITS. 
        ///</summary>
        private ILogicalTime HLALITS_;

        ///<summary>
        ///Attribute #HLAROlength. 
        ///</summary>
        private int HLAROlength_;

        ///<summary>
        ///Attribute #HLATSOlength. 
        ///</summary>
        private int HLATSOlength_;

        ///<summary>
        ///Attribute #HLAreflectionsReceived. 
        ///</summary>
        private int HLAreflectionsReceived_;

        ///<summary>
        ///Attribute #HLAupdatesSent. 
        ///</summary>
        private int HLAupdatesSent_;

        ///<summary>
        ///Attribute #HLAinteractionsReceived. 
        ///</summary>
        private int HLAinteractionsReceived_;

        ///<summary>
        ///Attribute #HLAinteractionsSent. 
        ///</summary>
        private int HLAinteractionsSent_;

        ///<summary>
        ///Attribute #HLAobjectsInstancesThatCanBeDeleted. 
        ///</summary>
        private int HLAobjectsInstancesThatCanBeDeleted_;

        ///<summary>
        ///Attribute #HLAobjectInstancesUpdated. 
        ///</summary>
        private int HLAobjectInstancesUpdated_;

        ///<summary>
        ///Attribute #HLAobjectInstancesReflected. 
        ///</summary>
        private int HLAobjectInstancesReflected_;

        ///<summary>
        ///Attribute #HLAobjectInstancesDeleted. 
        ///</summary>
        private int HLAobjectInstancesDeleted_;

        ///<summary>
        ///Attribute #HLAobjectInstancesRemoved. 
        ///</summary>
        private int HLAobjectInstancesRemoved_;

        ///<summary>
        ///Attribute #HLAobjectInstancesRegistered. 
        ///</summary>
        private int HLAobjectInstancesRegistered_;

        ///<summary>
        ///Attribute #HLAobjectInstancesDiscovered. 
        ///</summary>
        private int HLAobjectInstancesDiscovered_;

        ///<summary>
        ///Attribute #HLAtimeGrantedTime. 
        ///</summary>
        private int HLAtimeGrantedTime_;

        ///<summary>
        ///Attribute #HLAtimeAdvancingTime. 
        ///</summary>
        private int HLAtimeAdvancingTime_;

        ///<summary> Returns a string representation of this HLAfederate. </summary>
        ///<returns> a string representation of this HLAfederate</returns>
        public override String ToString()
        {
            return "HLAfederate(" +
                     "HLAfederateHandle: " + HLAfederateHandle_ + ", " +
                     "HLAfederateType: " + HLAfederateType_ + ", " +
                     "HLAfederateHost: " + HLAfederateHost_ + ", " +
                     "HLARTIversion: " + HLARTIversion_ + ", " +
                     "HLAFDDID: " + HLAFDDID_ + ", " +
                     "HLAtimeConstrained: " + HLAtimeConstrained_ + ", " +
                     "HLAtimeRegulating: " + HLAtimeRegulating_ + ", " +
                     "HLAasynchronousDelivery: " + HLAasynchronousDelivery_ + ", " +
                     "HLAfederateState: " + HLAfederateState_ + ", " +
                     "HLAtimeManagerState: " + HLAtimeManagerState_ + ", " +
                     "HLAlogicalTime: " + HLAlogicalTime_ + ", " +
                     "HLAlookahead: " + HLAlookahead_ + ", " +
                     "HLAGALT: " + HLAGALT_ + ", " +
                     "HLALITS: " + HLALITS_ + ", " +
                     "HLAROlength: " + HLAROlength_ + ", " +
                     "HLATSOlength: " + HLATSOlength_ + ", " +
                     "HLAreflectionsReceived: " + HLAreflectionsReceived_ + ", " +
                     "HLAupdatesSent: " + HLAupdatesSent_ + ", " +
                     "HLAinteractionsReceived: " + HLAinteractionsReceived_ + ", " +
                     "HLAinteractionsSent: " + HLAinteractionsSent_ + ", " +
                     "HLAobjectsInstancesThatCanBeDeleted: " + HLAobjectsInstancesThatCanBeDeleted_ + ", " +
                     "HLAobjectInstancesUpdated: " + HLAobjectInstancesUpdated_ + ", " +
                     "HLAobjectInstancesReflected: " + HLAobjectInstancesReflected_ + ", " +
                     "HLAobjectInstancesDeleted: " + HLAobjectInstancesDeleted_ + ", " +
                     "HLAobjectInstancesRemoved: " + HLAobjectInstancesRemoved_ + ", " +
                     "HLAobjectInstancesRegistered: " + HLAobjectInstancesRegistered_ + ", " +
                     "HLAobjectInstancesDiscovered: " + HLAobjectInstancesDiscovered_ + ", " +
                     "HLAtimeGrantedTime: " + HLAtimeGrantedTime_ + ", " +
                     "HLAtimeAdvancingTime: " + HLAtimeAdvancingTime_ +
                   ")";
        }

        ///<summary>Gets/Sets the value of the HLAfederateHandle attribute.</summary>
        [HLAAttribute(Name = "HLAfederateHandle",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAhandle",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual HLAfederateHandle HLAfederateHandle
        {
            set { HLAfederateHandle_ = value; }
            get { return HLAfederateHandle_; }
        }

        ///<summary>Gets/Sets the value of the HLAfederateType attribute.</summary>
        [HLAAttribute(Name = "HLAfederateType",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual String HLAfederateType
        {
            set { HLAfederateType_ = value; }
            get { return HLAfederateType_; }
        }

        ///<summary>Gets/Sets the value of the HLAfederateHost attribute.</summary>
        [HLAAttribute(Name = "HLAfederateHost",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual String HLAfederateHost
        {
            set { HLAfederateHost_ = value; }
            get { return HLAfederateHost_; }
        }

        ///<summary>Gets/Sets the value of the HLARTIversion attribute.</summary>
        [HLAAttribute(Name = "HLARTIversion",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual String HLARTIversion
        {
            set { HLARTIversion_ = value; }
            get { return HLARTIversion_; }
        }

        ///<summary>Gets/Sets the value of the HLAFDDID attribute.</summary>
        [HLAAttribute(Name = "HLAFDDID",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual String HLAFDDID
        {
            set { HLAFDDID_ = value; }
            get { return HLAFDDID_; }
        }

        ///<summary>Gets/Sets the value of the HLAtimeConstrained attribute.</summary>
        [HLAAttribute(Name = "HLAtimeConstrained",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAboolean",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual bool HLAtimeConstrained
        {
            set { HLAtimeConstrained_ = value; }
            get { return HLAtimeConstrained_; }
        }

        ///<summary>Gets/Sets the value of the HLAtimeRegulating attribute.</summary>
        [HLAAttribute(Name = "HLAtimeRegulating",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAboolean",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual bool HLAtimeRegulating
        {
            set { HLAtimeRegulating_ = value; }
            get { return HLAtimeRegulating_; }
        }

        ///<summary>Gets/Sets the value of the HLAasynchronousDelivery attribute.</summary>
        [HLAAttribute(Name = "HLAasynchronousDelivery",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAboolean",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual bool HLAasynchronousDelivery
        {
            set { HLAasynchronousDelivery_ = value; }
            get { return HLAasynchronousDelivery_; }
        }

        ///<summary>Gets/Sets the value of the HLAfederateState attribute.</summary>
        [HLAAttribute(Name = "HLAfederateState",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAfederateState",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual HLAfederateState HLAfederateState
        {
            set { HLAfederateState_ = value; }
            get { return HLAfederateState_; }
        }

        ///<summary>Gets/Sets the value of the HLAtimeManagerState attribute.</summary>
        [HLAAttribute(Name = "HLAtimeManagerState",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAtimeState",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual HLAtimeState HLAtimeManagerState
        {
            set { HLAtimeManagerState_ = value; }
            get { return HLAtimeManagerState_; }
        }

        // TODO ANGEL: Este código no se genera automáticamente. Habría que incluirlo
        private IFederateAmbassador FederateAmbassador_;

        public IFederateAmbassador FederateAmbassador
        {
            get { return FederateAmbassador_; }
            set { FederateAmbassador_ = value; }
        }

        private HLAfederation Federation_;

        public HLAfederation Federation
        {
            get { return Federation_; }
            set { Federation_ = value; }
        }

        protected ILogicalTime ConvertToFederationLogicalTimeRepresentation(ILogicalTime time)
        {
            if (Federation != null)
            {
                ILogicalTimeFactory factory = Federation.LogicalTimeFactory;

                if (factory != null)
                {
                    byte[] timeByteArray = new byte[time.EncodedLength()];
                    time.Encode(timeByteArray, 0);

                    return factory.Decode(timeByteArray, 0);
                }
                else
                {
                    return time;
                }
            }
            else
            {
                return time;
            }
        }

        protected ILogicalTimeInterval ConvertToFederationLogicalTimeIntervalRepresentation(ILogicalTimeInterval timeInterval)
        {
            if (Federation != null)
            {
                ILogicalTimeIntervalFactory factory = Federation.LogicalTimeIntervalFactory;

                if (factory != null)
                {
                    byte[] timeIntervalByteArray = new byte[timeInterval.EncodedLength()];
                    timeInterval.Encode(timeIntervalByteArray, 0);

                    return factory.Decode(timeIntervalByteArray, 0);
                }
                else
                {
                    return timeInterval;
                }
            }
            else
            {
                return timeInterval;
            }
        }
        // END TODO

        ///<summary>Gets/Sets the value of the HLAlogicalTime attribute.</summary>
        [HLAAttribute(Name = "HLAlogicalTime",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAlogicalTime",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual ILogicalTime HLAlogicalTime
        {
            set 
            { 
                HLAlogicalTime_ = ConvertToFederationLogicalTimeRepresentation(value); 
            }
            get 
            {
                if (HLAlogicalTime_ != null)
                {
                    HLAlogicalTime_ = ConvertToFederationLogicalTimeRepresentation(HLAlogicalTime_);
                }
                return HLAlogicalTime_; 
            }
        }

        ///<summary>Gets/Sets the value of the HLAlookahead attribute.</summary>
        [HLAAttribute(Name = "HLAlookahead",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAtimeInterval",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual ILogicalTimeInterval HLAlookahead
        {
            set 
            { 
                HLAlookahead_ = ConvertToFederationLogicalTimeIntervalRepresentation(value); 
            }
            get 
            {
                if (HLAlookahead_ != null)
                {
                    HLAlookahead_ = ConvertToFederationLogicalTimeIntervalRepresentation(HLAlookahead_);
                }
                return HLAlookahead_; 
            }
        }

        ///<summary>Gets/Sets the value of the HLAGALT attribute.</summary>
        [HLAAttribute(Name = "HLAGALT",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAlogicalTime",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual ILogicalTime HLAGALT
        {
            set 
            {
                HLAGALT_ = ConvertToFederationLogicalTimeRepresentation(value);
            }
            get 
            {
                if (HLAGALT_ != null)
                {
                    HLAGALT_ = ConvertToFederationLogicalTimeRepresentation(HLAGALT_);
                }
                return HLAGALT_; 
            }
        }

        ///<summary>Gets/Sets the value of the HLALITS attribute.</summary>
        [HLAAttribute(Name = "HLALITS",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAlogicalTime",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual ILogicalTime HLALITS
        {
            set 
            {
                HLALITS_ = ConvertToFederationLogicalTimeRepresentation(value);
            }
            get 
            {
                if (HLALITS_ != null)
                {
                    HLALITS_ = ConvertToFederationLogicalTimeRepresentation(HLALITS_);
                }
                return HLALITS_; 
            }
        }

        ///<summary>Gets/Sets the value of the HLAROlength attribute.</summary>
        [HLAAttribute(Name = "HLAROlength",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAROlength
        {
            set { HLAROlength_ = value; }
            get { return HLAROlength_; }
        }

        ///<summary>Gets/Sets the value of the HLATSOlength attribute.</summary>
        [HLAAttribute(Name = "HLATSOlength",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLATSOlength
        {
            set { HLATSOlength_ = value; }
            get { return HLATSOlength_; }
        }

        ///<summary>Gets/Sets the value of the HLAreflectionsReceived attribute.</summary>
        [HLAAttribute(Name = "HLAreflectionsReceived",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAreflectionsReceived
        {
            set { HLAreflectionsReceived_ = value; }
            get { return HLAreflectionsReceived_; }
        }

        ///<summary>Gets/Sets the value of the HLAupdatesSent attribute.</summary>
        [HLAAttribute(Name = "HLAupdatesSent",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAupdatesSent
        {
            set { HLAupdatesSent_ = value; }
            get { return HLAupdatesSent_; }
        }

        ///<summary>Gets/Sets the value of the HLAinteractionsReceived attribute.</summary>
        [HLAAttribute(Name = "HLAinteractionsReceived",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAinteractionsReceived
        {
            set { HLAinteractionsReceived_ = value; }
            get { return HLAinteractionsReceived_; }
        }

        ///<summary>Gets/Sets the value of the HLAinteractionsSent attribute.</summary>
        [HLAAttribute(Name = "HLAinteractionsSent",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAinteractionsSent
        {
            set { HLAinteractionsSent_ = value; }
            get { return HLAinteractionsSent_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectsInstancesThatCanBeDeleted attribute.</summary>
        [HLAAttribute(Name = "HLAobjectsInstancesThatCanBeDeleted",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectsInstancesThatCanBeDeleted
        {
            set { HLAobjectsInstancesThatCanBeDeleted_ = value; }
            get { return HLAobjectsInstancesThatCanBeDeleted_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectInstancesUpdated attribute.</summary>
        [HLAAttribute(Name = "HLAobjectInstancesUpdated",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectInstancesUpdated
        {
            set { HLAobjectInstancesUpdated_ = value; }
            get { return HLAobjectInstancesUpdated_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectInstancesReflected attribute.</summary>
        [HLAAttribute(Name = "HLAobjectInstancesReflected",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectInstancesReflected
        {
            set { HLAobjectInstancesReflected_ = value; }
            get { return HLAobjectInstancesReflected_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectInstancesDeleted attribute.</summary>
        [HLAAttribute(Name = "HLAobjectInstancesDeleted",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectInstancesDeleted
        {
            set { HLAobjectInstancesDeleted_ = value; }
            get { return HLAobjectInstancesDeleted_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectInstancesRemoved attribute.</summary>
        [HLAAttribute(Name = "HLAobjectInstancesRemoved",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectInstancesRemoved
        {
            set { HLAobjectInstancesRemoved_ = value; }
            get { return HLAobjectInstancesRemoved_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectInstancesRegistered attribute.</summary>
        [HLAAttribute(Name = "HLAobjectInstancesRegistered",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectInstancesRegistered
        {
            set { HLAobjectInstancesRegistered_ = value; }
            get { return HLAobjectInstancesRegistered_; }
        }

        ///<summary>Gets/Sets the value of the HLAobjectInstancesDiscovered attribute.</summary>
        [HLAAttribute(Name = "HLAobjectInstancesDiscovered",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAcount",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAobjectInstancesDiscovered
        {
            set { HLAobjectInstancesDiscovered_ = value; }
            get { return HLAobjectInstancesDiscovered_; }
        }

        ///<summary>Gets/Sets the value of the HLAtimeGrantedTime attribute.</summary>
        [HLAAttribute(Name = "HLAtimeGrantedTime",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAmsec",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAtimeGrantedTime
        {
            set { HLAtimeGrantedTime_ = value; }
            get { return HLAtimeGrantedTime_; }
        }

        ///<summary>Gets/Sets the value of the HLAtimeAdvancingTime attribute.</summary>
        [HLAAttribute(Name = "HLAtimeAdvancingTime",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAmsec",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual int HLAtimeAdvancingTime
        {
            set { HLAtimeAdvancingTime_ = value; }
            get { return HLAtimeAdvancingTime_; }
        }
    }
}