using System;
using System.IO;
using System.Collections.Generic;

using Hla.Rti1516;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;

namespace ExternalSamples
{

    ///<summary>
    ///Serializes and deserializes ExternalCountry.Name parameters into and from HLA formats.
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler</author>
    public class ExternalCountryPropertyNameXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public ExternalCountryPropertyNameXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }
        ///<summary>
        /// Writes this ExternalCountry.Name to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object name)
        {
            try
            {
                writer.WriteHLAunicodeString((String)name);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            ;
        }


        ///<summary>
        /// Reads and returns a HLAalternative from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
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

    ///<summary>
    ///Serializes and deserializes ExternalCountry.Population parameters into and from HLA formats.
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler</author>
    public class ExternalCountryPropertyPopulationXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public ExternalCountryPropertyPopulationXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }


        ///<summary>
        /// Writes this ExternalCountry.Population to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object population)
        {
            try
            {
                writer.WriteHLAfloat64BE((double)population);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }


        ///<summary>
        /// Reads and returns a Population from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            double decodedValue;
            try
            {
                decodedValue = reader.ReadHLAfloat64BE();
                return decodedValue;
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }

        }
    }

    ///<summary>
    ///Serializes and deserializes Vector3FloatStruct objects into and from HLA
    ///formats. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Hello World Object Model </author>
    public sealed class Vector3FloatStructXrtiSerializer
    {
        ///<summary>
        /// Writes this Vector3FloatStruct to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static void Serialize(HlaEncodingWriter writer, Vector3FloatStruct obj)
        {
            writer.WriteHLAfloat32BE(obj.XComponent);
            writer.WriteHLAfloat32BE(obj.YComponent);
            writer.WriteHLAfloat32BE(obj.ZComponent);
        }


        ///<summary>
        /// Reads and returns a Vector3FloatStruct from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="IOException"> if an error occurs</exception>
        public static Vector3FloatStruct Deserialize(HlaEncodingReader reader)
        {
            Vector3FloatStruct decodedValue = new Vector3FloatStruct();

            decodedValue.XComponent = reader.ReadHLAfloat32BE();
            decodedValue.YComponent = reader.ReadHLAfloat32BE();
            decodedValue.ZComponent = reader.ReadHLAfloat32BE();
            return decodedValue;
        }

    }

    ///<summary>
    ///Autogenerated Serializer Helper. Serializes and deserializes Country.Position
    ///parameters into and from HLA formats. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Hello World Object Model </author>
    public class ExternalCountryPropertyPositionXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of ExternalCountry.Position property.
        /// </summary>
        public ExternalCountryPropertyPositionXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary>
        /// Writes this ExternalCountry.Position to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="Position"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object Position)
        {
            try
            {
                Vector3FloatStructXrtiSerializer.Serialize(writer, (Vector3FloatStruct)Position);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        ///<summary>
        /// Reads and returns a Country.Position from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<param name="dummy"> this parameter is not used</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            Vector3FloatStruct decodedValue;
            try
            {
                decodedValue = Vector3FloatStructXrtiSerializer.Deserialize(reader);
                return decodedValue;
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }

    ///<summary>
    ///A HLA serializer for CommunicationMessage. 
    ///</summary>
    public class CommunicationMessageXrtiSerializer : HLAinteractionRootMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public CommunicationMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this CommunicationMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                //base.Serialize(writer, obj);
                writer.WriteHLAunicodeString(((CommunicationMessage)obj).Message);
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this CommunicationMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            CommunicationMessage decodedValue;
            if (!(msg is CommunicationMessage))
            {
                decodedValue = new CommunicationMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as CommunicationMessage;
            }
            //object tmp = decodedValue;
            //decodedValue = base.Deserialize(reader, ref tmp) as CommunicationMessage;
            try
            {
                decodedValue.Message = reader.ReadHLAunicodeString();
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}