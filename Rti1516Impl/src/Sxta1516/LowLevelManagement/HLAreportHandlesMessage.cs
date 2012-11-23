namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAreportHandles iteraction : Reports a block of handles.
    ///</summary>
    [Serializable]
    public class HLAreportHandlesMessage : BaseInteractionMessage
    {
        long blockStart;

        ///<summary>The first handle in the block.</summary> 
        public long BlockStart
        {
            get { return blockStart; }
            set { blockStart = value; }
        }

        long blockSize;

        ///<summary>The number of handles in the block.</summary> 
        public long BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        ///<summary> Returns a string representation of this HLAreportHandlesMessage. </summary>
        ///<returns> a string representation of this HLAreportHandlesMessage</returns>
        public override string ToString()
        {
            return "HLAreportHandlesMessage(" + base.ToString() + ", BlockStart: " + BlockStart + ", BlockSize: " + BlockSize + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAreportHandlesMessage. 
    ///</summary>
    [Serializable]
    public class HLAreportHandlesMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLAreportHandlesMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAreportHandlesMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                writer.WriteHLAinteger64BE(((HLAreportHandlesMessage)obj).BlockStart);
                writer.WriteHLAinteger64BE(((HLAreportHandlesMessage)obj).BlockSize);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }
        ///<summary> Reads this HLAreportHandlesMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg2)
        {
            HLAreportHandlesMessage msg = new HLAreportHandlesMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);
            try
            {
                msg.BlockStart = reader.ReadHLAinteger64BE();
                msg.BlockSize = reader.ReadHLAinteger64BE();
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return msg;
        }
    }
}
