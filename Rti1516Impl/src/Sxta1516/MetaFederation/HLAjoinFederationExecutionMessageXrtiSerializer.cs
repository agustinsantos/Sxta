namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.BoostrapProtocol;

    ///<summary>
    ///A HLA serializer for HLAjoinFederationExecutionMessage. 
    ///</summary>
    public class HLAjoinFederationExecutionMessageXrtiSerializer : HLAmetaFederationMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAjoinFederationExecutionMessageXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary> Writes this HLAjoinFederationExecutionMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);
                writer.WriteHLAunicodeString(((HLAjoinFederationExecutionMessage) obj).FederationExecutionName);
                writer.WriteHLAunicodeString(((HLAjoinFederationExecutionMessage) obj).FederateType);
                writer.WriteHLAinteger64BE(((HLAjoinFederationExecutionMessage) obj).FederateHandle);
            }
            catch(System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAjoinFederationExecutionMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAjoinFederationExecutionMessage decodedValue;
            if (!(msg is HLAjoinFederationExecutionMessage))
            {
                decodedValue = new HLAjoinFederationExecutionMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAjoinFederationExecutionMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAjoinFederationExecutionMessage;
            try
            {
                decodedValue.FederationExecutionName = reader.ReadHLAunicodeString();
                decodedValue.FederateType = reader.ReadHLAunicodeString();
                decodedValue.FederateHandle = reader.ReadHLAinteger64BE();
            }
            catch(System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}
