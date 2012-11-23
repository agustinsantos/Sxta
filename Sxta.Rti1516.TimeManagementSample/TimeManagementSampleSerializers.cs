namespace Sxta.Rti1516.TimeManagementSample
{
    using System;
    using System.IO;

    using Hla.Rti1516;

    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.BoostrapProtocol;
    using Sxta.Rti1516.XrtiHandles;

    ///<summary>
    ///Autogenerated Serializer Helper. Serializes and deserializes Home.PosX
    ///parameters into and from HLA formats. 
    ///</summary>
    /// <author> Sxta.Rti1516.DynamicCompiler.DynamicCompiler from Time Management Object Model </author>
    public class IntegerPositionXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of Home.PosX property.
        /// </summary>
        public IntegerPositionXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary>
        /// Writes this Home.PosX to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="PosX"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object PosX)
        {
            try
            {
                writer.WriteHLAinteger32BE((int)PosX);
            }
            catch(IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        ///<summary>
        /// Reads and returns a Home.PosX from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<param name="dummy"> this parameter is not used</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            int decodedValue;
            try
            {
                decodedValue = reader.ReadHLAinteger32BE();
                return decodedValue;
            }
            catch(IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }

    ///<summary>
    ///Autogenerated Serializer Helper. Serializes and deserializes Home.BoxesCount
    ///parameters into and from HLA formats. 
    ///</summary>
    /// <author> Sxta.Rti1516.DynamicCompiler.DynamicCompiler from Time Management Object Model </author>
    public class HomePropertyBoxesCountXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary>Constructor for the serializer of Home.BoxesCount property.
        /// </summary>
        public HomePropertyBoxesCountXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary>
        /// Writes this Home.BoxesCount to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="BoxesCount"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object BoxesCount)
        {
            try
            {
                writer.WriteHLAinteger32BE((int)BoxesCount);
            }
            catch(IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        ///<summary>
        /// Reads and returns a Home.BoxesCount from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<param name="dummy"> this parameter is not used</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            int decodedValue;
            try
            {
                decodedValue = reader.ReadHLAinteger32BE();
                return decodedValue;
            }
            catch(IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }

    ///<summary>
    ///A HLA serializer for HLAinteractionRootMessage. 
    ///</summary>
    public class HLAinteractionRootMessageXrtiSerializer: BaseInteractionMessageXrtiSerializer

    {
        ///<summary> Constructor </summary>
        public HLAinteractionRootMessageXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary> Writes this HLAinteractionRootMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
        }

        ///<summary> Reads this HLAinteractionRootMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAinteractionRootMessage decodedValue;
            if (!(msg is HLAinteractionRootMessage))
            {
                decodedValue = new HLAinteractionRootMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAinteractionRootMessage;
            }

            return decodedValue;
        }
    }

    ///<summary>
    ///A HLA serializer for BoxInHouseMessage. 
    ///</summary>
    public class BoxInHouseMessageXrtiSerializer : HLAinteractionRootMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public BoxInHouseMessageXrtiSerializer(XrtiSerializerManager manager)
        : base(manager)
        {
        }

        ///<summary> Writes this BoxInHouseMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                byte[] logicalTimeBytesArray = ((BoxInHouseMessage)obj).Time;
                writer.WriteHLAopaqueData(logicalTimeBytesArray);
            }
            catch(System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this BoxInHouseMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            BoxInHouseMessage decodedValue;
            if (!(msg is BoxInHouseMessage))
            {
                decodedValue = new BoxInHouseMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as BoxInHouseMessage;
            }
            object tmp = decodedValue;
            
            try
            {
                decodedValue = base.Deserialize(reader, ref tmp) as BoxInHouseMessage;
                decodedValue.Time = reader.ReadHLAopaqueData();
            }
            catch(System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }

    public class ActorPropertyDirectionXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        public ActorPropertyDirectionXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        public override void Serialize(HlaEncodingWriter writer, object moveDirection)
        {
            try
            {
                MoveDirectionXrtiSerializer.Serialize(writer, (Actor.MoveDirection)moveDirection);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }

        }

        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            Actor.MoveDirection decodedValue;
            try
            {
                decodedValue = MoveDirectionXrtiSerializer.Deserialize(reader);
                return decodedValue;
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }

    public sealed class MoveDirectionXrtiSerializer
    {
        public static Actor.MoveDirection Deserialize(HlaEncodingReader reader)
        {
            return (Actor.MoveDirection)reader.ReadHLAinteger32BE();
        }

        public static void Serialize(HlaEncodingWriter writer, Actor.MoveDirection val)
        {
            writer.WriteHLAinteger32BE((int)val);
        }
    }

    ///<summary>
    ///Autogenerated interaction listener interface. 
    ///</summary>
    /// <author> Sxta.Rti1516.DynamicCompiler.DynamicCompiler from Time Management Object Model </author>
    public interface ITimeManagementObjectModelInteractionListener : IInteractionListener
    {
        ///<summary>
        ///Notifies that the box is already in its house 
        ///</summary>
        ///<param name="msg"> the message associated with the interaction</param>
        ///<exception cref="InteractionClassNotRecognized"> if the interaction class was not recognized</exception>
        ///<exception cref="InteractionParameterNotRecognized"> if a parameter of the interaction was not
        /// recognized</exception>
        ///<exception cref="InteractionClassNotSubscribed"> if the federate had not subscribed to the
        /// interaction class</exception>
        ///<exception cref="FederateInternalError"> if an error occurs in the federate</exception>
        void OnReceiveBoxInHouse(BoxInHouseMessage msg);

    }

    ///<summary>
    ///Autogenerated interaction and serializer registration Helper. 
    ///</summary>
    /// <author> Sxta.Rti1516.DynamicCompiler.DynamicCompiler from Time Management Object Model </author>
    [HLAinteractionHelperAttribute(Name = "TimeManagementObjectModelInteractionHelper", FomName = "Time Management Object Model", Semantics = "A Listener Manager and serializer manager")]
    public class TimeManagementObjectModelInteractionHelper
    {
        InteractionManager manager;

        /// <summary>Constructor.</summary>
        /// <param name="interactionManager"> the run-time interaction manager</param>
        public TimeManagementObjectModelInteractionHelper(InteractionManager interactionManager)
        {
            Type objType;
            manager = interactionManager;
            XrtiSerializerManager serializerMngr = manager.SerializerManager;
            long handle;
            ObjectClassDescriptor ocd;

            // Home
            ocd = manager.DescriptorManager.GetObjectClassDescriptor("Home");
            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("PosX").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new IntegerPositionXrtiSerializer(serializerMngr));

            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("PosY").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new IntegerPositionXrtiSerializer(serializerMngr));

            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("BoxesCount").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new HomePropertyBoxesCountXrtiSerializer(serializerMngr));

            // Actor
            ocd = manager.DescriptorManager.GetObjectClassDescriptor("Actor");
            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("PosX").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new IntegerPositionXrtiSerializer(serializerMngr));

            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("PosY").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new IntegerPositionXrtiSerializer(serializerMngr));

            handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor("Direction").Handle).Identifier;
            serializerMngr.RegisterSerializer(null, handle, new ActorPropertyDirectionXrtiSerializer(serializerMngr));

            // Interactions
            objType = typeof(HLAinteractionRootMessage);
            manager.AddReceiveInteractionDelegate(objType, "HLAinteractionRoot", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("HLAinteractionRoot").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new HLAinteractionRootMessageXrtiSerializer(serializerMngr));

            objType = typeof(BoxInHouseMessage);
            manager.AddReceiveInteractionDelegate(objType, "BoxInHouse", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("BoxInHouse").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new BoxInHouseMessageXrtiSerializer(serializerMngr));
        }

        /// <summary>Notifies the listener of a received interaction.</summary>
        /// <param name="msg"> the message of the received interaction</param>
        public void ReceiveInteraction(BaseInteractionMessage msg)
        {
            try
            {
                if(msg is BoxInHouseMessage)
                {
                    foreach(IInteractionListener il in manager.InteractionListeners)
                    {
                        if (il is ITimeManagementObjectModelInteractionListener)
                            (il as ITimeManagementObjectModelInteractionListener).OnReceiveBoxInHouse(msg as BoxInHouseMessage);
                        else
                            il.ReceiveInteraction(msg);
                    }
                }
                else
                    foreach (IInteractionListener il in manager.InteractionListeners)
                    {
                        il.ReceiveInteraction(msg);
                    }
            }
            catch(System.IO.IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }

        }
    }
}
