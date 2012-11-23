using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents an HLA fixedRecord Data type.
    /// <code>
    /// <objectClass name="HLAFixedRecordData"
    ///          sharing="PublishSubscribe"
    ///         semantics="Represents an HLA fixedRecord Data type.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAFixedRecordData",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "HLA fixedRecord Data type.")]
    public class HLAFixedRecordData : HLAreflection
    {
        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "encoding",
                      Semantics = "TODO.")]
        public string Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "encodingNotes",
                      Semantics = "TODO.")]
        public string EncodingNotes
        {
            get { return encodingNotes; }
            set { encodingNotes = value; }
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

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "RecordFields",
                      Semantics = "TODO.")]
        public IList<HLARecordField> RecordFields
        {
            get { return fields; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HLAFixedRecordData()
            : base()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixedRecordDataElement"></param>
        public HLAFixedRecordData(System.Xml.XmlElement fixedRecordDataElement)
            : base(fixedRecordDataElement)
        {
            Encoding = fixedRecordDataElement.GetAttribute("encoding");
            EncodingNotes = fixedRecordDataElement.GetAttribute("encodingNotes");
            Semantics = ReplaceNewLines(fixedRecordDataElement.GetAttribute("semantics"));
            SemanticsNotes = fixedRecordDataElement.GetAttribute("semanticsNotes");

            System.Xml.XmlNodeList nl = fixedRecordDataElement.GetElementsByTagName("field");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement enumeratorElement = (System.Xml.XmlElement)nl.Item(i);
                HLARecordField field = new HLARecordField(enumeratorElement);
                fields.Add(field);
            }

        }

        ///<summary> Returns a string representation of this HLAfixedRecordDataType. </summary>
        ///<returns> a string representation of this HLAfixedRecordDataType</returns>
        public override String ToString()
        {
            return "HLAfixedRecordDataType(" +
                     "fields: " + fields + ", " +
                     "encoding: " + encoding + ", " +
                     "encodingNotes: " + encodingNotes + ", " +
                     "semantics: " + semantics + ", " +
                     "semanticsNotes: " + semanticsNotes +
                   ")";
        }

        ///<summary>
        ///Attribute #fields. 
        ///</summary>
        private IList<HLARecordField> fields = new List<HLARecordField>();

        ///<summary>
        ///Attribute #encoding. 
        ///</summary>
        private String encoding;

        ///<summary>
        ///Attribute #encodingNotes. 
        ///</summary>
        private String encodingNotes;

        ///<summary>
        ///Attribute #semantics. 
        ///</summary>
        private String semantics;

        ///<summary>
        ///Attribute #semanticsNotes. 
        ///</summary>
        private String semanticsNotes;


        protected System.Type nativeType;
    }
}
