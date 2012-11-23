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
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class HLASimpleDataAttribute : HLAReflectionAttribute
    {

        public HLASimpleData SimpleDataInfo
        {
            get { return ((HLASimpleData)baseInfo); }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Representation
        {
            get { return ((HLASimpleData)baseInfo).Representation; }
            set { ((HLASimpleData)baseInfo).Representation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RepresentationNotes
        {
            get { return ((HLASimpleData)baseInfo).RepresentationNotes; }
            set { ((HLASimpleData)baseInfo).RepresentationNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Units
        {
            get { return ((HLASimpleData)baseInfo).Units; }
            set { ((HLASimpleData)baseInfo).Units = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UnitsNotes
        {
            get { return ((HLASimpleData)baseInfo).UnitsNotes; }
            set { ((HLASimpleData)baseInfo).UnitsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Resolution
        {
            get { return ((HLASimpleData)baseInfo).Resolution; }
            set { ((HLASimpleData)baseInfo).Resolution = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ResolutionNotes
        {
            get { return ((HLASimpleData)baseInfo).ResolutionNotes; }
            set { ((HLASimpleData)baseInfo).ResolutionNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Accuracy
        {
            get { return ((HLASimpleData)baseInfo).Accuracy; }
            set { ((HLASimpleData)baseInfo).Accuracy = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AccuracyNotes
        {
            get { return ((HLASimpleData)baseInfo).AccuracyNotes; }
            set { ((HLASimpleData)baseInfo).AccuracyNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Semantics
        {
            get { return ((HLASimpleData)baseInfo).Semantics; }
            set { ((HLASimpleData)baseInfo).Semantics = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SemanticsNotes
        {
            get { return ((HLASimpleData)baseInfo).SemanticsNotes; }
            set { ((HLASimpleData)baseInfo).SemanticsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Type NativeType
        {
            get { return ((HLASimpleData)baseInfo).NativeType; }
            set { ((HLASimpleData)baseInfo).NativeType = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLASimpleDataAttribute()
        {
            baseInfo = new HLASimpleData();
        }

    }
}
