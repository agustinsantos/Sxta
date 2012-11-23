using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sxta.Rti1516.Reflection
{
    /// <summary>
    /// <code>
    ///     <objectClass name="HLAreflection"
    ///                 sharing="Neither"
    ///                 semantics="This object class is the root class of all
    ///                            ROM object classes.">
    ///
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAreflection",
                            Sharing = HLAsharingType.Neither,
                            Semantics = "This object class is the root class of all ROM object classes.")]
    public class HLAReflectionObject //: HLAobjectRoot
    {
        protected string name;
        protected string nameNotes;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAReflectionObject()
        {
        }

        /// <summary>
        /// Creates a new instance from a Xml Element.
        /// </summary>
        public HLAReflectionObject(System.Xml.XmlElement reflectionElement)
        {
            Name = reflectionElement.GetAttribute("name");
            NameNotes = reflectionElement.GetAttribute("nameNotes");
        }

        /// <summary>
        /// The name of the reflection object.
        /// <code>
        /// <attribute name="reflectionName"
        ///            dataType="HLAunicodeString"
        ///            updateType="Static"
        ///            updateCondition="NA"
        ///            ownership="NoTransfer"
        ///            sharing="PublishSubscribe"
        ///            dimensions="NA"
        ///            transportation="HLAreliable"
        ///            order="Receive"
        ///            semantics="The name of the reflection object."/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "Name",
                      UpdateType = HLAupdateType.Static,
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer ,
                      Sharing = HLAsharingType.PublishSubscribe,
                      Transportation = "HLAreliable",
                      Order = HLAorderType.Receive,
                      Semantics = "The name of the reflection object.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// Some notes to the name of the reflection object.
        /// <code>
        /// <attribute name="reflectionNameNotes"
        ///            dataType="HLAunicodeString"
        ///            updateType="Static"
        ///            updateCondition="NA"
        ///            ownership="NoTransfer"
        ///            sharing="PublishSubscribe"
        ///            dimensions="NA"
        ///            transportation="HLAreliable"
        ///            order="Receive"
        ///            semantics="Some notes to the name of the reflection object."/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "NameNotes",
                        UpdateType = HLAupdateType.Static,
                       OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                        Sharing = HLAsharingType.PublishSubscribe,
                        Transportation = "HLAreliable",
                        Order = HLAorderType.Receive,
                        Semantics = "Some notes to the name of the reflection object.")]
        public string NameNotes
        {
            get { return nameNotes; }
            set { nameNotes = value; }
        }


        // look for new lines, blanks, tab, etc.
        static Regex regexExpr = new Regex("\\s+");

        /// <summary>
        /// Remove blanks, new lines and other character.
        /// </summary>
        /// <param name="text">the text to be replaced</param>
        /// <returns>the new text</returns>
        protected string ReplaceNewLines(string text)
        {
            return regexExpr.Replace(text, " ");
        }
    }
}
