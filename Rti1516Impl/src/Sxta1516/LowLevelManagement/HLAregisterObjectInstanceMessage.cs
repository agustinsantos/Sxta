using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    [HLAInteractionClass(Name = "HLAregisterObjectInstanceMessage",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Registers a new object instance.",
                    Transportation = "HLAreliable")]
    public class HLAregisterObjectInstanceMessage : BaseInteractionMessage
    {
        String objectName;

        ///<summary>The name of the object.</summary> 
        public String ObjectName
        {
            get { return objectName; }
            set { objectName = value; }
        }

        long objectInstanceHandle;

        ///<summary>The object instance handle.</summary> 
        public long ObjectInstanceHandle
        {
            get { return objectInstanceHandle; }
            set { objectInstanceHandle = value; }
        }

        long objectClassHandle;

        ///<summary>The object class handle.</summary> 
        public long ObjectClassHandle
        {
            get { return objectClassHandle; }
            set { objectClassHandle = value; }
        }

        ///<summary> Returns a string representation of this HLAregisterObjectInstanceMessage. </summary>
        ///<returns> a string representation of this HLAregisterObjectInstanceMessage</returns>
        public override string ToString()
        {
            return "HLAregisterObjectInstanceMessage(" + base.ToString() + ", ObjectName: " + ObjectName + ", ObjectInstanceHandle: " + ObjectInstanceHandle + ", ObjectClassHandle: " + ObjectClassHandle + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAregisterObjectInstanceMessage. 
    ///</summary>
    [Serializable]
    public class HLAregisterObjectInstanceMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLAregisterObjectInstanceMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAregisterObjectInstanceMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                writer.WriteHLAunicodeString(((HLAregisterObjectInstanceMessage)obj).ObjectName);
                writer.WriteHLAinteger64BE(((HLAregisterObjectInstanceMessage)obj).ObjectInstanceHandle);
                writer.WriteHLAinteger64BE(((HLAregisterObjectInstanceMessage)obj).ObjectClassHandle);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }
        ///<summary> Reads this HLAregisterObjectInstanceMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            /* msg2
            HLAregisterObjectInstanceMessage msg = new HLAregisterObjectInstanceMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);

            try
            {
                msg.ObjectName = reader.ReadHLAunicodeString();
                msg.ObjectInstanceHandle = reader.ReadHLAinteger64BE();
                msg.ObjectClassHandle = reader.ReadHLAinteger64BE();
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return msg;
            */

            HLAregisterObjectInstanceMessage decodedValue;
            if (!(msg is HLAregisterObjectInstanceMessage))
            {
                decodedValue = new HLAregisterObjectInstanceMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAregisterObjectInstanceMessage;
            }
            object tmp = decodedValue;
            
            try
            {
                decodedValue = base.Deserialize(reader, ref tmp) as HLAregisterObjectInstanceMessage;

                decodedValue.ObjectName = reader.ReadHLAunicodeString();
                decodedValue.ObjectInstanceHandle = reader.ReadHLAinteger64BE();
                decodedValue.ObjectClassHandle = reader.ReadHLAinteger64BE();
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}
