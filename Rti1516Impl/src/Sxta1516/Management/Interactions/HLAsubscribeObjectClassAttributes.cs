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
    ///Message for HLAsubscribeObjectClassAttributes iteraction : Set the joined
    ///federate's subscription status of attributes belonging to an object class.
    ///</summary>
    [HLAInteractionClass(Name = "HLAsubscribeObjectClassAttributes",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Subscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Set the joined federate's subscription status of attributes belonging to an object class.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLAsubscribeObjectClassAttributesMessage : HLAserviceMessage
    {
        IObjectClassHandle HLAobjectClass_;

        ///<summary>Object class for which the joined federate's subscription shall
        ///change.</summary> 
        [HLAInteractionParameter(Name = "HLAobjectClass",
                      Semantics = "Object class for which the joined federate's subscription shall change.",
                      DataType = "HLAhandle")]
        public IObjectClassHandle HLAobjectClass
        {
            get { return HLAobjectClass_; }
            set { HLAobjectClass_ = value; }
        }

        IAttributeHandleSet HLAattributeList_;

        ///<summary>List of handles of attributes of HLAobjectClass to which the
        ///joined federate shall now subscribe.</summary> 
        [HLAInteractionParameter(Name = "HLAattributeList",
                      Semantics = "List of handles of attributes of HLAobjectClass to which the joined federate shall now subscribe.",
                      DataType = "HLAhandleList")]
        public IAttributeHandleSet HLAattributeList
        {
            get { return HLAattributeList_; }
            set { HLAattributeList_ = value; }
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

        ///<summary> Returns a string representation of this HLAsubscribeObjectClassAttributesMessage. </summary>
        ///<returns> a string representation of this HLAsubscribeObjectClassAttributesMessage</returns>
        public override string ToString()
        {
            return "HLAsubscribeObjectClassAttributesMessage(" + base.ToString()
                   + ", HLAobjectClass: " + HLAobjectClass
                   + ", HLAattributeList: " + HLAattributeList
                   + ", HLAactive: " + HLAactive + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAsubscribeObjectClassAttributesMessage. 
    ///</summary>
    public class HLAsubscribeObjectClassAttributesMessageXrtiSerializer : HLAserviceMessageXrtiSerializer
    {
        private AttributeHandleSetFactory attributeHandleSetFactory;
        private IAttributeHandleFactory attributeHandleFactory;
        private IObjectClassHandleFactory objectClassFactory;

        ///<summary> Constructor </summary>
        public HLAsubscribeObjectClassAttributesMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
            // TODO ANGEL: Es apropiado que conozca la instancia concreta a este nivel o se debería pasar en el constructor la referencia
            attributeHandleSetFactory = new XRTIAttributeHandleSetFactory();
            attributeHandleFactory = new XRTIAttributeHandleFactory();
            objectClassFactory = new XRTIObjectClassHandleFactory();
        }

        ///<summary> Writes this HLAsubscribeObjectClassAttributesMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                IObjectClassHandle objectClass = ((HLAsubscribeObjectClassAttributesMessage)obj).HLAobjectClass;
                byte[] objectClassByteArray = new byte[objectClass.EncodedLength()];
                objectClass.Encode(objectClassByteArray, 0);

                writer.WriteHLAopaqueData(objectClassByteArray);

                IAttributeHandleSet attributeHandleSet = ((HLAsubscribeObjectClassAttributesMessage)obj).HLAattributeList;
                writer.WriteHLAinteger32BE(attributeHandleSet.Count);

                foreach (IAttributeHandle attributeHandle in attributeHandleSet)
                {
                    byte[] attributeHandleByteArray = new byte[attributeHandle.EncodedLength()];
                    attributeHandle.Encode(attributeHandleByteArray, 0);

                    writer.WriteHLAopaqueData(attributeHandleByteArray);
                }

                writer.WriteHLAboolean(((HLAsubscribeObjectClassAttributesMessage)obj).HLAactive);

                /*
                writer.WriteHLAinteger32BE((((HLAsubscribeObjectClassAttributesMessage)obj).HLAobjectClass).Length);

                for (int i = 0; i < (((HLAsubscribeObjectClassAttributesMessage)obj).HLAobjectClass).Length; i++)
                {
                    writer.WriteHLAoctet((((HLAsubscribeObjectClassAttributesMessage)obj).HLAobjectClass)[i]);
                }
                writer.WriteHLAinteger32BE((((HLAsubscribeObjectClassAttributesMessage)obj).HLAattributeList).Length);

                for (int i = 0; i < (((HLAsubscribeObjectClassAttributesMessage)obj).HLAattributeList).Length; i++)
                {
                    writer.WriteHLAinteger32BE(((((HLAsubscribeObjectClassAttributesMessage)obj).HLAattributeList)[i]).Length);

                    for (int j = 0; j < ((((HLAsubscribeObjectClassAttributesMessage)obj).HLAattributeList)[i]).Length; j++)
                    {
                        writer.WriteHLAoctet(((((HLAsubscribeObjectClassAttributesMessage)obj).HLAattributeList)[i])[j]);
                    }
                }
                writer.WriteHLAboolean(((HLAsubscribeObjectClassAttributesMessage)obj).HLAactive);
                */
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLAsubscribeObjectClassAttributesMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAsubscribeObjectClassAttributesMessage decodedValue;
            if (!(msg is HLAsubscribeObjectClassAttributesMessage))
            {
                decodedValue = new HLAsubscribeObjectClassAttributesMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAsubscribeObjectClassAttributesMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAsubscribeObjectClassAttributesMessage;
            try
            {
                byte[] objectClassByteArray = reader.ReadHLAopaqueData();
                decodedValue.HLAobjectClass = objectClassFactory.Decode(objectClassByteArray, 0);

                decodedValue.HLAattributeList = attributeHandleSetFactory.Create();

                int count = reader.ReadHLAinteger32BE();

                for (int i = 0; i < count; i++)
                {
                    IAttributeHandle attributeHandle = attributeHandleFactory.Decode(reader.ReadHLAopaqueData(), 0);
                    decodedValue.HLAattributeList.Add(attributeHandle);
                }

                decodedValue.HLAactive = reader.ReadHLAboolean();

                /*
                decodedValue.HLAobjectClass = new byte[reader.ReadHLAinteger32BE()];

                for (int i = 0; i < decodedValue.HLAobjectClass.Length; i++)
                {
                    decodedValue.HLAobjectClass[i] = reader.ReadHLAoctet();
                }
                decodedValue.HLAattributeList = new byte[reader.ReadHLAinteger32BE()][];

                for (int i = 0; i < decodedValue.HLAattributeList.Length; i++)
                {
                    decodedValue.HLAattributeList[i] = new byte[reader.ReadHLAinteger32BE()];

                    for (int j = 0; j < decodedValue.HLAattributeList[i].Length; j++)
                    {
                        decodedValue.HLAattributeList[i][j] = reader.ReadHLAoctet();
                    }
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
