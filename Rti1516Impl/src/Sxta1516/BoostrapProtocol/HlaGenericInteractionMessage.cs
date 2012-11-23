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
    ///Message for HLAGenericInteraction iteraction : A piece of a fragmented interaction.
    ///</summary>
    [HLAInteractionClass(Name = "HLAGenericInteraction",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "A piece of a fragmented interaction.",
                    Transportation = "HLAbestEffort")]
    public class HLAGenericInteractionMessage : BaseInteractionMessage
    {
        HLAparameterHandleValuePair[] parameterList;

        ///<summary>List of parameter handle/value pairs.</summary> 
        [HLAInteractionParameter(Name = "parameterList",
                      Semantics = "List of parameter handle/value pairs.",
                      DataType = "HLAparameterHandleValuePairList")]
        public HLAparameterHandleValuePair[] ParameterList
        {
            get { return parameterList; }
            set { parameterList = value; }
        }

        ///<summary> Returns a string representation of this HLAGenericInteractionMessage. </summary>
        ///<returns> a string representation of this HLAGenericInteractionMessage</returns>
        public override string ToString()
        {
            return "HLAGenericInteractionMessage(" + base.ToString()
                   + ", ParameterList: " + ParameterList + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HlaGenericInteractionMessage. 
    ///</summary>
    [Serializable]
    public class HLAGenericInteractionMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public HLAGenericInteractionMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }


        ///<summary> Writes this HlaGenericInteractionMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                writer.WriteHLAinteger32BE(((HLAGenericInteractionMessage)obj).ParameterList.Length);

                for (int i = 0; i < ((HLAGenericInteractionMessage)obj).ParameterList.Length; i++)
                {
                    HLAparameterHandleValuePairXrtiSerializer.Serialize(writer, ((HLAGenericInteractionMessage)obj).ParameterList[i]);
                }
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }
        ///<summary> Reads this HlaGenericInteractionMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg2)
        {
            HLAGenericInteractionMessage msg = new HLAGenericInteractionMessage();
            msg.CopyTo((BaseInteractionMessage)msg2);
            try
            {
                msg.ParameterList = new HLAparameterHandleValuePair[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < msg.ParameterList.Length; i++)
                {
                    msg.ParameterList[i] = HLAparameterHandleValuePairXrtiSerializer.Deserialize(reader);
                }
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return msg;
        }
    }
}
