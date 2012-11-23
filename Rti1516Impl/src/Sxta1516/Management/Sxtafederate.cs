namespace Sxta.Rti1516.Management
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;


    [HLAObjectClass(Name = "Sxtafederate",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                    Semantics = "None")]
    public class Sxtafederate : HLAfederate
    {
        private bool HLAisJoined_ = false;

        ///<summary>Gets/Sets the value of the HLAfederateHandle attribute.</summary>
        [HLAAttribute(Name = "HLAisJoined",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAboolean",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual bool HLAisJoined
        {
            set { HLAisJoined_ = value; }
            get { return HLAisJoined_; }
        }

        private String HLAfederationNameJoined_;

        ///<summary>Gets/Sets the value of the HLAfederateHandle attribute.</summary>
        [HLAAttribute(Name = "HLAfederationNameJoined",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual String HLAfederationNameJoined
        {
            set { HLAfederationNameJoined_ = value; }
            get { return HLAfederationNameJoined_; }
        }

        private ILogicalTime HLApendingTime_;

        ///<summary>Gets/Sets the value of the HLAlogicalTime attribute.</summary>
        [HLAAttribute(Name = "HLApendingTime",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Publish,
                      DataType = "HLAlogicalTime",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Periodic,
                      UpdateCondition = "HLAsetTiming.HLAreportPeriod",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "Federate")]
        public virtual ILogicalTime HLApendingTime
        {
            set 
            {
                HLApendingTime_ = ConvertToFederationLogicalTimeRepresentation(value); 
            }
            get 
            {
                if (HLApendingTime_ != null)
                {
                    HLApendingTime_ = ConvertToFederationLogicalTimeRepresentation(HLApendingTime_);
                }
                return HLApendingTime_; 
            }
        }

        protected Sxtafederate() : base() { }

        #region Constructor
        // Create an instance of Sxtafederate
        public static Sxtafederate NewSxtafederate()
        {
            myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            return (Sxtafederate)NewInstance(myCallType);
        }

        #endregion

        public override string ToString()
        {
            return "Sxtafederate(" + base.ToString() + ", HLAisJoined:" + HLAisJoined_ + ", HLAfederationNameJoined:" + HLAfederationNameJoined_ + ")";
        }

    }
}
