using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents an HLA enumerated data type.
    /// <code>
    /// <objectClass name="HLAEnumeratedData"
    ///          sharing="PublishSubscribe"
    ///         semantics="Represents an HLA simple data type.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAEnumeratedData",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents an HLA enumerated data type.")]
    public class HLAEnumeratedData : HLAreflection
    {
        ///<summary>
        /// Gets/Sets the value of the representation field.
        ///</summary>
        [HLAAttribute(Name = "representation",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Representation
        {
            set { representation = value; }
            get { return representation; }
        }


        ///<summary>
        /// Gets/Sets the value of the representationNotes field.
        ///</summary>
        [HLAAttribute(Name = "representationNotes",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String RepresentationNotes
        {
            set { representationNotes = value; }
            get { return representationNotes; }
        }


        ///<summary>
        /// Gets/Sets the value of the enumerators field.
        ///</summary>
        [HLAAttribute(Name = "enumerators",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAenumeratorList",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public IList<HLAEnumerator> Enumerators
        {
            set { enumerators = value; }
            get { return enumerators; }
        }


        ///<summary>
        /// Gets/Sets the value of the semantics field.
        ///</summary>
        [HLAAttribute(Name = "semantics",
                        Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Semantics
        {
            set { semantics = value; }
            get { return semantics; }
        }


        ///<summary>
        /// Gets/Sets the value of the semanticsNotes field.
        ///</summary>
        [HLAAttribute(Name = "semanticsNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String SemanticsNotes
        {
            set { semanticsNotes = value; }
            get { return semanticsNotes; }
        }



        /// <summary>
        /// Gets/Sets the value of the underlying native type.
        /// </summary>
        [HLAAttribute(Name = "nativeType",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public System.Type NativeType
        {
            get { return nativeType; }
            set { nativeType = value; }
        }


        /// <summary>
        /// Contructor
        /// </summary>
        public HLAEnumeratedData()
            : base()
        { }

        /// <summary>
        /// Build a new instance using  XML data
        /// </summary>
        /// <param name="enumeratedDataElement"></param>
        public HLAEnumeratedData(System.Xml.XmlElement enumeratedDataElement)
            : base(enumeratedDataElement)
        {
            Representation = enumeratedDataElement.GetAttribute("representation");
            RepresentationNotes = enumeratedDataElement.GetAttribute("representationNotes");
            Semantics = ReplaceNewLines(enumeratedDataElement.GetAttribute("semantics"));
            SemanticsNotes = enumeratedDataElement.GetAttribute("semanticsNotes");

            System.Xml.XmlNodeList nl = enumeratedDataElement.GetElementsByTagName("enumerator");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement enumeratorElement = (System.Xml.XmlElement)nl.Item(i);
                HLAEnumerator enumerator = new HLAEnumerator(enumeratorElement);
                enumerators.Add(enumerator);
            }

        }

        ///<summary> Returns a string representation of this HLAenumeratedDataType. </summary>
        ///<returns> a string representation of this HLAenumeratedDataType</returns>
        public override String ToString()
        {
            return "HLAenumeratedDataType(" +
                     "representation: " + representation + ", " +
                     "representationNotes: " + representationNotes + ", " +
                     "enumerators: " + enumerators + ", " +
                     "semantics: " + semantics + ", " +
                     "semanticsNotes: " + semanticsNotes +
                   ")";
        }

        ///<summary>
        ///Attribute #representation. 
        ///</summary>
        private String representation;

        ///<summary>
        ///Attribute #representationNotes. 
        ///</summary>
        private String representationNotes;

        ///<summary>
        ///Attribute #semantics. 
        ///</summary>
        private String semantics;

        ///<summary>
        ///Attribute #semanticsNotes. 
        ///</summary>
        private String semanticsNotes;

        ///<summary>
        ///Attribute #enumerators. 
        ///</summary>
        protected IList<HLAEnumerator> enumerators = new List<HLAEnumerator>();

        /// <summary>
        /// The C# type for this datatype
        /// </summary>
        protected System.Type nativeType;
    }
}
