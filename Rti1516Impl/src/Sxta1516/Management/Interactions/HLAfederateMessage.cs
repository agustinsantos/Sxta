using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Serializers.XrtiEncoding;

namespace Sxta.Rti1516.Management
{
    ///<summary>
    ///Message for HLAfederate iteraction : Root class of MOM interactions that deal
    ///with a specific joined federate. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAfederate",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Root class of MOM interactions that deal with a specific joined federate.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLAfederateMessage : HLAmanagerMessage
    {
        HLAfederateHandle HLAfederate_;

        ///<summary>Handle of the joined federate that was provided when joining.</summary>
        [HLAInteractionParameter(Name = "HLAfederate",
                      Semantics = "Handle of the joined federate that was provided when joining.",
                      DataType = "HLAhandle")]
        public HLAfederateHandle HLAfederate
        {
            get { return HLAfederate_; }
            set { HLAfederate_ = value; }
        }

        ///<summary> Returns a string representation of this HLAfederateMessage. </summary>
        ///<returns> a string representation of this HLAfederateMessage</returns>
        public override string ToString()
        {
            return "HLAfederateMessage(" + base.ToString()
                   + ", HLAfederate: " + HLAfederate + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAfederateMessage. 
    ///</summary>
    public class HLAfederateMessageXrtiSerializer : HLAmanagerMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAfederateMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAfederateMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);
                int val = (((HLAfederateMessage)obj).HLAfederate).data;
                writer.WriteHLAinteger32BE(val);
                /*
                writer.WriteHLAinteger32BE((((HLAfederateMessage)obj).HLAfederate).Length);

                for (int i = 0; i < (((HLAfederateMessage)obj).HLAfederate).Length; i++)
                {
                    writer.WriteHLAoctet((((HLAfederateMessage)obj).HLAfederate)[i]);
                }
                */
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAfederateMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAfederateMessage decodedValue;
            if (!(msg is HLAfederateMessage))
            {
                decodedValue = new HLAfederateMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAfederateMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAfederateMessage;
            try
            {
                /*
                decodedValue.HLAfederate = new byte[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < decodedValue.HLAfederate.Length; i++)
                {
                    decodedValue.HLAfederate[i] = reader.ReadHLAoctet();
                }
                */
                decodedValue.HLAfederate = (HLAfederateHandle)reader.ReadHLAinteger32BE();
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}
