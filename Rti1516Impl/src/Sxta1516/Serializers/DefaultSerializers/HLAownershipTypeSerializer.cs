namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    [Serializable]
    public sealed class HLAownershipTypeXrtiSerializer
    {
        ///<summary> Reads and returns a HLAownershipType from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns>return the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static HLAownershipType Deserialize(HlaEncodingReader reader)
        {
            return (HLAownershipType)reader.ReadHLAinteger32BE();
        }

        ///<summary>Writes this HLAownershipType to the specified stream.</summary>
        ///<param name="writer"> the stream to write to</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static void Serialize(HlaEncodingWriter writer, HLAownershipType val)
        {
            writer.WriteHLAinteger32BE((int)val);
        }
    }

}
