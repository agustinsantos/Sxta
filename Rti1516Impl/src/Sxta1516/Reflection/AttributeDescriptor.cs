namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.Xml;
    using Hla.Rti1516;
    using IHLAattribute = Sxta.Rti1516.Reflection.IHLAattribute;
    using HLAorderType = Sxta.Rti1516.Reflection.HLAorderType;
    using HLAownershipType = Sxta.Rti1516.Reflection.HLAownershipType;
    using HLAsharingType = Sxta.Rti1516.Reflection.HLAsharingType;
    using HLAupdateType = Sxta.Rti1516.Reflection.HLAupdateType;
    using Sxta.Rti1516.XrtiHandles;

    /// <summary> 
    /// Describes an attribute.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class AttributeDescriptor 
    {
        /// <summary>
        /// Returns the name of this attribute.
        /// </summary>
        /// <returns> the name of this attribute
        /// </returns>
        virtual public System.String Name
        {
            get { return attribute.Name; }
        }

        /// <summary> 
        /// Returns the handle of this attribute.
        /// </summary>
        /// <returns> the handle of this attribute
        /// </returns>
        virtual public IAttributeHandle Handle
        {
            get { return handle; }
        }

        /// <summary> 
        /// Returns the dimensions associated with this attribute.
        /// </summary>
        /// <returns> the dimensions associated with this attribute
        /// </returns>
        virtual public IDimensionHandleSet Dimensions
        {
            get { return dimensions; }
        }

        /// <summary> 
        /// Gets/sets the transportation type of this attribute.
        /// </summary>
        virtual public TransportationType Transportation
        {
            get { return transportation; }
            set { transportation = value; }
        }

        /// <summary> 
        /// Gets/sets the order type of this attribute.
        /// </summary>
        virtual public OrderType Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary> 
        /// Gets/sets whether or not this attribute is published.
        /// </summary>
        virtual public bool Published
        {
            get { return published; }
            set { published = value; }
        }

        /// <summary> 
        /// Gets/Sets whether or not this attribute is subscribed.
        /// </summary>
        virtual public bool Subscribed
        {
            get { return subscribed; }
            set { subscribed = value; }
        }

        /// <summary> The descriptor manager responsible for this descriptor.</summary>
        private DescriptorManager descriptorManager;

        /// <summary> The handle of this attribute.</summary>
        private IAttributeHandle handle;

        public Sxta.Rti1516.Reflection.IHLAattribute attribute;

        /// <summary> The dimensions associated with this attribute.</summary>
        private IDimensionHandleSet dimensions;

        /// <summary> The transportation type of this attribute.</summary>
        private TransportationType transportation;

        /// <summary> The order type of this attribute.</summary>
        private OrderType order;

        /// <summary> Whether or not this attribute is published.</summary>
        private bool published;

        /// <summary> Whether or not this attribute is subscribed.</summary>
        private bool subscribed;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the attribute
        /// </param>
        /// <param name="pHandle">the handle of the attribute
        /// </param>
        /// <param name="pDimensions">the dimensions associated with the attribute
        /// </param>
        /// <param name="pTransportation">the transportation type of the attribute
        /// </param>
        /// <param name="pOrder">the order type of the attribute
        /// </param>
        public AttributeDescriptor(XmlElement attributeElement, IAttributeHandle pHandle, IDimensionHandleSet pDimensions, TransportationType pTransportation, OrderType pOrder)
        {
            attribute = new Sxta.Rti1516.Reflection.HLAattribute(attributeElement);
            handle = pHandle;
            dimensions = pDimensions;
            transportation = pTransportation;
            order = pOrder;
        }
                /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the attribute
        /// </param>
        /// <param name="pHandle">the handle of the attribute
        /// </param>
        /// <param name="pDimensions">the dimensions associated with the attribute
        /// </param>
        /// <param name="pTransportation">the transportation type of the attribute
        /// </param>
        /// <param name="pOrder">the order type of the attribute
        /// </param>
        public AttributeDescriptor(Sxta.Rti1516.Reflection.HLAattribute attributeInfo, IAttributeHandle pHandle, IDimensionHandleSet pDimensions)
        {
            attribute = attributeInfo;
            handle = pHandle;
            dimensions = pDimensions;
            transportation = "HLAreliable".Equals(attributeInfo.Transportation) ? TransportationType.HLA_RELIABLE : TransportationType.HLA_BEST_EFFORT;
            order = "Receive".Equals(attributeInfo.Order) ? OrderType.RECEIVE : OrderType.TIMESTAMP;
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pDescriptorManager">the descriptor manager responsible for
        /// this descriptor
        /// </param>
        /// <param name="pName">the name of the attribute
        /// </param>
        /// <param name="pHandle">the handle of the attribute
        /// </param>
        /// <param name="pProxy">the proxy that corresponds to a remote object
        /// </param>
        public AttributeDescriptor(DescriptorManager pDescriptorManager, System.String pName, IAttributeHandle pHandle)
        {
            descriptorManager = pDescriptorManager;
            attribute.Name = pName;
            handle = pHandle;

            dimensions = new XRTIDimensionHandleSet();
        }
    }
}
