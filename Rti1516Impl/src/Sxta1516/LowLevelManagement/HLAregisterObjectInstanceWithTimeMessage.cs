namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAregisterObjectInstanceWithTime iteraction : Registers a new
    ///object instance. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAregisterObjectInstanceWithTime",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Registers a new object instance with timestamp.",
                    Transportation = "HLAreliable")]
    public class HLAregisterObjectInstanceWithTimeMessage : HLAregisterObjectInstanceMessage
    {
        //private ILogicalTime logicalTime;
        private byte[] logicalTime;

        ///<summary>Time</summary> 
        [HLAInteractionParameter(Name = "logicalTime",
                      Semantics = "Time",
                      DataType = "HLAlogicalTime")]
        public byte[] LogicalTime // ILogicalTime
        {
            get { return logicalTime; }
            set { logicalTime = value; }
        }

        ///<summary> Returns a string representation of this HLAregisterObjectInstanceWithTimeMessage. </summary>
        ///<returns> a string representation of this HLAregisterObjectInstanceWithTimeMessage</returns>
        public override string ToString()
        {
            return "HLAregisterObjectInstanceWithTimeMessage(" + base.ToString()
                   + ", LogicalTime: " + LogicalTime + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAregisterObjectInstanceWithTimeMessage. 
    ///</summary>
    public class HLAregisterObjectInstanceWithTimeMessageXrtiSerializer : HLAregisterObjectInstanceMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAregisterObjectInstanceWithTimeMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAregisterObjectInstanceWithTimeMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                byte[] logicalTimeBytesArray = ((HLAregisterObjectInstanceWithTimeMessage)obj).LogicalTime;
                writer.WriteHLAopaqueData(logicalTimeBytesArray);
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAregisterObjectInstanceWithTimeMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAregisterObjectInstanceWithTimeMessage decodedValue;
            if (!(msg is HLAregisterObjectInstanceWithTimeMessage))
            {
                decodedValue = new HLAregisterObjectInstanceWithTimeMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.CopyTo(baseMsg);
                //decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                //decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                //decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAregisterObjectInstanceWithTimeMessage;
            }

            object tmp = decodedValue;

            try
            {
                decodedValue = base.Deserialize(reader, ref tmp) as HLAregisterObjectInstanceWithTimeMessage;
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
