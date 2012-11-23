namespace Sxta.Rti1516.LowLevelManagement
{
    using System;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    /// <summary>
    /// The size of a block of handles.
    /// <code>
    /// <simpleData name="HLAhandleBlockSize"
    ///        representation="HLAinteger64BE"
    ///        units="handles"
    ///        semantics="The size of a block of handles."/>
    /// </code>
    /// </summary>
    [HLASimpleDataAttribute(Name = "HLAhandleBlockSize",
                            Representation = "HLAinteger64BE",
                            Units = "handles",
                            Semantics = "The size of a block of handles.")]
    public struct HLAhandleBlockSize
    {
        public Int64 data;

        public static implicit operator Int64(HLAhandleBlockSize val)
        {
            return val.data;
        }

        public static implicit operator HLAhandleBlockSize(Int64 val)
        {
            HLAhandleBlockSize hbz = new HLAhandleBlockSize();
            hbz.data = val;
            return hbz;
        }
    }

    ///<summary>
    ///Message for HLArequestHandles iteraction : Requests a block of handles.
    /// <code>
    /// <interactionClass name="HLArequestHandles"
    ///              sharing="PublishSubscribe"
    ///              transportation="HLAreliable"
    ///              order="Receive"
    ///              semantics="Requests a block of handles.">
    /// <parameter name="blockSize"
    ///           dataType="HLAhandleBlockSize"
    ///           semantics="The number of handles desired."/>
    //</interactionClass>
    /// </code>
    ///</summary>
    [Serializable]
    [HLAInteractionClassMessage(Name = "HLArequestHandles")]
    public class HLArequestHandlesMessage : BaseInteractionMessage
    {
        long blockSize;

        ///<summary>The number of handles desired.</summary> 
        public HLAhandleBlockSize BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        ///<summary> Returns a string representation of this HLArequestHandlesMessage. </summary>
        ///<returns> a string representation of this HLArequestHandlesMessage</returns>
        public override string ToString()
        {
            return "HLArequestHandlesMessage(" + base.ToString() + ", BlockSize: " + BlockSize + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLArequestHandlesMessage. 
    ///</summary>
    [Serializable]
    public class HLArequestHandlesMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLArequestHandlesMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLArequestHandlesMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                writer.WriteHLAinteger64BE(((HLArequestHandlesMessage)obj).BlockSize);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }
        ///<summary> Reads this HLArequestHandlesMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg2)
        {
            HLArequestHandlesMessage msg = new HLArequestHandlesMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);
            try
            {
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
