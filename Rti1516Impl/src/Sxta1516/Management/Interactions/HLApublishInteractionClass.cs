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
    ///Message for HLApublishInteractionClass iteraction : Set the joined federate's
    ///publication status of an interaction class. 
    ///</summary>
    [HLAInteractionClass(Name = "HLApublishInteractionClass",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Subscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Set the joined federate's publication status of an interaction class.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLApublishInteractionClassMessage : HLAserviceMessage
    {
        IInteractionClassHandle HLAinteractionClass_;

        ///<summary>Interaction class that the joined federate shall publish.</summary>
        [HLAInteractionParameter(Name = "HLAinteractionClass",
                      Semantics = "Interaction class that the joined federate shall publish.",
                      DataType = "HLAhandle")]
        public IInteractionClassHandle HLAinteractionClass
        {
            get { return HLAinteractionClass_; }
            set { HLAinteractionClass_ = value; }
        }

        ///<summary> Returns a string representation of this HLApublishInteractionClassMessage. </summary>
        ///<returns> a string representation of this HLApublishInteractionClassMessage</returns>
        public override string ToString()
        {
            return "HLApublishInteractionClassMessage(" + base.ToString()
                   + ", HLAinteractionClass: " + HLAinteractionClass + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLApublishInteractionClassMessage. 
    ///</summary>
    public class HLApublishInteractionClassMessageXrtiSerializer : HLAserviceMessageXrtiSerializer
    {
        IInteractionClassHandleFactory interactionClassHandleFactory;

        ///<summary> Constructor </summary>
        public HLApublishInteractionClassMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
            // TODO ANGEL: Es apropiado que conozca la instancia concreta a este nivel o se debería pasar en el constructor la referencia
            interactionClassHandleFactory = new XRTIInteractionClassHandleFactory();
        }

        ///<summary> Writes this HLApublishInteractionClassMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                IInteractionClassHandle interactionClassHandle = (((HLApublishInteractionClassMessage)obj).HLAinteractionClass);
                byte[] interactionClassHandleByteArray = new byte[interactionClassHandle.EncodedLength()];
                interactionClassHandle.Encode(interactionClassHandleByteArray, 0);

                writer.WriteHLAopaqueData(interactionClassHandleByteArray);
                /*
                writer.WriteHLAinteger32BE((((HLApublishInteractionClassMessage)obj).HLAinteractionClass).Length);

                for (int i = 0; i < (((HLApublishInteractionClassMessage)obj).HLAinteractionClass).Length; i++)
                {
                    writer.WriteHLAoctet((((HLApublishInteractionClassMessage)obj).HLAinteractionClass)[i]);
                }
                */
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLApublishInteractionClassMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLApublishInteractionClassMessage decodedValue;
            if (!(msg is HLApublishInteractionClassMessage))
            {
                decodedValue = new HLApublishInteractionClassMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLApublishInteractionClassMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLApublishInteractionClassMessage;
            try
            {
                byte[] interactionClassHandleByteArray = reader.ReadHLAopaqueData();
                decodedValue.HLAinteractionClass = interactionClassHandleFactory.Decode(interactionClassHandleByteArray, 0);

                /*
                decodedValue.HLAinteractionClass = new byte[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < decodedValue.HLAinteractionClass.Length; i++)
                {
                    decodedValue.HLAinteractionClass[i] = reader.ReadHLAoctet();
                }
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
