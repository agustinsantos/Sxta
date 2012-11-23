namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;

    ///<summary>
    ///Message for HLAmetaFederation iteraction : Root class of MFOM interactions.
    ///</summary>
    [HLAInteractionClass(Name = "HLAmetaFederation",
                         Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                         Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                         Semantics = "Root class of MFOM interactions.",
                         Dimensions = "NA",
                         Transportation = "HLAreliable")]
    public class HLAmetaFederationMessage : BaseInteractionMessage
    {
        ///<summary> Returns a string representation of this HLAmetaFederationMessage. </summary>
        ///<returns> a string representation of this HLAmetaFederationMessage</returns>
        public override string ToString()
        {
            return "HLAmetaFederationMessage(" + base.ToString() + ")";
        }
    }
}
