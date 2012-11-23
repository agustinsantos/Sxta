namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;
    
    /// <summary>
    /// Defines a new BasicData (basic data element).
    /// <code>
    /// <xsd:element name="basicData"
    ///             maxOccurs="unbounded">
    ///    <xsd:annotation>
    ///        <xsd:documentation>A basic data element.</xsd:documentation>
    ///    </xsd:annotation>
    ///    <xsd:complexType>
    ///        <xsd:attribute name="name"
    ///                       type="xsd:NMTOKEN"
    ///                       use="required"/>
    ///        <xsd:attribute name="nameNotes"
    ///                       type="xsd:IDREFS"/>
    ///        <xsd:attribute name="size"
    ///                       type="xsd:string"/>
    ///        <xsd:attribute name="sizeNotes"
    ///                       type="xsd:IDREFS"/>
    ///        <xsd:attribute name="interpretation"
    ///                       type="xsd:string"/>
    ///        <xsd:attribute name="interpretationNotes"
    ///                       type="xsd:IDREFS"/>
    ///        <xsd:attribute name="endian"
    ///                       type="endianType"/>
    ///        <xsd:attribute name="endianNotes"
    ///                       type="xsd:IDREFS"/>
    ///        <xsd:attribute name="encoding"
    ///                       type="xsd:string"/>
    ///        <xsd:attribute name="encodingNotes"
    ///                       type="xsd:IDREFS"/>
    ///        <xsd:anyAttribute/>
    ///    </xsd:complexType>
    ///</xsd:element>
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class HLABasicDataAttribute : HLAReflectionAttribute
    {

        public HLABasicData BasicDataInfo
        {
            get { return ((HLABasicData)baseInfo); }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get { return ((HLABasicData)baseInfo).Size; }
            set { ((HLABasicData)baseInfo).Size = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SizeNotes
        {
            get { return ((HLABasicData)baseInfo).SizeNotes; }
            set { ((HLABasicData)baseInfo).SizeNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Interpretation
        {
            get { return ((HLABasicData)baseInfo).Interpretation; }
            set { ((HLABasicData)baseInfo).Interpretation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InterpretationNotes
        {
            get { return ((HLABasicData)baseInfo).InterpretationNotes; }
            set { ((HLABasicData)baseInfo).InterpretationNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HLAendianType Endian
        {
            get { return ((HLABasicData)baseInfo).Endian; }
            set { ((HLABasicData)baseInfo).Endian = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EndianNotes
        {
            get { return ((HLABasicData)baseInfo).EndianNotes; }
            set { ((HLABasicData)baseInfo).EndianNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Encoding
        {
            get { return ((HLABasicData)baseInfo).Encoding; }
            set { ((HLABasicData)baseInfo).Encoding = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EncodingNotes
        {
            get { return ((HLABasicData)baseInfo).EncodingNotes; }
            set { ((HLABasicData)baseInfo).EncodingNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Type NativeType
        {
            get { return ((HLABasicData)baseInfo).NativeType; }
            set { ((HLABasicData)baseInfo).NativeType = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLABasicDataAttribute()
        {
            baseInfo = new HLABasicData();
        }


    }
}
