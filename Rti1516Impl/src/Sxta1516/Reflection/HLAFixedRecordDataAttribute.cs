namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;
    
    /// <summary>
    /// A Fixed Record data type.
    /// <code>
    ///  <element name="simpleData"
    ///           maxOccurs="unbounded">
    ///  <annotation>
    ///      <documentation>A Fixed Record data type.</documentation>
    ///  </annotation>
    ///  <complexType>
    ///      <attribute name="name"
    ///                     type="xsd:NMTOKEN"
    ///                     use="required"/>
    ///      <attribute name="nameNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <attribute name="encoding"
    ///                     type="xsd:string"/>
    ///      <attribute name="encodingNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <attribute name="semantics"
    ///                     type="xsd:string"/>
    ///      <attribute name="semanticsNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <anyAttribute/>
    ///  </complexType>
    ///  </element>
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class HLAFixedRecordDataAttribute : HLAReflectionAttribute
    {

        public HLAFixedRecordData FixedRecordDataInfo
        {
            get { return ((HLAFixedRecordData)baseInfo); }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Encoding
        {
            get { return ((HLAFixedRecordData)baseInfo).Encoding; }
            set { ((HLAFixedRecordData)baseInfo).Encoding = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EncodingNotes
        {
            get { return ((HLAFixedRecordData)baseInfo).EncodingNotes; }
            set { ((HLAFixedRecordData)baseInfo).EncodingNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Semantics
        {
            get { return ((HLAFixedRecordData)baseInfo).Semantics; }
            set { ((HLAFixedRecordData)baseInfo).Semantics = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SemanticsNotes
        {
            get { return ((HLAFixedRecordData)baseInfo).SemanticsNotes; }
            set { ((HLAFixedRecordData)baseInfo).SemanticsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Type NativeType
        {
            get { return ((HLAFixedRecordData)baseInfo).NativeType; }
            set { ((HLAFixedRecordData)baseInfo).NativeType = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAFixedRecordDataAttribute()
        {
            baseInfo = new HLAFixedRecordData();
        }

    }
}
