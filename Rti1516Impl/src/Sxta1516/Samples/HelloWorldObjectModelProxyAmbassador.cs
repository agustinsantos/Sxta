using System;
using System.IO;
using System.Collections.Generic;

using Hla.Rti1516;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;

namespace Sxta.Rti1516.Samples
{

    ///<summary>
    ///Serializes and deserializes Country.Name parameters into and from HLA formats.
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler</author>
    public class CountryPropertyNameXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public CountryPropertyNameXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }
        ///<summary>
        /// Writes this Country.Name to the specified stream.
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
    ///Serializes and deserializes Country.Population parameters into and from HLA formats.
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler</author>
    public class CountryPropertyPopulationXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public CountryPropertyPopulationXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }


        ///<summary>
        /// Writes this Country.Population to the specified stream.
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
    public class CountryPropertyPositionXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of Country.Position property.
        /// </summary>
        public CountryPropertyPositionXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary>
        /// Writes this Country.Position to the specified stream.
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
    /*
    // PATCH ANGEL: [HLAinteractionListenerAttribute(Name = "HelloWorldListenerManager", Semantics = "A HelloWorld Listener Manager")]
    public class HelloWorldObjectListener__Generated
    {
        InteractionManager manager;
        public HelloWorldObjectListener__Generated(InteractionManager interactionManager)
        {
            Type msgType;
            manager = interactionManager;
            XrtiSerializerManager serializerMngr = manager.SerializerManager;
            long handle;
            ObjectClassDescriptor ocd = manager.DescriptorManager.GetObjectClassDescriptor("Country");

            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("Name").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new CountryPropertyNameXrtiSerializer(serializerMngr));

            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("Population").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new CountryPropertyPopulationXrtiSerializer(serializerMngr));

            msgType = typeof(CommunicationMessage);
            manager.AddReceiveInteractionDelegate(msgType, "Communication", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("Communication").Handle).Identifier;
            serializerMngr.RegisterSerializer(msgType, handle, new CommunicationMessageXrtiSerializer(serializerMngr));

            msgType = typeof(CommunicationMessage);
            manager.AddReceiveInteractionDelegate(msgType, "Communication", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("Communication").Handle).Identifier;
            serializerMngr.RegisterSerializer(msgType, handle, new CommunicationMessageXrtiSerializer(serializerMngr));
        }

        public void ReceiveInteraction(BaseInteractionMessage msg)
        {
            try
            {
                lock (manager.interactionListeners)
                {
                    if (msg is CommunicationMessage)
                    {
                        foreach (IInteractionListener il in manager.interactionListeners)
                        {
                            if (il is IHelloWorldObjectModelInteractionListener)
                                (il as IHelloWorldObjectModelInteractionListener).OnReceiveCommunication(msg as CommunicationMessage);
                            else
                                il.ReceiveInteraction(msg);
                        }
                    }
                    else
                        foreach (IInteractionListener il in manager.interactionListeners)
                        {
                            il.ReceiveInteraction(msg);
                        }
                }
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }
    */
}
