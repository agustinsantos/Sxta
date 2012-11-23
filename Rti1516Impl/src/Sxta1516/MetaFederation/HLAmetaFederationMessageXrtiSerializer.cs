namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.BoostrapProtocol;


    ///<summary>
    ///A HLA serializer for HLAmetaFederationMessage. 
    ///</summary>
    public class HLAmetaFederationMessageXrtiSerializer : HLAinteractionRootMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAmetaFederationMessageXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary> Writes this HLAmetaFederationMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
        }

        ///<summary> Reads this HLAmetaFederationMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAmetaFederationMessage decodedValue;
            if (!(msg is HLAmetaFederationMessage))
            {
                decodedValue = new HLAmetaFederationMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAmetaFederationMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAmetaFederationMessage;
            return decodedValue;
        }
    }
}
