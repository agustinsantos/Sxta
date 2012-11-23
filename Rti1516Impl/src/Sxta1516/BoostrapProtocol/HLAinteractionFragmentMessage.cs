
namespace Sxta.Rti1516.BoostrapProtocol
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for HLAinteractionFragment iteraction : A piece of a fragmented interaction.
    ///</summary>
    [HLAInteractionClass(Name = "HLAinteractionFragment",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                        Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                        Semantics = "A piece of a fragmented interaction.",
                        Transportation = "HLAbestEffort")]
    public class HLAinteractionFragmentMessage : BaseInteractionMessage
    {
        int interactionNumber;

        ///<summary>The sequence number of the fragmented interaction.</summary>
        [HLAInteractionParameter(Name = "interactionNumber",
                                  Semantics = "The sequence number of the fragmented interaction.",
                                  DataType = "HLAinteractionSequenceNumber")]
        public int InteractionNumber
        {
            get { return interactionNumber; }
            set { interactionNumber = value; }
        }

        int interactionSize;

        ///<summary>The size of the fragmented interaction.</summary> 
        [HLAInteractionParameter(Name = "interactionSize",
                                  Semantics = "The size of the fragmented interaction.",
                                  DataType = "HLAbufferSize")]
        public int InteractionSize
        {
            get { return interactionSize; }
            set { interactionSize = value; }
        }

        int fragmentOffset;

        ///<summary>The offset of this fragment in the buffer.</summary> 
        [HLAInteractionParameter(Name = "fragmentOffset",
                                 Semantics = "The offset of this fragment in the buffer.",
                                 DataType = "HLAbufferOffset")]
        public int FragmentOffset
        {
            get { return fragmentOffset; }
            set { fragmentOffset = value; }
        }

        byte[] fragmentContents;

        ///<summary>The contents of the fragment.</summary> 
        [HLAInteractionParameter(Name = "fragmentContents",
                                 Semantics = "The contents of the fragment.",
                                 DataType = "HLAopaqueData")]
        public byte[] FragmentContents
        {
            get { return fragmentContents; }
            set { fragmentContents = value; }
        }

        ///<summary> Returns a string representation of this HLAinteractionFragmentMessage. </summary>
        ///<returns> a string representation of this HLAinteractionFragmentMessage</returns>
        public override string ToString()
        {
            return "HLAinteractionFragmentMessage(" + base.ToString()
                   + ", InteractionNumber: " + InteractionNumber
                   + ", InteractionSize: " + InteractionSize
                   + ", FragmentOffset: " + FragmentOffset
                   + ", FragmentContents: " + BitConverter.ToString(FragmentContents) + ")";
        }
    }


    ///<summary>
    ///A HLA serializer for HLAinteractionFragmentMessage. 
    ///</summary>
    [Serializable]
    public class HLAinteractionFragmentMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLAinteractionFragmentMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAinteractionFragmentMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                writer.WriteHLAinteger32BE(((HLAinteractionFragmentMessage)obj).InteractionNumber);
                writer.WriteHLAinteger32BE(((HLAinteractionFragmentMessage)obj).InteractionSize);
                writer.WriteHLAinteger32BE(((HLAinteractionFragmentMessage)obj).FragmentOffset);
                writer.WriteHLAopaqueData(((HLAinteractionFragmentMessage)obj).FragmentContents);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }
        ///<summary> Reads this HLAinteractionFragmentMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg2)
        {
            HLAinteractionFragmentMessage msg = new HLAinteractionFragmentMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);
            try
            {
                msg.InteractionNumber = reader.ReadHLAinteger32BE();
                msg.InteractionSize = reader.ReadHLAinteger32BE();
                msg.FragmentOffset = reader.ReadHLAinteger32BE();
                msg.FragmentContents = reader.ReadHLAopaqueData();
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return msg;
        }
    }
}
