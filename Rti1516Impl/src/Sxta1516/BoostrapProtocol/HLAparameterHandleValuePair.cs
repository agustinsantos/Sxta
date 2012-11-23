namespace Sxta.Rti1516.BoostrapProtocol
{

    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Pairs a parameter handle with an encoded value. 
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler from BootstrapObjectModel.xml </author>
    [Serializable]
    public class HLAparameterHandleValuePair : System.ICloneable
    {
        ///<summary>
        ///Parameter handle. 
        ///</summary>
        private long parameterHandle;

        ///<summary>
        ///Encoded value. 
        ///</summary>
        private byte[] parameterValue;

        ///<summary>Default constructor.</summary>
        public HLAparameterHandleValuePair()
        {
            parameterValue = new byte[0];
        }

        ///<summary> Constructor. </summary>
        ///<param name="pParameterHandle"> the value of the parameterHandle field</param>
        ///<param name="pParameterValue"> the value of the parameterValue field</param>
        public HLAparameterHandleValuePair(long pParameterHandle, byte[] pParameterValue)
        {
            this.parameterHandle = pParameterHandle;
            this.parameterValue = pParameterValue;
        }

        ///<summary> Copy constructor. </summary>
        ///<param name="other"> the other HLAparameterHandleValuePair to copy</param>
        public HLAparameterHandleValuePair(HLAparameterHandleValuePair other)
        {
            this.parameterHandle = other.parameterHandle;
            this.parameterValue = other.parameterValue;
        }

        ///<summary> Returns a string representation of this HLAparameterHandleValuePair. </summary>
        ///<returns> a string representation of this HLAparameterHandleValuePair</returns>
        public override String ToString()
        {
            return "HLAparameterHandleValuePair(" +
                     "parameterHandle: " + parameterHandle + ", " +
                     "parameterValue: " + parameterValue +
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the parameterHandle field.
        ///</summary>
        public long ParameterHandle
        {
            set { parameterHandle = value; }
            get { return parameterHandle; }
        }


        ///<summary>
        /// Gets/Sets the value of the parameterValue field.
        ///</summary>
        public byte[] ParameterValue
        {
            set { parameterValue = value; }
            get { return parameterValue; }
        }


        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new HLAparameterHandleValuePair(this);
        }

        #endregion
    }

    ///<summary>
    ///Serializes and deserializes HLAparameterHandleValuePair objects into and
    ///from HLA formats. 
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler from BootstrapObjectModel.xml </author>
    public sealed class HLAparameterHandleValuePairXrtiSerializer
    {
        ///<summary>
        /// Writes this HLAparameterHandleValuePair to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static void Serialize(HlaEncodingWriter writer, HLAparameterHandleValuePair obj)
        {
            writer.WriteHLAinteger64BE(obj.ParameterHandle);
            writer.WriteHLAopaqueData(obj.ParameterValue);
        }


        ///<summary>
        /// Reads and returns a HLAparameterHandleValuePair from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static HLAparameterHandleValuePair Deserialize(HlaEncodingReader reader)
        {
            HLAparameterHandleValuePair decodedValue = new HLAparameterHandleValuePair();

            decodedValue.ParameterHandle = reader.ReadHLAinteger64BE();
            decodedValue.ParameterValue = reader.ReadHLAopaqueData();
            return decodedValue;
        }

    }

}
