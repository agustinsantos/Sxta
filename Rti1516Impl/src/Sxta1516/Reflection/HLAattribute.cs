namespace Sxta.Rti1516.Reflection
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Represents an HLA object attribute. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAObjectClass(Name = "HLAattribute",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Semantics = "Represents an HLA object attribute.")]
    public class HLAattribute : HLAreflection, IHLAattribute
    {

        ///<summary>
        ///Attribute #dataType. 
        ///</summary>
        private String dataType;

        ///<summary>
        ///Attribute #dataTypeNotes. 
        ///</summary>
        private String dataTypeNotes;

        ///<summary>
        ///Attribute #updateType. 
        ///</summary>
        private HLAupdateType updateType;

        ///<summary>
        ///Attribute #updateTypeNotes. 
        ///</summary>
        private String updateTypeNotes;

        ///<summary>
        ///Attribute #updateCondition. 
        ///</summary>
        private String updateCondition;

        ///<summary>
        ///Attribute #updateConditionNotes. 
        ///</summary>
        private String updateConditionNotes;

        ///<summary>
        ///Attribute #ownership. 
        ///</summary>
        private HLAownershipType ownership;

        ///<summary>
        ///Attribute #ownershipNotes. 
        ///</summary>
        private String ownershipNotes;

        ///<summary>
        ///Attribute #sharing. 
        ///</summary>
        private HLAsharingType sharing;

        ///<summary>
        ///Attribute #sharingNotes. 
        ///</summary>
        private String sharingNotes;

        ///<summary>
        ///Attribute #dimensions. 
        ///</summary>
        private String dimensions;

        ///<summary>
        ///Attribute #dimensionsNotes. 
        ///</summary>
        private String dimensionsNotes;

        ///<summary>
        ///Attribute #transportation. 
        ///</summary>
        private String transportation;

        ///<summary>
        ///Attribute #transportationNotes. 
        ///</summary>
        private String transportationNotes;

        ///<summary>
        ///Attribute #order. 
        ///</summary>
        private HLAorderType order;

        ///<summary>
        ///Attribute #orderNotes. 
        ///</summary>
        private String orderNotes;

        ///<summary>
        ///Attribute #semantics. 
        ///</summary>
        private String semantics;

        ///<summary>
        ///Attribute #semanticsNotes. 
        ///</summary>
        private String semanticsNotes;

        ///<summary> Returns a string representation of this HLAattribute. </summary>
        ///<returns> a string representation of this HLAattribute</returns>
        public override String ToString()
        {
            return "HLAattribute(" +
                     "dataType: " + dataType + ", " +
                     "dataTypeNotes: " + dataTypeNotes + ", " +
                     "updateType: " + updateType + ", " +
                     "updateTypeNotes: " + updateTypeNotes + ", " +
                     "updateCondition: " + updateCondition + ", " +
                     "updateConditionNotes: " + updateConditionNotes + ", " +
                     "ownership: " + ownership + ", " +
                     "ownershipNotes: " + ownershipNotes + ", " +
                     "sharing: " + sharing + ", " +
                     "sharingNotes: " + sharingNotes + ", " +
                     "dimensions: " + dimensions + ", " +
                     "dimensionsNotes: " + dimensionsNotes + ", " +
                     "transportation: " + transportation + ", " +
                     "transportationNotes: " + transportationNotes + ", " +
                     "order: " + order + ", " +
                     "orderNotes: " + orderNotes + ", " +
                     "semantics: " + semantics + ", " +
                     "semanticsNotes: " + semanticsNotes +
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the dataType field.
        ///</summary>
        [HLAAttribute(Name = "dataType",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String DataType
        {
            set { dataType = value; }
            get { return dataType; }
        }


        ///<summary>
        /// Gets/Sets the value of the dataTypeNotes field.
        ///</summary>
        [HLAAttribute(Name = "dataTypeNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String DataTypeNotes
        {
            set { dataTypeNotes = value; }
            get { return dataTypeNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the updateType field.
        ///</summary>
        [HLAAttribute(Name = "updateType",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAupdateType",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                     Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public HLAupdateType UpdateType
        {
            set { updateType = value; }
            get { return updateType; }
        }


        ///<summary>
        /// Gets/Sets the value of the updateTypeNotes field.
        ///</summary>
        [HLAAttribute(Name = "updateTypeNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                     Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String UpdateTypeNotes
        {
            set { updateTypeNotes = value; }
            get { return updateTypeNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the updateCondition field.
        ///</summary>
        [HLAAttribute(Name = "updateCondition",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                     Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String UpdateCondition
        {
            set { updateCondition = value; }
            get { return updateCondition; }
        }


        ///<summary>
        /// Gets/Sets the value of the updateConditionNotes field.
        ///</summary>
        [HLAAttribute(Name = "updateConditionNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String UpdateConditionNotes
        {
            set { updateConditionNotes = value; }
            get { return updateConditionNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the ownership field.
        ///</summary>
        [HLAAttribute(Name = "ownership",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAownershipType",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public HLAownershipType Ownership
        {
            set { ownership = value; }
            get { return ownership; }
        }


        ///<summary>
        /// Gets/Sets the value of the ownershipNotes field.
        ///</summary>
        [HLAAttribute(Name = "ownershipNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                     Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String OwnershipNotes
        {
            set { ownershipNotes = value; }
            get { return ownershipNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the sharing field.
        ///</summary>
        [HLAAttribute(Name = "sharing",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAsharingType",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                     Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public HLAsharingType Sharing
        {
            set { sharing = value; }
            get { return sharing; }
        }


        ///<summary>
        /// Gets/Sets the value of the sharingNotes field.
        ///</summary>
        [HLAAttribute(Name = "sharingNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String SharingNotes
        {
            set { sharingNotes = value; }
            get { return sharingNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the dimensions field.
        ///</summary>
        [HLAAttribute(Name = "dimensions",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Dimensions
        {
            set { dimensions = value; }
            get { return dimensions; }
        }


        ///<summary>
        /// Gets/Sets the value of the dimensionsNotes field.
        ///</summary>
        [HLAAttribute(Name = "dimensionsNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String DimensionsNotes
        {
            set { dimensionsNotes = value; }
            get { return dimensionsNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the transportation field.
        ///</summary>
        [HLAAttribute(Name = "transportation",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Transportation
        {
            set { transportation = value; }
            get { return transportation; }
        }


        ///<summary>
        /// Gets/Sets the value of the transportationNotes field.
        ///</summary>
        [HLAAttribute(Name = "transportationNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String TransportationNotes
        {
            set { transportationNotes = value; }
            get { return transportationNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the order field.
        ///</summary>
        [HLAAttribute(Name = "order",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAorderType",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public HLAorderType Order
        {
            set { order = value; }
            get { return order; }
        }


        ///<summary>
        /// Gets/Sets the value of the orderNotes field.
        ///</summary>
        [HLAAttribute(Name = "orderNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String OrderNotes
        {
            set { orderNotes = value; }
            get { return orderNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the semantics field.
        ///</summary>
        [HLAAttribute(Name = "semantics",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Semantics
        {
            set { semantics = value; }
            get { return semantics; }
        }


        ///<summary>
        /// Gets/Sets the value of the semanticsNotes field.
        ///</summary>
        [HLAAttribute(Name = "semanticsNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String SemanticsNotes
        {
            set { semanticsNotes = value; }
            get { return semanticsNotes; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAattribute()
            : base()
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAattribute(System.Xml.XmlElement attributeElement)
            : base(attributeElement)
        {
            if (string.IsNullOrEmpty(attributeElement.GetAttribute("sharing")))
                Sharing = HLAsharingType.Neither;
            else
                Sharing = (HLAsharingType)Enum.Parse(typeof(HLAsharingType), attributeElement.GetAttribute("sharing"));
            SharingNotes = attributeElement.GetAttribute("sharingNotes");
            Semantics = attributeElement.GetAttribute("semantic");
            SemanticsNotes = attributeElement.GetAttribute("semanticsNotes");
            DataType = attributeElement.GetAttribute("dataType");

            if (string.IsNullOrEmpty(attributeElement.GetAttribute("updateType")))
                UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static;
            else
                UpdateType = (HLAupdateType)Enum.Parse(typeof(HLAupdateType), attributeElement.GetAttribute("updateType"));

            UpdateCondition = attributeElement.GetAttribute("updateCondition");
            if (string.IsNullOrEmpty(attributeElement.GetAttribute("ownership")))
                Ownership = HLAownershipType.NoTransfer;
            else
                Ownership = (HLAownershipType)Enum.Parse(typeof(HLAownershipType), attributeElement.GetAttribute("ownership"));

            Dimensions = attributeElement.GetAttribute("dimensions");
            Transportation = attributeElement.GetAttribute("transportation");
            if (string.IsNullOrEmpty(attributeElement.GetAttribute("order")))
                Order = HLAorderType.Receive;
            else
                Order = (HLAorderType)Enum.Parse(typeof(HLAorderType), attributeElement.GetAttribute("order"));
        }

    }
}
