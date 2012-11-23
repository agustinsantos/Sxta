namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.BoostrapProtocol;


    ///<summary>
    ///A HLA serializer for HLAdestroyFederationExecutionMessage. 
    ///</summary>
    public class HLAdestroyFederationExecutionMessageXrtiSerializer : HLAmetaFederationMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAdestroyFederationExecutionMessageXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary> Writes this HLAdestroyFederationExecutionMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);
                writer.WriteHLAunicodeString(((HLAdestroyFederationExecutionMessage) obj).FederationExecutionName);
            }
            catch(System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAdestroyFederationExecutionMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAdestroyFederationExecutionMessage decodedValue;
            if (!(msg is HLAdestroyFederationExecutionMessage))
            {
                decodedValue = new HLAdestroyFederationExecutionMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAdestroyFederationExecutionMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAdestroyFederationExecutionMessage;
            try
            {
                decodedValue.FederationExecutionName = reader.ReadHLAunicodeString();
            }
            catch(System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}
