namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAupdateAttributeValuesReliableWithTime iteraction : Updates
    ///a set of attribute values with reliable transportation and receive ordering.
    ///</summary>
    [HLAInteractionClass(Name = "HLAupdateAttributeValuesReliableWithTime",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Updates a set of attribute values with reliable transportation and receive ordering.",
                    Transportation = "HLAreliable")]
    public class HLAupdateAttributeValuesReliableWithTimeMessage : HLAupdateAttributeValuesWithTimeMessage
    {
        ///<summary> Returns a string representation of this HLAupdateAttributeValuesReliableWithTimeMessage. </summary>
        ///<returns> a string representation of this HLAupdateAttributeValuesReliableWithTimeMessage</returns>
        public override string ToString()
        {
            return "HLAupdateAttributeValuesReliableWithTimeMessage(" + base.ToString() + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAupdateAttributeValuesReliableWithTimeMessage.
    ///</summary>
    public class HLAupdateAttributeValuesReliableWithTimeMessageXrtiSerializer : HLAupdateAttributeValuesWithTimeMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAupdateAttributeValuesReliableWithTimeMessageXrtiSerializer(XrtiSerializerManager manager)//, ILogicalTimeFactory aLogicalTimeFactory)
            : base(manager)//, aLogicalTimeFactory)
        {
        }

        ///<summary> Writes this HLAupdateAttributeValuesReliableWithTimeMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            base.Serialize(writer, obj);

            //byte[] logicalTimeBytesArray = ((HLAupdateAttributeValuesReliableWithTimeMessage)obj).LogicalTime;
            //writer.WriteHLAopaqueData(logicalTimeBytesArray);
        }

        ///<summary> Reads this HLAupdateAttributeValuesReliableWithTimeMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAupdateAttributeValuesReliableWithTimeMessage decodedValue;
            if (!(msg is HLAupdateAttributeValuesReliableWithTimeMessage))
            {
                decodedValue = new HLAupdateAttributeValuesReliableWithTimeMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAupdateAttributeValuesReliableWithTimeMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAupdateAttributeValuesReliableWithTimeMessage;

            //decodedValue.LogicalTime = reader.ReadHLAopaqueData();

            return decodedValue;
        }
    }
}
