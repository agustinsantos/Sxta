namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    [Serializable]
    public sealed class HLAorderTypeXrtiSerializer
    {
        ///<summary> Reads and returns a HLAorderType from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns>return the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static HLAorderType Deserialize(HlaEncodingReader reader)
        {
            return (HLAorderType)reader.ReadHLAinteger32BE();
        }

        ///<summary>Writes this HLAorderType to the specified stream.</summary>
        ///<param name="writer"> the stream to write to</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static void Serialize(HlaEncodingWriter writer, HLAorderType val)
        {
            writer.WriteHLAinteger32BE((int)val);
        }

    }
}
