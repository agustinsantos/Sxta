namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    ///<summary>
    ///Variant record type alternative. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [Serializable]
    [HLAFixedRecordData(Name = "HLAalternative",
                        Encoding = "HLAfixedRecord",
                        Semantics = "Variant record type alternative.")]
    public struct HLAalternative
    {
        ///<summary>
        ///Alternative name. 
        ///</summary>
        private String name;

        ///<summary>
        ///Notes to the alternative name. 
        ///</summary>
        private String nameNotes;

        ///<summary>
        ///Alternative enumerator. 
        ///</summary>
        private String enumerator;

        ///<summary>
        ///Notes to the alternative enumerator. 
        ///</summary>
        private String enumeratorNotes;

        ///<summary>
        ///Alternative data type. 
        ///</summary>
        private String dataType;

        ///<summary>
        ///Notes to the alternative data type. 
        ///</summary>
        private String dataTypeNotes;

        ///<summary>
        ///Alternative semantics. 
        ///</summary>
        private String semantics;

        ///<summary>
        ///Notes to field semantics. 
        ///</summary>
        private String semanticsNotes;

        ///<summary> Returns a string representation of this HLAalternative. </summary>
        ///<returns> a string representation of this HLAalternative</returns>
        public override String ToString()
        {
            return "HLAalternative(" +
                     "name: " + name + ", " +
                     "nameNotes: " + nameNotes + ", " +
                     "enumerator: " + enumerator + ", " +
                     "enumeratorNotes: " + enumeratorNotes + ", " +
                     "dataType: " + dataType + ", " +
                     "dataTypeNotes: " + dataTypeNotes + ", " +
                     "semantics: " + semantics + ", " +
                     "semanticsNotes: " + semanticsNotes +
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the name field.
        ///</summary>
        [HLARecordField(Name = "name",
                        DataType = "HLAunicodeString",
                        Semantics = "Alternative name.")]
        public String Name
        {
            set { name = value; }
            get { return name; }
        }


        ///<summary>
        /// Gets/Sets the value of the nameNotes field.
        ///</summary>
        [HLARecordField(Name = "nameNotes",
                        DataType = "HLAunicodeString",
                        Semantics = "Notes to the alternative name.")]
        public String NameNotes
        {
            set { nameNotes = value; }
            get { return nameNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the enumerator field.
        ///</summary>
        [HLARecordField(Name = "enumerator",
                        DataType = "HLAunicodeString",
                        Semantics = "Alternative enumerator.")]
        public String Enumerator
        {
            set { enumerator = value; }
            get { return enumerator; }
        }


        ///<summary>
        /// Gets/Sets the value of the enumeratorNotes field.
        ///</summary>
        [HLARecordField(Name = "enumeratorNotes",
                        DataType = "HLAunicodeString",
                        Semantics = "Notes to the alternative enumerator.")]
        public String EnumeratorNotes
        {
            set { enumeratorNotes = value; }
            get { return enumeratorNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the dataType field.
        ///</summary>
        [HLARecordField(Name = "dataType",
                        DataType = "HLAunicodeString",
                        Semantics = "Alternative data type.")]
        public String DataType
        {
            set { dataType = value; }
            get { return dataType; }
        }


        ///<summary>
        /// Gets/Sets the value of the dataTypeNotes field.
        ///</summary>
        [HLARecordField(Name = "dataTypeNotes",
                        DataType = "HLAunicodeString",
                        Semantics = "Notes to the alternative data type.")]
        public String DataTypeNotes
        {
            set { dataTypeNotes = value; }
            get { return dataTypeNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the semantics field.
        ///</summary>
        [HLARecordField(Name = "semantics",
                        DataType = "HLAunicodeString",
                        Semantics = "Alternative semantics.")]
        public String Semantics
        {
            set { semantics = value; }
            get { return semantics; }
        }


        ///<summary>
        /// Gets/Sets the value of the semanticsNotes field.
        ///</summary>
        [HLARecordField(Name = "semanticsNotes",
                        DataType = "HLAunicodeString",
                        Semantics = "Notes to field semantics.")]
        public String SemanticsNotes
        {
            set { semanticsNotes = value; }
            get { return semanticsNotes; }
        }

    }
}
