namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;

    /// <summary>
    /// An Array data type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class HLAArrayDataAttribute : HLAReflectionAttribute
    {

        /// <summary>
        /// Get the HLAarrayDataType info
        /// </summary>
        public HLAarrayDataType ArrayDataInfo
        {
            get { return ((HLAarrayDataType)baseInfo); }
        }


        ///<summary>
        /// Gets/Sets the value of the DataType field.
        ///</summary>
        public string DataType
        {
            get { return ((HLAarrayDataType)baseInfo).DataType; }
            set { ((HLAarrayDataType)baseInfo).DataType = value; }
        }

        ///<summary>
        /// Gets/Sets the value of the DataTypeNotes field.
        ///</summary>
        public string DataTypeNotes
        {
            get { return ((HLAarrayDataType)baseInfo).DataTypeNotes; }
            set { ((HLAarrayDataType)baseInfo).DataTypeNotes = value; }
        }

        ///<summary>
        /// Gets/Sets the value of the Cardinality field.
        ///</summary>
        public string Cardinality
        {
            get { return ((HLAarrayDataType)baseInfo).Cardinality; }
            set { ((HLAarrayDataType)baseInfo).Cardinality = value; }
        }

        ///<summary>
        /// Gets/Sets the value of the CardinalityNotes field.
        ///</summary>
        public string CardinalityNotes
        {
            get { return ((HLAarrayDataType)baseInfo).CardinalityNotes; }
            set { ((HLAarrayDataType)baseInfo).CardinalityNotes = value; }
        }


        ///<summary>
        /// Gets/Sets the value of the Encoding field.
        ///</summary>
        public string Encoding
        {
            get { return ((HLAarrayDataType)baseInfo).Encoding; }
            set { ((HLAarrayDataType)baseInfo).Encoding = value; }
        }

        /// <summary>
        /// Gets/Sets the value of the EncodingNotes field.
        /// </summary>
        public string EncodingNotes
        {
            get { return ((HLAarrayDataType)baseInfo).EncodingNotes; }
            set { ((HLAarrayDataType)baseInfo).EncodingNotes = value; }
        }

        /// <summary>
        /// Gets/Sets the value of the Semantics field.
        /// </summary>
        public string Semantics
        {
            get { return ((HLAarrayDataType)baseInfo).Semantics; }
            set { ((HLAarrayDataType)baseInfo).Semantics = value; }
        }

        /// <summary>
        /// Gets/Sets the value of the SemanticsNotes field.
        /// </summary>
        public string SemanticsNotes
        {
            get { return ((HLAarrayDataType)baseInfo).SemanticsNotes; }
            set { ((HLAarrayDataType)baseInfo).SemanticsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasNativeSerializer
        {
            get { return ((HLAarrayDataType)baseInfo).HasNativeSerializer; }
            set { ((HLAarrayDataType)baseInfo).HasNativeSerializer = value; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAArrayDataAttribute()
        {
            baseInfo = new HLAarrayDataType();
        }

    }
}
