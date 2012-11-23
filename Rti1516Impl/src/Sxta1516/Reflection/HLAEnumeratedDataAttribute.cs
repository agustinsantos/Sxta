namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;

    /// <summary>
    /// A enumerated data type.
    /// <code>
    ///  <xsd:element name="simpleData"
    ///           maxOccurs="unbounded">
    ///  <xsd:annotation>
    ///      <xsd:documentation>A simple data type.</xsd:documentation>
    ///  </xsd:annotation>
    ///  <xsd:complexType>
    ///          ...........
    ///      <xsd:attribute name="name"
    ///                     type="xsd:NMTOKEN"
    ///                     use="required"/>
    ///      <xsd:attribute name="nameNotes"
    ///                     type="xsd:IDREFS"/>
    ///      <xsd:attribute name="representation"
    ///                     type="xsd:NMTOKEN"/>
    ///      <xsd:attribute name="representationNotes"
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
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public sealed class HLAEnumeratedDataAttribute : HLAReflectionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public HLAEnumeratedData EnumeratedDataInfo
        {
            get { return ((HLAEnumeratedData)baseInfo); }
        }


        /// <summary>
        /// Gets/Sets the value of the representation field.
        /// </summary>
        public string Representation
        {
            get { return ((HLAEnumeratedData)baseInfo).Representation; }
            set { ((HLAEnumeratedData)baseInfo).Representation = value; }
        }

        /// <summary>
        /// Gets/Sets the value of the representationNotes field.
        /// </summary>
        public string RepresentationNotes
        {
            get { return ((HLAEnumeratedData)baseInfo).RepresentationNotes; }
            set { ((HLAEnumeratedData)baseInfo).RepresentationNotes = value; }
        }


        /// <summary>
        /// Gets/Sets the value of the semantics field.
        /// </summary>
        public string Semantics
        {
            get { return ((HLAEnumeratedData)baseInfo).Semantics; }
            set { ((HLAEnumeratedData)baseInfo).Semantics = value; }
        }

        /// <summary>
        /// Gets/Sets the value of the semanticsNotes field.
        /// </summary>
        public string SemanticsNotes
        {
            get { return ((HLAEnumeratedData)baseInfo).SemanticsNotes; }
            set { ((HLAEnumeratedData)baseInfo).SemanticsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Type NativeType
        {
            get { return ((HLAEnumeratedData)baseInfo).NativeType; }
            set { ((HLAEnumeratedData)baseInfo).NativeType = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAEnumeratedDataAttribute()
        {
            baseInfo = new HLAEnumeratedData();
        }

    }
}
