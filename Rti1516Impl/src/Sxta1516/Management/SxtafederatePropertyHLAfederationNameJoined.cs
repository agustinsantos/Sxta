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
    ///Autogenerated Serializer Helper. Serializes and deserializes HLAfederate.HLAfederationNameJoined
    ///parameters into and from HLA formats. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Management Object Model </author>
    public class SxtafederatePropertyHLAfederationNameJoinedXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of HLAfederate.HLAfederationNameJoined property.
        /// </summary>
        public SxtafederatePropertyHLAfederationNameJoinedXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary>
        /// Writes this HLAfederate.HLAfederationNameJoined to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="HLAfederationName"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object HLAfederationName)
        {
            try
            {
                writer.WriteHLAunicodeString((String)HLAfederationName);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        ///<summary>
        /// Reads and returns a HLAfederate.HLAfederationNameJoined from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<param name="dummy"> this parameter is not used</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            String decodedValue;
            try
            {
                decodedValue = reader.ReadHLAunicodeString();
                return decodedValue;
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }
}