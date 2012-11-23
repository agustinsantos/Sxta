using System;
using System.Collections.Generic;
using System.Text;

using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Serializers.XrtiEncoding;

namespace Sxta.Rti1516.Management
{
    ///<summary>
    ///Message for HLAmanager iteraction : Root class of MOM interactions. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAmanager",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Root class of MOM interactions.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLAmanagerMessage : HLAinteractionRootMessage
    {
        ///<summary> Returns a string representation of this HLAmanagerMessage. </summary>
        ///<returns> a string representation of this HLAmanagerMessage</returns>
        public override string ToString()
        {
            return "HLAmanagerMessage(" + base.ToString() + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAmanagerMessage. 
    ///</summary>
    public class HLAmanagerMessageXrtiSerializer : HLAinteractionRootMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAmanagerMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAmanagerMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            base.Serialize(writer, obj);
        }

        ///<summary> Reads this HLAmanagerMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAmanagerMessage decodedValue;
            if (!(msg is HLAmanagerMessage))
            {
                decodedValue = new HLAmanagerMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAmanagerMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAmanagerMessage;
            return decodedValue;
        }
    }
}
