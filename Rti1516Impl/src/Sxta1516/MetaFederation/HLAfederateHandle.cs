namespace Sxta.Rti1516.Management
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;


    ///<summary>
    ///The type of the argument to Normalize Federate Handle service. This is
    ///a pointer to an RTI-defined programming language object, not an integer
    ///32. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Management Object Model </author>
    [HLASimpleData(Name = "HLAfederateHandle",
                   Representation = "HLAinteger32BE",
                   Semantics = "The type of the argument to Normalize Federate Handle service. This is a pointer to an RTI-defined programming language object, not an integer 32.")]
    public struct HLAfederateHandle
    {
        public int data;

        public static implicit operator int(HLAfederateHandle val)
        {
            return val.data;
        }

        public static implicit operator HLAfederateHandle(int val)
        {
            HLAfederateHandle tmp = new HLAfederateHandle();
            tmp.data = val;
            return tmp;
        }

        public static explicit operator byte[](HLAfederateHandle val)
        {
            byte[] tmp = BitConverter.GetBytes(((HLAfederateHandle)val).data);
            return tmp;
        }

        public static explicit operator HLAfederateHandle(byte[] val)
        {
            int value = BitConverter.ToInt32(val, 0);
            HLAfederateHandle tmp = new HLAfederateHandle();
            tmp.data = value;
            return tmp;
        }

        public override string ToString()
        {
            return "HLAfederateHandle(data = " + data + " )";
        }
    }


    public class HLAhandleList : List<HLAfederateHandle>
    {
        //TODO ANGEL. Los array se traducen de dos formas diferentes en este momento
        // como byte[] y como List. el primero automaticamente y el segundo a mano
        // es interesante buscar una forma de solucionar este tema
        // Ademas el sistema no intercepta los cambios en el array y por lo tanto
        // no propaga los cambios.
    }


    [Serializable]
    public sealed class HLAfederateHandleXrtiSerializer
    {
        ///<summary> Reads and returns a HLAfederateHandle from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns>return the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static HLAfederateHandle Deserialize(HlaEncodingReader reader)
        {
            return (HLAfederateHandle)reader.ReadHLAinteger32BE();
        }

        ///<summary>Writes this HLAfederateHandle to the specified stream.</summary>
        ///<param name="writer"> the stream to write to</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static void Serialize(HlaEncodingWriter writer, HLAfederateHandle val)
        {
            writer.WriteHLAinteger32BE((int)val);
        }

    }
}
