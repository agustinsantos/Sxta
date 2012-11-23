using System;
using System.IO;

namespace Sxta.Rti1516.Serializers.XrtiEncoding
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHlaEncodingSerializer
    {
        /// <summary>
        /// Deserialize data from the specified stream, rebuilding
        /// the object hierarchy
        /// </summary>
        /// <param name="serializationStream">the serialization stream</param>
        /// <param name="obj"> the object to deserialized is provided</param>
        /// <returns>the new deserialized object </returns>
        object Deserialize(Stream serializationStream, ref Object obj);
        
        /// <summary>
        /// Deserialize data from the specified stream, rebuilding
        /// the object hierarchy
        /// </summary>
        /// <param name="reader">the reader</param>
        /// <param name="obj"> the object to deserialized is provided</param>
        /// <returns>the new deserialized object </returns>
        object Deserialize(HlaEncodingReader reader, ref Object obj);

        /// <summary>
        /// Serialize the specified object to the specified stream.
        /// </summary>
        /// <param name="serializationStream">the serialization stream</param>
        /// <param name="obj">the object to serialize</param>
        void Serialize(Stream serializationStream, object obj);

        /// <summary>
        /// Serialize the specified object to the specified stream.
        /// </summary>
        /// <param name="writer">a encoding writer</param>
        /// <param name="obj">the object to serialize</param>
        void Serialize(HlaEncodingWriter writer, object obj);
    }
}
