namespace Sxta.Rti1516.Management
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    ///<summary>
    ///Autogenerated Serializer Helper. Serializes and deserializes HLAfederation.HLAlastSaveTime
    ///parameters into and from HLA formats. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Management Object Model </author>
    public class HLAfederationPropertyHLAlastSaveTimeXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of HLAfederation.HLAlastSaveTime property.
        /// </summary>
        public HLAfederationPropertyHLAlastSaveTimeXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary>
        /// Writes this HLAfederation.HLAlastSaveTime to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="HLAlastSaveTime"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object HLAlastSaveTime)
        {
            try
            {
                writer.WriteHLAinteger32BE(((byte[])HLAlastSaveTime).Length);

                for(int i=0;i< ((byte[])HLAlastSaveTime).Length;i++)
                {
                    writer.WriteHLAoctet(((byte[])HLAlastSaveTime)[i]);
                }
            }
            catch(IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        ///<summary>
        /// Reads and returns a HLAfederation.HLAlastSaveTime from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<param name="dummy"> this parameter is not used</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            byte[] decodedValue;
            try
            {
                decodedValue = new byte[ reader.ReadHLAinteger32BE() ];

                for(int i=0;i<decodedValue.Length;i++)
                {
                    decodedValue[i] = reader.ReadHLAoctet();
                }
                return decodedValue;
            }
            catch(IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }
}
