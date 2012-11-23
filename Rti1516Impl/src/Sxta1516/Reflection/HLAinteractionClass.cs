using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{
    /// <summary>
    /// Represents an HLA interaction class.
    /// <code>
    ///  <objectClass name="HLAinteractionClass"
    ///          sharing="PublishSubscribe"
    ///          semantics="Represents an HLA interaction class.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAinteractionClass",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents an HLA object attribute.")]
    public class HLAinteractionClass : HLAreflection
    {
        protected string semantics;
        protected string semanticsNotes;
        protected HLAsharingType sharing;
        protected string sharingNotes;
        protected string dimensions;
        protected string transportation;
        protected HLAorderType order;

        public HLAinteractionClass()
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAinteractionClass(System.Xml.XmlElement interactionElement)
            : base(interactionElement)
        {
            Semantics = ReplaceNewLines(interactionElement.GetAttribute("semantics"));
            SemanticsNotes = interactionElement.GetAttribute("semanticsNotes");
            if (string.IsNullOrEmpty(interactionElement.GetAttribute("sharing")))
                Sharing = HLAsharingType.Neither;
            else
                Sharing = (HLAsharingType)Enum.Parse(typeof(HLAsharingType), interactionElement.GetAttribute("sharing"));
            SharingNotes = interactionElement.GetAttribute("sharingNotes");
            dimensions = interactionElement.GetAttribute("dimensions");
            transportation = interactionElement.GetAttribute("transportation");
            if (string.IsNullOrEmpty(interactionElement.GetAttribute("order")))
                Order = HLAorderType.Receive;
            else
                Order = (HLAorderType)Enum.Parse(typeof(HLAorderType), interactionElement.GetAttribute("order"));
        }

        [HLAAttribute(Name = "SemanticsNotes",
                    Semantics = "Some notes about object semantics.")]
        public string SemanticsNotes
        {
            get { return semanticsNotes; }
            set { semanticsNotes = value; }
        }


        /// <summary> 
        /// The object semantics.
        ///<code>
        ///  <attribute name="semantics"
        ///  dataType="HLAunicodeString"
        ///  updateType="Static"
        ///  updateCondition="NA"
        ///  ownership="NoTransfer"
        ///  sharing="PublishSubscribe"
        ///  dimensions="NA"
        ///  transportation="HLAreliable"
        ///  order="Receive"
        ///  semantics="The object semantics."/>
        ///</code>
        ///</summary>
        [HLAAttribute(Name = "semantics",
                      Semantics = "The object semantics.")]
        public string Semantics
        {
            get { return semantics; }
            set { semantics = value; }
        }

        [HLAAttribute(Name = "sharing",
                     Semantics = "TODO.")]
        public HLAsharingType Sharing
        {
            get { return sharing; }
            set { sharing = value; }
        }

        [HLAAttribute(Name = "sharingNotes",
                     Semantics = "TODO.")]
        public string SharingNotes
        {
            get { return sharingNotes; }
            set { sharingNotes = value; }
        }

        [HLAAttribute(Name = "dimensions",
    Semantics = "TODO.")]
        public string Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        [HLAAttribute(Name = "transportation",
             Semantics = "TODO")]
        public string Transportation
        {
            get { return transportation; }
            set { transportation = value; }
        }

        [HLAAttribute(Name = "order",
             Semantics = "TODO.")]
        public HLAorderType Order
        {
            get { return order; }
            set { order = value; }
        }
    }

    /// <summary>
    /// Represents an HLA interaction parameter.
    /// <code>
    ///  <objectClass name="HLAparameter"
    ///          sharing="PublishSubscribe"
    ///          semantics="Represents an HLA interaction parameter.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAparameter",
                             Sharing = HLAsharingType.PublishSubscribe,
                            Semantics = "Represents an HLA interaction parameter.")]
    public class HLAparameter : HLAReflectionObject
    {
        protected string semantics;
        protected string semanticsNotes;
        protected string dataType;

        [HLAAttribute(Name = "dataType",
                     Semantics = "TODO.")]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        [HLAAttribute(Name = "semanticsNotes",
                      Semantics = "Some notes about object semantics.")]
        public string SemanticsNotes
        {
            get { return semanticsNotes; }
            set { semanticsNotes = value; }
        }


        /// <summary> 
        /// The object semantics.
        ///<code>
        ///  <attribute name="semantics"
        ///  dataType="HLAunicodeString"
        ///  updateType="Static"
        ///  updateCondition="NA"
        ///  ownership="NoTransfer"
        ///  sharing="PublishSubscribe"
        ///  dimensions="NA"
        ///  transportation="HLAreliable"
        ///  order="Receive"
        ///  semantics="The object semantics."/>
        ///</code>
        ///</summary>
        [HLAAttribute(Name = "semantics",
                      Semantics = "The object semantics.")]
        public string Semantics
        {
            get { return semantics; }
            set { semantics = value; }
        }

    }
}
