namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLArequestAttributeValueUpdate iteraction : Requests an attribute
    ///value update. 
    ///</summary>
    [Serializable]
    public class HLArequestAttributeValueUpdateMessage : BaseInteractionMessage
    {
        long objectInstanceHandle;

        ///<summary>Object instance handle.</summary> 
        public long ObjectInstanceHandle
        {
            get { return objectInstanceHandle; }
            set { objectInstanceHandle = value; }
        }

        long[] attributeHandleList;

        ///<summary>List of attribute handles.</summary> 
        public long[] AttributeHandleList
        {
            get { return attributeHandleList; }
            set { attributeHandleList = value; }
        }

        ///<summary> Returns a string representation of this HLArequestAttributeValueUpdateMessage. </summary>
        ///<returns> a string representation of this HLArequestAttributeValueUpdateMessage</returns>
        public override string ToString()
        {
            return "HLArequestAttributeValueUpdateMessage(" + base.ToString() + ", ObjectInstanceHandle: " + ObjectInstanceHandle + ", AttributeHandleList: " + AttributeHandleList + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLArequestAttributeValueUpdateMessage. 
    ///</summary>
    [Serializable]
    public class HLArequestAttributeValueUpdateMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLArequestAttributeValueUpdateMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLArequestAttributeValueUpdateMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                writer.WriteHLAinteger64BE(((HLArequestAttributeValueUpdateMessage)obj).ObjectInstanceHandle);
                writer.WriteHLAinteger32BE(((HLArequestAttributeValueUpdateMessage)obj).AttributeHandleList.Length);

                for (int i = 0; i < ((HLArequestAttributeValueUpdateMessage)obj).AttributeHandleList.Length; i++)
                {
                    writer.WriteHLAinteger64BE(((HLArequestAttributeValueUpdateMessage)obj).AttributeHandleList[i]);
                }
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }
        ///<summary> Reads this HLArequestAttributeValueUpdateMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg2)
        {
            HLArequestAttributeValueUpdateMessage msg = new HLArequestAttributeValueUpdateMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);

            try
            {
                msg.ObjectInstanceHandle = reader.ReadHLAinteger64BE();
                msg.AttributeHandleList = new long[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < msg.AttributeHandleList.Length; i++)
                {
                    msg.AttributeHandleList[i] = reader.ReadHLAinteger64BE();
                }
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return msg;
        }
    }
}
