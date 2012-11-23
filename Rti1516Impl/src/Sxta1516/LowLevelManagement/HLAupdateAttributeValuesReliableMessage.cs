namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAupdateAttributeValuesReliable iteraction : Updates a set of
    ///attribute values with reliable transportation and receive ordering. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAupdateAttributeValuesReliable",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Updates a set of attribute values with reliable transportation and receive ordering.",
                    Transportation = "HLAreliable")]
    public class HLAupdateAttributeValuesReliableMessage : HLAupdateAttributeValuesMessage
    {
        ///<summary> Returns a string representation of this HLAupdateAttributeValuesReliableMessage. </summary>
        ///<returns> a string representation of this HLAupdateAttributeValuesReliableMessage</returns>
        public override string ToString()
        {
            return "HLAupdateAttributeValuesReliableMessage(" + base.ToString() + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAupdateAttributeValuesReliableMessage. 
    ///</summary>
    public class HLAupdateAttributeValuesReliableMessageXrtiSerializer : HLAupdateAttributeValuesMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAupdateAttributeValuesReliableMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAupdateAttributeValuesReliableMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            base.Serialize(writer, obj);
        }

        ///<summary> Reads this HLAupdateAttributeValuesReliableMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAupdateAttributeValuesReliableMessage decodedValue;
            if (!(msg is HLAupdateAttributeValuesReliableMessage))
            {
                decodedValue = new HLAupdateAttributeValuesReliableMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAupdateAttributeValuesReliableMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAupdateAttributeValuesReliableMessage;
            return decodedValue;
        }
    }
}
