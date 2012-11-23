namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAupdateAttributeValuesWithTime iteraction : Updates a set of
    ///attribute values. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAupdateAttributeValuesWithTime",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Updates a set of attribute values.",
                    Transportation = "HLAreliable")]
    public class HLAupdateAttributeValuesWithTimeMessage : HLAupdateAttributeValuesMessage
    {
        //ILogicalTime logicalTime;
        byte[] logicalTime;

        ///<summary>xxx</summary> 
        [HLAInteractionParameter(Name = "logicalTime",
                      Semantics = "xxx",
                      DataType = "HLAlogicalTime")]
        public byte[] LogicalTime 
        {
            get { return logicalTime; }
            set { logicalTime = value; }
        }

        ///<summary> Returns a string representation of this HLAupdateAttributeValuesWithTimeMessage. </summary>
        ///<returns> a string representation of this HLAupdateAttributeValuesWithTimeMessage</returns>
        public override string ToString()
        {
            return "HLAupdateAttributeValuesWithTimeMessage(" + base.ToString()
                   + ", LogicalTime: " + LogicalTime + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAupdateAttributeValuesWithTimeMessage. 
    ///</summary>
    public class HLAupdateAttributeValuesWithTimeMessageXrtiSerializer : HLAupdateAttributeValuesMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAupdateAttributeValuesWithTimeMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAupdateAttributeValuesWithTimeMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                byte[] logicalTimeBytesArray = ((HLAupdateAttributeValuesWithTimeMessage)obj).LogicalTime;
                writer.WriteHLAopaqueData(logicalTimeBytesArray);
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAupdateAttributeValuesWithTimeMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAupdateAttributeValuesWithTimeMessage decodedValue;
            if (!(msg is HLAupdateAttributeValuesWithTimeMessage))
            {
                decodedValue = new HLAupdateAttributeValuesWithTimeMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAupdateAttributeValuesWithTimeMessage;
            }

            object tmp = decodedValue;

            try
            {
                decodedValue = base.Deserialize(reader, ref tmp) as HLAupdateAttributeValuesWithTimeMessage;

                decodedValue.LogicalTime = reader.ReadHLAopaqueData();
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

            return decodedValue;
        }
    }
}
