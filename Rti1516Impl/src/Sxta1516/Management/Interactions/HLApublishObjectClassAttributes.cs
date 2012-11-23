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
    ///Message for HLApublishObjectClassAttributes iteraction : Set the joined federate's
    ///publication status of attributes belonging to an object class. 
    ///</summary>
    [HLAInteractionClass(Name = "HLApublishObjectClassAttributes",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Subscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Set the joined federate's publication status of attributes belonging to an object class.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLApublishObjectClassAttributesMessage : HLAserviceMessage
    {
        IObjectClassHandle HLAobjectClass_;

        ///<summary>Object class for which the joined federate's publication shall
        ///change.</summary> 
        [HLAInteractionParameter(Name = "HLAobjectClass",
                      Semantics = "Object class for which the joined federate's publication shall change.",
                      DataType = "HLAhandle")]
        public IObjectClassHandle HLAobjectClass
        {
            get { return HLAobjectClass_; }
            set { HLAobjectClass_ = value; }
        }

        IAttributeHandleSet HLAattributeList_;

        ///<summary>List of handles of attributes of HLAobjectClass, which the federate
        ///shall now publish.</summary> 
        [HLAInteractionParameter(Name = "HLAattributeList",
                      Semantics = "List of handles of attributes of HLAobjectClass, which the federate shall now publish.",
                      DataType = "HLAhandleList")]
        public IAttributeHandleSet HLAattributeList
        {
            get { return HLAattributeList_; }
            set { HLAattributeList_ = value; }
        }

        ///<summary> Returns a string representation of this HLApublishObjectClassAttributesMessage. </summary>
        ///<returns> a string representation of this HLApublishObjectClassAttributesMessage</returns>
        public override string ToString()
        {
            return "HLApublishObjectClassAttributesMessage(" + base.ToString()
                   + ", HLAobjectClass: " + HLAobjectClass
                   + ", HLAattributeList: " + HLAattributeList + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLApublishObjectClassAttributesMessage. 
    ///</summary>
    public class HLApublishObjectClassAttributesMessageXrtiSerializer : HLAserviceMessageXrtiSerializer
    {
        private AttributeHandleSetFactory attributeHandleSetFactory;
        private IAttributeHandleFactory attributeHandleFactory;
        private IObjectClassHandleFactory objectClassFactory;

        ///<summary> Constructor </summary>
        public HLApublishObjectClassAttributesMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
            // TODO ANGEL: Es apropiado que conozca la instancia concreta a este nivel o se debería pasar en el constructor la referencia
            attributeHandleSetFactory = new XRTIAttributeHandleSetFactory();
            attributeHandleFactory = new XRTIAttributeHandleFactory();
            objectClassFactory = new XRTIObjectClassHandleFactory();
        }

        ///<summary> Writes this HLApublishObjectClassAttributesMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                base.Serialize(writer, obj);

                IObjectClassHandle objectClass = ((HLApublishObjectClassAttributesMessage)obj).HLAobjectClass;
                byte[] objectClassByteArray = new byte[objectClass.EncodedLength()];
                objectClass.Encode(objectClassByteArray, 0);

                writer.WriteHLAopaqueData(objectClassByteArray);

                IAttributeHandleSet attributeHandleSet = ((HLApublishObjectClassAttributesMessage)obj).HLAattributeList;
                writer.WriteHLAinteger32BE(attributeHandleSet.Count);

                foreach (IAttributeHandle attributeHandle in attributeHandleSet)
                {
                    byte[] attributeHandleByteArray = new byte[attributeHandle.EncodedLength()];
                    attributeHandle.Encode(attributeHandleByteArray, 0);

                    writer.WriteHLAopaqueData(attributeHandleByteArray);
                }

                /*
                writer.WriteHLAinteger32BE((((HLApublishObjectClassAttributesMessage)obj).HLAobjectClass).Length);

                for (int i = 0; i < (((HLApublishObjectClassAttributesMessage)obj).HLAobjectClass).Length; i++)
                {
                    writer.WriteHLAoctet((((HLApublishObjectClassAttributesMessage)obj).HLAobjectClass)[i]);
                }
                writer.WriteHLAinteger32BE((((HLApublishObjectClassAttributesMessage)obj).HLAattributeList).Length);

                for (int i = 0; i < (((HLApublishObjectClassAttributesMessage)obj).HLAattributeList).Length; i++)
                {
                    writer.WriteHLAinteger32BE(((((HLApublishObjectClassAttributesMessage)obj).HLAattributeList)[i]).Length);

                    for (int j = 0; j < ((((HLApublishObjectClassAttributesMessage)obj).HLAattributeList)[i]).Length; j++)
                    {
                        writer.WriteHLAoctet(((((HLApublishObjectClassAttributesMessage)obj).HLAattributeList)[i])[j]);
                    }
                }
                */
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this HLApublishObjectClassAttributesMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLApublishObjectClassAttributesMessage decodedValue;
            if (!(msg is HLApublishObjectClassAttributesMessage))
            {
                decodedValue = new HLApublishObjectClassAttributesMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLApublishObjectClassAttributesMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLApublishObjectClassAttributesMessage;
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
