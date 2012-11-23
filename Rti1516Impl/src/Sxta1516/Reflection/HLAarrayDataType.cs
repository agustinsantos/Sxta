namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    /// <summary>
    /// Represents an HLA array data type. 
    /// <code>
    /// <objectClass name="HLAarrayDataType"
    ///          sharing="PublishSubscribe"
    ///         semantics="Represents an HLA simple data type.">
    /// </code>
    /// </summary>
    [HLAObjectClass(Name = "HLAarrayDataType",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Semantics = "Represents an HLA array data type.")]
    public class HLAarrayDataType : HLAreflection
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
        ///Attribute #cardinality. 
        ///</summary>
        private String cardinality;

        ///<summary>
        ///Attribute #cardinalityNotes. 
        ///</summary>
        private String cardinalityNotes;

        ///<summary>
        ///Attribute #encoding. 
        ///</summary>
        private String encoding;

        ///<summary>
        ///Attribute #encodingNotes. 
        ///</summary>
        private String encodingNotes;

        ///<summary>
        ///Attribute #semantics. 
        ///</summary>
        private String semantics;

        ///<summary>
        ///Attribute #semanticsNotes. 
        ///</summary>
        private String semanticsNotes;

        ///<summary> Returns a string representation of this HLAarrayDataType. </summary>
        ///<returns> a string representation of this HLAarrayDataType</returns>
        public override String ToString()
        {
            return "HLAarrayDataType(" +
                     "dataType: " + dataType + ", " +
                     "dataTypeNotes: " + dataTypeNotes + ", " +
                     "cardinality: " + cardinality + ", " +
                     "cardinalityNotes: " + cardinalityNotes + ", " +
                     "encoding: " + encoding + ", " +
                     "encodingNotes: " + encodingNotes + ", " +
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
        /// Gets/Sets the value of the cardinality field.
        ///</summary>
        [HLAAttribute(Name = "cardinality",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Cardinality
        {
            set { cardinality = value; }
            get { return cardinality; }
        }


        ///<summary>
        /// Gets/Sets the value of the cardinalityNotes field.
        ///</summary>
        [HLAAttribute(Name = "cardinalityNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String CardinalityNotes
        {
            set { cardinalityNotes = value; }
            get { return cardinalityNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the encoding field.
        ///</summary>
        [HLAAttribute(Name = "encoding",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Encoding
        {
            set { encoding = value; }
            get { return encoding; }
        }


        ///<summary>
        /// Gets/Sets the value of the encodingNotes field.
        ///</summary>
        [HLAAttribute(Name = "encodingNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String EncodingNotes
        {
            set { encodingNotes = value; }
            get { return encodingNotes; }
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
        /// If this array has a built-in serializer like
        /// unicode strings, and so on.
        /// </summary>
        public bool HasNativeSerializer
        {
            set { hasNativeSerializer = value; }
            get { return hasNativeSerializer; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HLAarrayDataType()
            : base()
        { }

        /// <summary>
        /// Constructs from a XML source
        /// </summary>
        public HLAarrayDataType(System.Xml.XmlElement simpleDataElement)
            : base(simpleDataElement)
        {
            DataType = simpleDataElement.GetAttribute("dataType");
            DataTypeNotes = simpleDataElement.GetAttribute("dataTypeNotes");
            Cardinality = simpleDataElement.GetAttribute("cardinality");
            CardinalityNotes = simpleDataElement.GetAttribute("cardinalityNotes");
            Encoding = simpleDataElement.GetAttribute("encoding");
            EncodingNotes = simpleDataElement.GetAttribute("encodingNotes");
            Semantics = ReplaceNewLines(simpleDataElement.GetAttribute("semantics"));
            SemanticsNotes = simpleDataElement.GetAttribute("semanticsNotes");

            if (Name.Equals("HLAASCIIstring") ||
                Name.Equals("HLAunicodeString") ||
                Name.Equals("HLAopaqueData"))
            {
                hasNativeSerializer = true;
            }
            else
            {
                hasNativeSerializer = false;
            }
        }

        /// <summary>
        /// If this array has a built-in serializer like
        /// unicode strings, and so on.
        /// </summary>
        protected bool hasNativeSerializer = false;
    }
}
