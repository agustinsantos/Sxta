using System;
using System.IO;
using System.Collections.Generic;

using Mogre;

using Hla.Rti1516;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;

namespace Sxta.Rti1516.BoidSample
{

    ///<summary>
    ///Serializes and deserializes Vector3 objects into and from HLA
    ///formats. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Boid Object Model </author>
    public sealed class Vector3XrtiSerializer
    {
        ///<summary>
        /// Writes this Vector3FloatStruct to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static void Serialize(HlaEncodingWriter writer, Vector3 obj)
        {
            writer.WriteHLAfloat32BE(obj.x);
            writer.WriteHLAfloat32BE(obj.y);
            writer.WriteHLAfloat32BE(obj.z);
        }


        ///<summary>
        /// Reads and returns a Vector3 from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static Vector3 Deserialize(HlaEncodingReader reader)
        {
            Vector3 decodedValue = new Vector3();

            decodedValue.x = reader.ReadHLAfloat32BE();
            decodedValue.y = reader.ReadHLAfloat32BE();
            decodedValue.z = reader.ReadHLAfloat32BE();
            return decodedValue;
        }

    }

}
