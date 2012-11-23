namespace Sxta.Rti1516.Reflection
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Represents an HLA dimension. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAObjectClass(Name = "HLAdimension",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Semantics = "Represents an HLA dimension.")]
    public class HLAdimension : HLAreflection
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
        ///Attribute #upperBound. 
        ///</summary>
        private String upperBound;

        ///<summary>
        ///Attribute #upperBoundNotes. 
        ///</summary>
        private String upperBoundNotes;

        ///<summary>
        ///Attribute #normalization. 
        ///</summary>
        private String normalization;

        ///<summary>
        ///Attribute #normalizationNotes. 
        ///</summary>
        private String normalizationNotes;

        ///<summary>
        ///Attribute #value. 
        ///</summary>
        private String internalValue;

        ///<summary>
        ///Attribute #valueNotes. 
        ///</summary>
        private String valueNotes;

        ///<summary> Returns a string representation of this HLAdimension. </summary>
        ///<returns> a string representation of this HLAdimension</returns>
        public override String ToString()
        {
            return "HLAdimension(" +
                     "dataType: " + dataType + ", " +
                     "dataTypeNotes: " + dataTypeNotes + ", " +
                     "upperBound: " + upperBound + ", " +
                     "upperBoundNotes: " + upperBoundNotes + ", " +
                     "normalization: " + normalization + ", " +
                     "normalizationNotes: " + normalizationNotes + ", " +
                     "value: " + internalValue + ", " +
                     "valueNotes: " + valueNotes +
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
        /// Gets/Sets the value of the upperBound field.
        ///</summary>
        [HLAAttribute(Name = "upperBound",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String UpperBound
        {
            set { upperBound = value; }
            get { return upperBound; }
        }


        ///<summary>
        /// Gets/Sets the value of the upperBoundNotes field.
        ///</summary>
        [HLAAttribute(Name = "upperBoundNotes",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String UpperBoundNotes
        {
            set { upperBoundNotes = value; }
            get { return upperBoundNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the normalization field.
        ///</summary>
        [HLAAttribute(Name = "normalization",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Normalization
        {
            set { normalization = value; }
            get { return normalization; }
        }


        ///<summary>
        /// Gets/Sets the value of the normalizationNotes field.
        ///</summary>
        [HLAAttribute(Name = "normalizationNotes",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String NormalizationNotes
        {
            set { normalizationNotes = value; }
            get { return normalizationNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the value field.
        ///</summary>
        [HLAAttribute(Name = "value",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Value
        {
            set { internalValue = value; }
            get { return internalValue; }
        }


        ///<summary>
        /// Gets/Sets the value of the valueNotes field.
        ///</summary>
        [HLAAttribute(Name = "valueNotes",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String ValueNotes
        {
            set { valueNotes = value; }
            get { return valueNotes; }
        }

    }
}
