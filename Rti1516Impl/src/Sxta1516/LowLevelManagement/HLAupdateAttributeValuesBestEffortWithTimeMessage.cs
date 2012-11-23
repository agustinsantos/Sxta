namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;


    ///<summary>
    ///Message for HLAupdateAttributeValuesBestEffortWithTime iteraction : Updates
    ///a set of attribute values with best-effort transportation and receive ordering.
    ///</summary>
    [HLAInteractionClass(Name = "HLAupdateAttributeValuesBestEffortWithTime",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Updates a set of attribute values with best-effort transportation and receive ordering.",
                    Transportation = "HLAbestEffort")]
    public class HLAupdateAttributeValuesBestEffortWithTimeMessage : HLAupdateAttributeValuesWithTimeMessage
    {
        ///<summary> Returns a string representation of this HLAupdateAttributeValuesBestEffortWithTimeMessage. </summary>
        ///<returns> a string representation of this HLAupdateAttributeValuesBestEffortWithTimeMessage</returns>
        public override string ToString()
        {
            return "HLAupdateAttributeValuesBestEffortWithTimeMessage(" + base.ToString() + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAupdateAttributeValuesBestEffortWithTimeMessage.
    ///</summary>
    public class HLAupdateAttributeValuesBestEffortWithTimeMessageXrtiSerializer : HLAupdateAttributeValuesWithTimeMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAupdateAttributeValuesBestEffortWithTimeMessageXrtiSerializer(XrtiSerializerManager manager)//, ILogicalTimeFactory aLogicalTimeFactory)
            : base(manager)//, aLogicalTimeFactory)
        {
        }

        ///<summary> Writes this HLAupdateAttributeValuesBestEffortWithTimeMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            base.Serialize(writer, obj);

            //byte[] logicalTimeBytesArray = ((HLAupdateAttributeValuesBestEffortWithTimeMessage)obj).LogicalTime;
            //writer.WriteHLAopaqueData(logicalTimeBytesArray);
        }

        ///<summary> Reads this HLAupdateAttributeValuesBestEffortWithTimeMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAupdateAttributeValuesBestEffortWithTimeMessage decodedValue;
            if (!(msg is HLAupdateAttributeValuesBestEffortWithTimeMessage))
            {
                decodedValue = new HLAupdateAttributeValuesBestEffortWithTimeMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAupdateAttributeValuesBestEffortWithTimeMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAupdateAttributeValuesBestEffortWithTimeMessage;

            //decodedValue.LogicalTime = reader.ReadHLAopaqueData();

            return decodedValue;
        }
    }

}
