using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents an HLA object class.
    /// <code>
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAobjectClass",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents an HLA object class.")]
    public class HLAObjectClass : HLAreflection
    {

        public HLAObjectClass()
        {
        }

        /// <summary>
        /// Creates a new instance from a Xml element.
        /// <code>
        ///  <xsd:attribute name="name"
        ///                type="xsd:NMTOKEN"
        ///                use="required"/>
        ///                         
        /// <xsd:attribute name="nameNotes"
        ///                type="xsd:IDREFS"/>
        ///                             
        /// <xsd:attribute name="sharing"
        ///                type="sharingType"/>
        ///                         
        /// <xsd:attribute name="sharingNotes"
        ///                type="xsd:IDREFS"/>
        ///                                            
        /// <xsd:attribute name="semantics"
        ///                type="xsd:string"/>
        ///                                            
        /// <xsd:attribute name="semanticsNotes"
        ///                type="xsd:IDREFS"/>
        /// </code>
        /// </summary>
        public HLAObjectClass(System.Xml.XmlElement objectClassElement)
            : base(objectClassElement)
        {
            if (string.IsNullOrEmpty(objectClassElement.GetAttribute("sharing")))
                Sharing = HLAsharingType.Neither;
            else
                Sharing = (HLAsharingType)Enum.Parse(typeof(HLAsharingType), objectClassElement.GetAttribute("sharing"));
            SharingNotes = objectClassElement.GetAttribute("sharingNotes");
            Semantics = ReplaceNewLines(objectClassElement.GetAttribute("semantics"));
            SemanticsNotes = objectClassElement.GetAttribute("semanticsNotes");
        }


        /// <summary>
        /// <code>
        /// <attribute name="sharing"
        ///    dataType="HLAsharingType"
        ///   updateType="Static"
        ///    updateCondition="NA"
        ///    ownership="NoTransfer"
        ///   sharing="PublishSubscribe"
        ///   dimensions="NA"
        ///   transportation="HLAreliable"
        ///   order="Receive"
        ///   semantics="The types of sharing permitted on the object."/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "sharing",
            Semantics = "The types of sharing permitted on the object.")]
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

        protected HLAsharingType sharing;
        protected string sharingNotes;
        protected string semantics;
        protected string semanticsNotes;

    }
}
