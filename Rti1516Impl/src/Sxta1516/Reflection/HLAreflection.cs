namespace Sxta.Rti1516.Reflection
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    using Hla.Rti1516;

    ///<summary>
    ///This object class is the root class of all ROM object classes. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAObjectClass(Name = "HLAreflection",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Semantics = "This object class is the root class of all ROM object classes.")]
    public class HLAreflection : IHLAreflection, IHLAobjectRoot
    {

        ///<summary>
        ///Attribute #reflectionName. 
        ///</summary>
        private String reflectionName;

        ///<summary>
        ///Attribute #reflectionNameNotes. 
        ///</summary>
        private String reflectionNameNotes;

        ///<summary> Returns a string representation of this HLAreflection. </summary>
        ///<returns> a string representation of this HLAreflection</returns>
        public override String ToString()
        {
            return "HLAreflection(" +
                     "reflectionName: " + reflectionName + ", " +
                     "reflectionNameNotes: " + reflectionNameNotes +
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the reflectionName field.
        ///</summary>
        [HLAAttribute(Name = "Name",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String Name
        {
            set { reflectionName = value; }
            get { return reflectionName; }
        }


        ///<summary>
        /// Gets/Sets the value of the reflectionNameNotes field.
        ///</summary>
        [HLAAttribute(Name = "NameNotes",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public String NameNotes
        {
            set { reflectionNameNotes = value; }
            get { return reflectionNameNotes; }
        }

        /// <summary>
        /// Creates a new instance from a Xml Element.
        /// </summary>
        public HLAreflection(System.Xml.XmlElement reflectionElement)
        {
            Name = reflectionElement.GetAttribute("name");
            NameNotes = reflectionElement.GetAttribute("nameNotes");
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAreflection()
        {
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
