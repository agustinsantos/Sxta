namespace Sxta.Rti1516.Management
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Base; //TODO

    ///<summary>
    ///This object class shall contain RTI state variables relating to a federation
    ///execution. The RTI shall publish it and shall register one object instance
    ///for the federation execution. It shall not automatically update the values
    ///of the instance attributes; a joined federate shall use a Request Attribute
    ///Value Update service to obtain values for the instance attributes. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Management Object Model </author>
    [HLAObjectClass(Name = "HLAfederation",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                    Semantics = "This object class shall contain RTI state variables relating to a federation execution. The RTI shall publish it and shall register one object instance for the federation execution. It shall not automatically update the values of the instance attributes; a joined federate shall use a Request Attribute Value Update service to obtain values for the instance attributes.")]
    public class HLAfederation :   HLAobjectRoot, IHLAfederation 
    {

        protected HLAfederation() : base() { }

        #region Constructor
        // Create an instance of HLAfederation
        static Type myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        public static HLAfederation NewHLAfederation()
        {
            return (HLAfederation)NewInstance(myCallType);
        }

        #endregion
        ///<summary>
        ///Attribute #HLAfederationName. 
        ///</summary>
        private String HLAfederationName_;

        ///<summary>
        ///Attribute #HLAfederatesinFederation. 
        ///</summary>
        private HLAhandleList HLAfederatesinFederation_ = new HLAhandleList();

        ///<summary>
        ///Attribute #HLARTIversion. 
        ///</summary>
        private String HLARTIversion_;

        ///<summary>
        ///Attribute #HLAFDDID. 
        ///</summary>
        private String HLAFDDID_;

        ///<summary>
        ///Attribute #HLAlastSaveName. 
        ///</summary>
        private String HLAlastSaveName_;

        ///<summary>
        ///Attribute #HLAlastSaveTime. 
        ///</summary>
        private ILogicalTime HLAlastSaveTime_;

        ///<summary>
        ///Attribute #HLAnextSaveName. 
        ///</summary>
        private String HLAnextSaveName_;

        ///<summary>
        ///Attribute #HLAnextSaveTime. 
        ///</summary>
        private ILogicalTime HLAnextSaveTime_;

        ///<summary>
        ///Attribute #HLAautoProvide. 
        ///</summary>
        private HLAswitch HLAautoProvide_;

        ///<summary>
        ///Attribute #HLAconveyRegionDesignatorSets. 
        ///</summary>
        private HLAswitch HLAconveyRegionDesignatorSets_;

        ///<summary> Returns a string representation of this HLAfederation. </summary>
        ///<returns> a string representation of this HLAfederation</returns>
        public override String ToString()
        {
            return "HLAfederation(" +
                     "HLAfederationName: " + HLAfederationName_ + ", " +
                     "HLAfederatesinFederation: " + HLAfederatesinFederation_ + ", " +
                     "HLARTIversion: " + HLARTIversion_ + ", " +
                     "HLAFDDID: " + HLAFDDID_ + ", " +
                     "HLAlastSaveName: " + HLAlastSaveName_ + ", " +
                     "HLAlastSaveTime: " + HLAlastSaveTime_ + ", " +
                     "HLAnextSaveName: " + HLAnextSaveName_ + ", " +
                     "HLAnextSaveTime: " + HLAnextSaveTime_ + ", " +
                     "HLAautoProvide: " + HLAautoProvide_ + ", " +
                     "HLAconveyRegionDesignatorSets: " + HLAconveyRegionDesignatorSets_ + 
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the HLAfederationName field.
        ///</summary>
        [HLAAttribute(Name = "HLAfederationName",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual String HLAfederationName
        {
            set {HLAfederationName_ = value; }
            get { return HLAfederationName_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAfederatesinFederation field.
        ///</summary>
        [HLAAttribute(Name = "HLAfederatesinFederation",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAhandleList",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Federate joins or resigns",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual HLAhandleList HLAfederatesinFederation
        {
            set {HLAfederatesinFederation_ = value; }
            get { return HLAfederatesinFederation_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLARTIversion field.
        ///</summary>
        [HLAAttribute(Name = "HLARTIversion",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual String HLARTIversion
        {
            set {HLARTIversion_ = value; }
            get { return HLARTIversion_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAFDDID field.
        ///</summary>
        [HLAAttribute(Name = "HLAFDDID",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual String HLAFDDID
        {
            set {HLAFDDID_ = value; }
            get { return HLAFDDID_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAlastSaveName field.
        ///</summary>
        [HLAAttribute(Name = "HLAlastSaveName",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual String HLAlastSaveName
        {
            set {HLAlastSaveName_ = value; }
            get { return HLAlastSaveName_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAlastSaveTime field.
        ///</summary>
        [HLAAttribute(Name = "HLAlastSaveTime",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAlogicalTime",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual ILogicalTime HLAlastSaveTime
        {
            set {HLAlastSaveTime_ = value; }
            get { return HLAlastSaveTime_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAnextSaveName field.
        ///</summary>
        [HLAAttribute(Name = "HLAnextSaveName",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual String HLAnextSaveName
        {
            set {HLAnextSaveName_ = value; }
            get { return HLAnextSaveName_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAnextSaveTime field.
        ///</summary>
        [HLAAttribute(Name = "HLAnextSaveTime",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAlogicalTime",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "Service invocation",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual ILogicalTime HLAnextSaveTime
        {
            set {HLAnextSaveTime_ = value; }
            get { return HLAnextSaveTime_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAautoProvide field.
        ///</summary>
        [HLAAttribute(Name = "HLAautoProvide",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAswitch",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "MOM interaction",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual HLAswitch HLAautoProvide
        {
            set {HLAautoProvide_ = value; }
            get { return HLAautoProvide_; }
        }


        ///<summary>
        /// Gets/Sets the value of the HLAconveyRegionDesignatorSets field.
        ///</summary>
        [HLAAttribute(Name = "HLAconveyRegionDesignatorSets",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAswitch",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Conditional,
                      UpdateCondition = "MOM interaction",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual HLAswitch HLAconveyRegionDesignatorSets
        {
            set {HLAconveyRegionDesignatorSets_ = value; }
            get { return HLAconveyRegionDesignatorSets_; }
        }

    }
}
