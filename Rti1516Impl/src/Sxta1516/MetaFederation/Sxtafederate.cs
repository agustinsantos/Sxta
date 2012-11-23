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
            return "Sxtafederate(" + base.ToString() + ", HLAisJoined:" + HLAisJoined_ + ")";
        }

    }
}
