namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.BoostrapProtocol;

    ///<summary>
    ///Message for HLAupdateAttributeValues iteraction : Updates a set of attribute
    ///values. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAupdateAttributeValues",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Updates a set of attribute values.",
                    Transportation = "HLAreliable")]
    public class HLAupdateAttributeValuesMessage : BaseInteractionMessage
    {
        long objectInstanceHandle;

        ///<summary>Object instance handle.</summary> 
        [HLAInteractionParameter(Name = "objectInstanceHandle",
                      Semantics = "Object instance handle.",
                      DataType = "HLAnormalizedH5andle")]
        public long ObjectInstanceHandle
        {
            get { return objectInstanceHandle; }
            set { objectInstanceHandle = value; }
        }

        HLAattributeHandleValuePair[] attributeHandleValuePairList;

        ///<summary>List of attribute handle/value pairs.</summary> 
        [HLAInteractionParameter(Name = "attributeHandleValuePairList",
                      Semantics = "List of attribute handle/value pairs.",
                      DataType = "HLAattributeHandleValuePairList")]
        public HLAattributeHandleValuePair[] AttributeHandleValuePairList
        {
            get { return attributeHandleValuePairList; }
            set { attributeHandleValuePairList = value; }
        }

        ///<summary> Returns a string representation of this HLAupdateAttributeValuesMessage. </summary>
        ///<returns> a string representation of this HLAupdateAttributeValuesMessage</returns>
        public override string ToString()
        {
            string returned = "HLAupdateAttributeValuesMessage(" + base.ToString()
                   + ", ObjectInstanceHandle: " + ObjectInstanceHandle
                   + ", AttributeHandleValuePairList[";

            for (int i = 0; i < attributeHandleValuePairList.Length; i++)
            {
                if (i + 1 == attributeHandleValuePairList.Length)
                    returned = returned + attributeHandleValuePairList[i];
                else
                    returned = returned + attributeHandleValuePairList[i] + ", ";
            }

            return returned + "]";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAupdateAttributeValuesMessage. 
    ///</summary>
    public class HLAupdateAttributeValuesMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        IHlaEncodingSerializer hlaAttributeHandleValuePairXrtiSerializer;

        ///<summary> Constructor </summary>
        public HLAupdateAttributeValuesMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
            hlaAttributeHandleValuePairXrtiSerializer = manager.GetSerializer(typeof(HLAattributeHandleValuePair));
        }

        ///<summary> Writes this HLAupdateAttributeValuesMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                writer.WriteHLAinteger64BE(((HLAupdateAttributeValuesMessage)obj).ObjectInstanceHandle);
                writer.WriteHLAinteger32BE((((HLAupdateAttributeValuesMessage)obj).AttributeHandleValuePairList).Length);

                for (int i = 0; i < (((HLAupdateAttributeValuesMessage)obj).AttributeHandleValuePairList).Length; i++)
                {
                    hlaAttributeHandleValuePairXrtiSerializer.Serialize(writer, ((HLAupdateAttributeValuesMessage)obj).AttributeHandleValuePairList[i]);
                }
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAupdateAttributeValuesMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAupdateAttributeValuesMessage decodedValue;
            if (!(msg is HLAupdateAttributeValuesMessage))
            {
                decodedValue = new HLAupdateAttributeValuesMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAupdateAttributeValuesMessage;
            }
            object tmp = decodedValue;
            
            try
            {
                decodedValue = base.Deserialize(reader, ref tmp) as HLAupdateAttributeValuesMessage;
             
                decodedValue.ObjectInstanceHandle = reader.ReadHLAinteger64BE();
                decodedValue.AttributeHandleValuePairList = new HLAattributeHandleValuePair[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < decodedValue.AttributeHandleValuePairList.Length; i++)
                {
                    tmp = BaseInteractionMessage.NullBaseInteractionMessage;
                    decodedValue.AttributeHandleValuePairList[i] = (HLAattributeHandleValuePair)hlaAttributeHandleValuePairXrtiSerializer.Deserialize(reader, ref tmp);
                }
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}
