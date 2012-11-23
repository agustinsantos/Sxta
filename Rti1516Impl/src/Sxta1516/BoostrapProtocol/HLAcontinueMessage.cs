namespace Sxta.Rti1516.BoostrapProtocol
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAcontinue iteraction : Notifies the federate that the executive
    ///is done sending callbacks. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAcontinue",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                        Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                        Semantics = "Notifies the federate that the executive is done sending callbacks.",
                        Transportation = "HLAreliable")]
    public class HLAcontinueMessage : BaseInteractionMessage
    {
        ///<summary> Returns a string representation of this HLAcontinueMessage. </summary>
        ///<returns> a string representation of this HLAcontinueMessage</returns>
        public override string ToString()
        {
            return "HLAcontinueMessage(" + base.ToString() + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAcontinueMessage. 
    ///</summary>
    [Serializable]
    public class HLAcontinueMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLAcontinueMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAcontinueMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            //base.Serialize(writer, obj);
        }

        ///<summary> Reads this HLAcontinueMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg2)
        {
            HLAcontinueMessage msg = new HLAcontinueMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);
            return msg;
        }
    }
}
