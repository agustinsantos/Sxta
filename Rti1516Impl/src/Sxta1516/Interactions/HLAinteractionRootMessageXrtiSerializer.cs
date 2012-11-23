namespace Sxta.Rti1516.Interactions
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    ///<summary>
    ///A HLA serializer for HLAinteractionRootMessage. 
    ///</summary>
    public class HLAinteractionRootMessageXrtiSerializer: BaseInteractionMessageXrtiSerializer

    {
        ///<summary> Constructor </summary>
        public HLAinteractionRootMessageXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary> Writes this HLAinteractionRootMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            base.Serialize(writer, obj);
        }

        ///<summary> Reads this HLAinteractionRootMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAinteractionRootMessage decodedValue;
            if (!(msg is HLAinteractionRootMessage))
            {
                decodedValue = new HLAinteractionRootMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAinteractionRootMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAinteractionRootMessage;

            return decodedValue;
        }
    }
}
