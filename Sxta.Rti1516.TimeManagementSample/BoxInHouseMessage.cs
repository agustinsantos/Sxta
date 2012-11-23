using System;
using System.IO;

using Hla.Rti1516;

using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.XrtiHandles;

namespace Sxta.Rti1516.TimeManagementSample
{
    ///<summary>
    ///Message for BoxInHouse iteraction : Notifies that the box is already in its
    ///house 
    ///</summary>
    [HLAInteractionClass(Name = "BoxInHouse",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Notifies that the box is already in its house",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class BoxInHouseMessage : HLAinteractionRootMessage
    {
        byte[] time;

        ///<summary>Time</summary> 
        [HLAInteractionParameter(Name = "time",
                      Semantics = "Time",
                      DataType = "HLAlogicalTime")]
        public byte[] Time
        {
            get { return time; }
            set { time = value; }
        }

        ///<summary> Returns a string representation of this BoxInHouseMessage. </summary>
        ///<returns> a string representation of this BoxInHouseMessage</returns>
        public override string ToString()
        {
            return "BoxInHouseMessage(" + base.ToString()
                   + ", Time: " + Time + ")";
        }
    }
}
