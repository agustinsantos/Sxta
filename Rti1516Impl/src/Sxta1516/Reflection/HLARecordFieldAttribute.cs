namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;

    /// <summary>
    /// A simple data type.
    /// <code>
    ///  <xsd:element name="simpleData"
    ///           maxOccurs="unbounded">
    ///  <xsd:annotation>
    ///      <xsd:documentation>A simple data type.</xsd:documentation>
    ///  </xsd:annotation>
    ///  <xsd:complexType>
    ///      <xsd:attribute name="name"
    ///                     type="xsd:NMTOKEN"
    ///                     use="required"/>
    ///      <xsd:attribute name="nameNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:attribute name="representation"
    ///                     type="xsd:NMTOKEN"/>
    ///      <xsd:attribute name="representationNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:attribute name="units"
    ///                     type="xsd:string"/>
    ///      <xsd:attribute name="unitsNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:attribute name="resolution"
    ///                     type="xsd:string"/>
    ///      <xsd:attribute name="resolutionNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:attribute name="accuracy"
    ///                     type="xsd:string"/>
    ///      <xsd:attribute name="accuracyNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:attribute name="semantics"
    ///                     type="xsd:string"/>
    ///      <xsd:attribute name="semanticsNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:anyAttribute/>
    ///  </xsd:complexType>
    ///  </xsd:element>
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class HLARecordFieldAttribute : HLAReflectionAttribute
    {

        public HLARecordField RecordFieldInfo
        {
            get { return ((HLARecordField)baseInfo); }
        }


        /// <summary>
        /// 
        /// </summary>
        public string DataType
        {
            get { return ((HLARecordField)baseInfo).DataType; }
            set { ((HLARecordField)baseInfo).DataType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DataTypeNotes
        {
            get { return ((HLARecordField)baseInfo).DataTypeNotes; }
            set { ((HLARecordField)baseInfo).DataTypeNotes = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Semantics
        {
            get { return ((HLARecordField)baseInfo).Semantics; }
            set { ((HLARecordField)baseInfo).Semantics = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SemanticsNotes
        {
            get { return ((HLARecordField)baseInfo).SemanticsNotes; }
            set { ((HLARecordField)baseInfo).SemanticsNotes = value; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLARecordFieldAttribute()
        {
            baseInfo = new HLARecordField();
        }

    }
}
