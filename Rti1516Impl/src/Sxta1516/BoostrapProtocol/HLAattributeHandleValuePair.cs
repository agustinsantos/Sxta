namespace Sxta.Rti1516.BoostrapProtocol
{

    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;
    using XrtiSerializerManager = Sxta.Rti1516.Serializers.XrtiEncoding.XrtiSerializerManager;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;

    ///<summary>
    ///Pairs an attribute handle with an encoded value. 
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler from BootstrapObjectModel.xml </author>
    [Serializable]
    public class HLAattributeHandleValuePair : System.ICloneable
    {
        ///<summary>
        ///Attribute handle. 
        ///</summary>
        private long attributeHandle;

        ///<summary>
        ///Encoded value. 
        ///</summary>
        private object attributeValue;

        ///<summary>Default constructor.</summary>
        public HLAattributeHandleValuePair()
        {
            attributeValue = null;
        }

        ///<summary> Constructor. </summary>
        ///<param name="pAttributeHandle"> the value of the attributeHandle field</param>
        ///<param name="pAttributeValue"> the value of the attributeValue field</param>
        public HLAattributeHandleValuePair(long pAttributeHandle, object pAttributeValue)
        {
            this.attributeHandle = pAttributeHandle;
            this.attributeValue = pAttributeValue;
        }

        ///<summary> Copy constructor. </summary>
        ///<param name="other"> the other HLAattributeHandleValuePair to copy</param>
        public HLAattributeHandleValuePair(HLAattributeHandleValuePair other)
        {
            this.attributeHandle = other.attributeHandle;
            this.attributeValue = other.attributeValue;
        }

        ///<summary> Returns a string representation of this HLAattributeHandleValuePair. </summary>
        ///<returns> a string representation of this HLAattributeHandleValuePair</returns>
        public override String ToString()
        {
            /*
            return "HLAattributeHandleValuePair(" +
                     "attributeHandle: " + attributeHandle + ", " +
                     "attributeValue: " + attributeValue +
                   ")";
            */

            return "[" + attributeHandle + " = " + attributeValue + "]";
        }

        ///<summary>
        /// Gets/Sets the value of the attributeHandle field.
        ///</summary>
        public long AttributeHandle
        {
            set { attributeHandle = value; }
            get { return attributeHandle; }
        }


        ///<summary>
        /// Gets/Sets the value of the attributeValue field.
        ///</summary>
        public object AttributeValue
        {
            set { attributeValue = value; }
            get { return attributeValue; }
        }


        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new HLAattributeHandleValuePair(this);
        }

        #endregion
    }

    ///<summary>
    ///Serializes and deserializes HLAattributeHandleValuePair objects into and
    ///from HLA formats. 
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler from BootstrapObjectModel.xml </author>
    public class HLAattributeHandleValuePairXrtiSerializer : HlaXrtiBaseSerializer
    {

        public HLAattributeHandleValuePairXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary>
        /// Writes this HLAattributeHandleValuePair to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            HLAattributeHandleValuePair attributeHandleValue = (HLAattributeHandleValuePair)obj;
            writer.WriteHLAinteger64BE(attributeHandleValue.AttributeHandle);
            serializerManager.GetSerializer(attributeHandleValue.AttributeHandle).Serialize(writer, attributeHandleValue.AttributeValue);
        }


        ///<summary>
        /// Reads and returns a HLAattributeHandleValuePair from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            if (msg.Equals(BaseInteractionMessage.NullBaseInteractionMessage))
            {
                msg = new HLAattributeHandleValuePair();
            }
            HLAattributeHandleValuePair decodedValue = msg as HLAattributeHandleValuePair;
            decodedValue.AttributeHandle = reader.ReadHLAinteger64BE();
            object tmp = BaseInteractionMessage.NullBaseInteractionMessage;
            decodedValue.AttributeValue = serializerManager.GetSerializer(decodedValue.AttributeHandle).Deserialize(reader, ref tmp);
            return decodedValue;
        }
    }

}
