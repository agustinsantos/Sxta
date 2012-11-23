using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents an HLA simple data type.
    /// <code>
    /// <objectClass name="HLASimpleData"
    ///          sharing="PublishSubscribe"
    ///         semantics="Represents an HLA simple data type.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLASimpleData",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents an HLA simple data type.")]
    public class HLASimpleData : HLAreflection
    {
        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "representation",
                      Semantics = "TODO.")]
        public string Representation
        {
            get { return representation; }
            set { representation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "representationNotes",
                      Semantics = "TODO.")]
        public string RepresentationNotes
        {
            get { return representationNotes; }
            set { representationNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "units",
                      Semantics = "TODO.")]
        public string Units
        {
            get { return units; }
            set { units = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "unitsNotes",
                      Semantics = "TODO.")]
        public string UnitsNotes
        {
            get { return unitsNotes; }
            set { unitsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "resolution",
                      Semantics = "TODO.")]
        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "resolutionNotes",
                      Semantics = "TODO.")]
        public string ResolutionNotes
        {
            get { return resolutionNotes; }
            set { resolutionNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "representation",
              Semantics = "TODO.")]
        public string Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "representation",
              Semantics = "TODO.")]
        public string AccuracyNotes
        {
            get { return accuracyNotes; }
            set { accuracyNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "semantics",
                      Semantics = "TODO.")]
        public string Semantics
        {
            get { return semantics; }
            set { semantics = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "semanticsNotes",
                      Semantics = "TODO.")]
        public string SemanticsNotes
        {
            get { return semanticsNotes; }
            set { semanticsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "nativeType",
                      Semantics = "TODO.")]
        public System.Type NativeType
        {
            get { return nativeType; }
            set { nativeType = value; }
        }

        public HLASimpleData()
            : base()
        { }

        public HLASimpleData(System.Xml.XmlElement simpleDataElement)
            : base(simpleDataElement)
        {
            Representation = simpleDataElement.GetAttribute("representation");
            RepresentationNotes = simpleDataElement.GetAttribute("representationNotes");
            Units = simpleDataElement.GetAttribute("units");
            UnitsNotes = simpleDataElement.GetAttribute("unitsNotes");
            Resolution = simpleDataElement.GetAttribute("resolution");
            ResolutionNotes = simpleDataElement.GetAttribute("resolutionNotes");
            Accuracy = simpleDataElement.GetAttribute("accuracy");
            AccuracyNotes = simpleDataElement.GetAttribute("accuracyNotes");
            Semantics = ReplaceNewLines(simpleDataElement.GetAttribute("semantics"));
            SemanticsNotes = simpleDataElement.GetAttribute("semanticsNotes");
        }


        protected string representation;
        protected string representationNotes;
        protected string units;
        protected string unitsNotes;
        protected string resolution;
        protected string resolutionNotes;
        protected string accuracy;
        protected string accuracyNotes;
        protected string semantics;
        protected string semanticsNotes;

        protected System.Type nativeType;
    }
}
