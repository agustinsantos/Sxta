namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;

    ///<summary>
    ///Represents an HLA basic representation. 
    ///</summary>
    [HLAObjectClassAttribute(Name = "HLABasicData",
                             Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                             Semantics = "Represents an HLA basic representation.")]
    public class HLABasicData : HLAreflection
    {
        ///<summary>
        ///Attribute #size. 
        ///</summary>
        private int size;

        ///<summary>
        ///Attribute #sizeNotes. 
        ///</summary>
        private string sizeNotes;

        ///<summary>
        ///Attribute #endian. 
        ///</summary>
        private HLAendianType endian;

        ///<summary>
        ///Attribute #endianNotes. 
        ///</summary>
        private string endianNotes;

        ///<summary>
        ///Attribute #interpretation. 
        ///</summary>
        private String interpretation;

        ///<summary>
        ///Attribute #interpretationNotes. 
        ///</summary>
        private String interpretationNotes;

        ///<summary>
        ///Attribute #encoding. 
        ///</summary>
        private String encoding;

        ///<summary>
        ///Attribute #encodingNotes. 
        ///</summary>
        private String encodingNotes;

        ///<summary> Returns a string representation of this HLAbasicRepresentation. </summary>
        ///<returns> a string representation of this HLAbasicRepresentation</returns>
        public override String ToString()
        {
            return "HLAbasicRepresentation(" +
                     "size: " + size + ", " +
                     "sizeNotes: " + sizeNotes + ", " +
                     "endian: " + endian + ", " +
                     "endianNotes: " + endianNotes + ", " +
                     "interpretation: " + interpretation + ", " +
                     "interpretationNotes: " + interpretationNotes + ", " +
                     "encoding: " + encoding + ", " +
                     "encodingNotes: " + encodingNotes +
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the size field.
        ///</summary>
        [HLAAttribute(Name = "size",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAbasicRepresentationSize",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public int Size
        {
            set { size = value; }
            get { return size; }
        }


        ///<summary>
        /// Gets/Sets the value of the sizeNotes field.
        ///</summary>
        [HLAAttribute(Name = "sizeNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAbasicRepresentationSize",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public string SizeNotes
        {
            set { sizeNotes = value; }
            get { return sizeNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the endian field.
        ///</summary>
        [HLAAttribute(Name = "endian",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAendianType",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public HLAendianType Endian
        {
            set { endian = value; }
            get { return endian; }
        }


        ///<summary>
        /// Gets/Sets the value of the endianNotes field.
        ///</summary>
        [HLAAttribute(Name = "endianNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAendianType",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public string EndianNotes
        {
            set { endianNotes = value; }
            get { return endianNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the interpretation field.
        ///</summary>
        [HLAAttribute(Name = "interpretation",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Interpretation
        {
            set { interpretation = value; }
            get { return interpretation; }
        }


        ///<summary>
        /// Gets/Sets the value of the interpretationNotes field.
        ///</summary>
        [HLAAttribute(Name = "interpretationNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String InterpretationNotes
        {
            set { interpretationNotes = value; }
            get { return interpretationNotes; }
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

        /// <summary>
        /// The underlying  native type
        /// </summary>
        public System.Type NativeType
        {
            get { return nativeType; }
            set { nativeType = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HLABasicData()
            : base()
        {
        }

        /// <summary>
        /// Constructor from a source
        /// </summary>
        public HLABasicData(System.Xml.XmlElement basicDataElement)
            : base(basicDataElement)
        {
            Size = int.Parse(basicDataElement.GetAttribute("size"));
            SizeNotes = basicDataElement.GetAttribute("sizeNotes");
            Interpretation = ReplaceNewLines(basicDataElement.GetAttribute("interpretation"));
            InterpretationNotes = basicDataElement.GetAttribute("interpretationNotes");
            Endian = (HLAendianType)System.Enum.Parse(typeof(HLAendianType), basicDataElement.GetAttribute("endian"));
            EndianNotes = basicDataElement.GetAttribute("endianNotes");
            Encoding = ReplaceNewLines(basicDataElement.GetAttribute("encoding"));
            EncodingNotes = basicDataElement.GetAttribute("encodingNotes");
            NativeType = NativeTypeForBasicRepresentation(this.Name);
        }

        /// <summary> 
        /// Returns the Native type name corresponding to the specified basic representation
        /// name.
        /// </summary>
        /// <param name="basicRepresentationName">the basic representation name
        /// </param>
        /// <returns> the corresponding Native type name
        /// </returns>
        private System.Type NativeTypeForBasicRepresentation(string basicRepresentationName)
        {
            if (basicRepresentationName == null)
            {
                return typeof(byte[]);
            }
            else if (basicRepresentationName.Equals("HLAinteger16BE") || basicRepresentationName.Equals("HLAinteger16LE"))
            {
                return typeof(short);
            }
            else if (basicRepresentationName.Equals("HLAinteger32BE") || basicRepresentationName.Equals("HLAinteger32LE"))
            {
                return typeof(int);
            }
            else if (basicRepresentationName.Equals("HLAinteger64BE") || basicRepresentationName.Equals("HLAinteger64LE"))
            {
                return typeof(long);
            }
            else if (basicRepresentationName.Equals("HLAfloat32BE") || basicRepresentationName.Equals("HLAfloat32LE"))
            {
                return typeof(float);
            }
            else if (basicRepresentationName.Equals("HLAfloat64BE") || basicRepresentationName.Equals("HLAfloat64LE"))
            {
                return typeof(double);
            }
            else if (basicRepresentationName.Equals("HLAoctetPairBE") || basicRepresentationName.Equals("HLAoctetPairLE"))
            {
                return typeof(short);
            }
            else if (basicRepresentationName.Equals("HLAoctet"))
            {
                return typeof(byte);
            }
            else
            {
                return typeof(byte[]);
            }
        }

        /// <summary>
        ///  The native type
        /// </summary>
        protected System.Type nativeType;
    }
}
