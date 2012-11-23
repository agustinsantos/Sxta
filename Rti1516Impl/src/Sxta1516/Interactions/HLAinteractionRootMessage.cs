namespace Sxta.Rti1516.Interactions
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;

    ///<summary> Message for HLAinteractionRoot iteraction : HLAinteractionRoot classSerializerHelperName.</summary>
    [HLAInteractionClass(Name = "HLAinteractionRoot",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLAinteractionRootMessage : BaseInteractionMessage

    {
        ///<summary> Returns a string representation of this HLAinteractionRootMessage. </summary>
        ///<returns> a string representation of this HLAinteractionRootMessage</returns>
        public override string ToString()
        {
            return "HLAinteractionRootMessage(" + base.ToString() + ")";
        }
    }
}
