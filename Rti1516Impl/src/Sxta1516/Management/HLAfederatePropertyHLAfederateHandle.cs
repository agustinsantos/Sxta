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
    ///Autogenerated Serializer Helper. Serializes and deserializes HLAfederate.HLAfederateHandle
    ///parameters into and from HLA formats. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Management Object Model </author>
    public class HLAfederatePropertyHLAfederateHandleXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of HLAfederate.HLAfederateHandle property.
        /// </summary>
        public HLAfederatePropertyHLAfederateHandleXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary>
        /// Writes this HLAfederate.HLAfederateHandle to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="HLAfederateHandle"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object HLAfederateHandle)
        {
            // PATCH ANGEL: Es necesario realizar un cast del object primero a HLAfederateHandle y luego a byte[] para que funcione correctamente (1� Linea de c�digo)
            // Se deber�a incluir esta modificaci�n en el Dynamic Compiler (Lo comentado es lo que generaba).
            try
            {
                byte[] handle = ((byte[])((HLAfederateHandle)HLAfederateHandle));  
                writer.WriteHLAinteger32BE(handle.Length);                      // Como argumento: ((byte[])HLAfederateHandle).Length

                for (int i = 0; i < handle.Length; i++)                         // Condici�n del for: i < ((byte[])HLAfederateHandle).Length
                {
                    writer.WriteHLAoctet(handle[i]);                            // Como argumento: ((byte[])HLAfederateHandle)[i]
                }
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        ///<summary>
        /// Reads and returns a HLAfederate.HLAfederateHandle from the specified stream.
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
                decodedValue = new byte[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < decodedValue.Length; i++)
                {
                    decodedValue[i] = reader.ReadHLAoctet();
                }
                return (HLAfederateHandle)decodedValue;
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }
}
