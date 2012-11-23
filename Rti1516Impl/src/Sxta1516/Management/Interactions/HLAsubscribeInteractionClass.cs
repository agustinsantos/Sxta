using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.XrtiHandles;

namespace Sxta.Rti1516.Management
{
    ///<summary>
    ///Message for HLAsubscribeInteractionClass iteraction : Set the joined federate's
    ///subscription status to an interaction class. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAsubscribeInteractionClass",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Subscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Set the joined federate's subscription status to an interaction class.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLAsubscribeInteractionClassMessage : HLAserviceMessage
    {
        IInteractionClassHandle HLAinteractionClass_;

        ///<summary>Interaction class to which the federate shall subscribe.</summary>
        [HLAInteractionParameter(Name = "HLAinteractionClass",
                      Semantics = "Interaction class to which the federate shall subscribe.",
                      DataType = "HLAhandle")]
        public IInteractionClassHandle HLAinteractionClass
        {
            get { return HLAinteractionClass_; }
            set { HLAinteractionClass_ = value; }
        }

        bool HLAactive_;

        ///<summary>Whether the subscription is active.</summary> 
        [HLAInteractionParameter(Name = "HLAactive",
                      Semantics = "Whether the subscription is active.",
                      DataType = "HLAboolean")]
        public bool HLAactive
        {
            get { return HLAactive_; }
            set { HLAactive_ = value; }
        }

        ///<summary> Returns a string representation of this HLAsubscribeInteractionClassMessage. </summary>
        ///<returns> a string representation of this HLAsubscribeInteractionClassMessage</returns>
        public override string ToString()
        {
            return "HLAsubscribeInteractionClassMessage(" + base.ToString()
                   + ", HLAinteractionClass: " + HLAinteractionClass
                   + ", HLAactive: " + HLAactive + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAsubscribeInteractionClassMessage. 
    ///</summary>
    public class HLAsubscribeInteractionClassMessageXrtiSerializer : HLAserviceMessageXrtiSerializer
    {
        IInteractionClassHandleFactory interactionClassHandleFactory;

        ///<summary> Constructor </summary>
        public HLAsubscribeInteractionClassMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
            // TODO ANGEL: Es apropiado que conozca la instancia concreta a este nivel o se debería pasar en el constructor la referencia
            interactionClassHandleFactory = new XRTIInteractionClassHandleFactory();
        }

        ///<summary> Writes this HLAsubscribeInteractionClassMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                IInteractionClassHandle interactionClassHandle = (((HLAsubscribeInteractionClassMessage)obj).HLAinteractionClass);
                byte[] interactionClassHandleByteArray = new byte[interactionClassHandle.EncodedLength()];
                interactionClassHandle.Encode(interactionClassHandleByteArray, 0);

                writer.WriteHLAopaqueData(interactionClassHandleByteArray);

                writer.WriteHLAboolean(((HLAsubscribeInteractionClassMessage)obj).HLAactive);
                
                /*
                writer.WriteHLAinteger32BE((((HLAsubscribeInteractionClassMessage)obj).HLAinteractionClass).Length);

                for (int i = 0; i < (((HLAsubscribeInteractionClassMessage)obj).HLAinteractionClass).Length; i++)
                {
                    writer.WriteHLAoctet((((HLAsubscribeInteractionClassMessage)obj).HLAinteractionClass)[i]);
                }
                writer.WriteHLAboolean(((HLAsubscribeInteractionClassMessage)obj).HLAactive);
                */
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAsubscribeInteractionClassMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAsubscribeInteractionClassMessage decodedValue;
            if (!(msg is HLAsubscribeInteractionClassMessage))
            {
                decodedValue = new HLAsubscribeInteractionClassMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAsubscribeInteractionClassMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAsubscribeInteractionClassMessage;
            try
            {
                byte[] interactionClassHandleByteArray = reader.ReadHLAopaqueData();
                decodedValue.HLAinteractionClass = interactionClassHandleFactory.Decode(interactionClassHandleByteArray, 0);

                decodedValue.HLAactive = reader.ReadHLAboolean();

                /*
                decodedValue.HLAinteractionClass = new byte[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < decodedValue.HLAinteractionClass.Length; i++)
                {
                    decodedValue.HLAinteractionClass[i] = reader.ReadHLAoctet();
                }
                decodedValue.HLAactive = reader.ReadHLAboolean();
                */
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }

}
