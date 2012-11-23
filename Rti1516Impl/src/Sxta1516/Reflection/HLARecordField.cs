using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents a HLA record field data.
    /// <code>
    /// <objectClass name="HLASimpleData"
    ///          sharing="PublishSubscribe"
    ///         semantics="Represents a HLA record field data.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLARecordField",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents a HLA record field data.")]
    public class HLARecordField : HLAreflection
    {
        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "dataType",
                      Semantics = "TODO.")]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "dataTypeNotes",
                      Semantics = "TODO.")]
        public string DataTypeNotes
        {
            get { return dataTypeNotes; }
            set { dataTypeNotes = value; }
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

        public HLARecordField()
            : base()
        { }

        public HLARecordField(System.Xml.XmlElement recordFieldElement)
            : base(recordFieldElement)
        {
            DataType = recordFieldElement.GetAttribute("dataType");
            DataTypeNotes = recordFieldElement.GetAttribute("dataTypeNotes");
            Semantics = ReplaceNewLines(recordFieldElement.GetAttribute("semantics"));
            SemanticsNotes = recordFieldElement.GetAttribute("semanticsNotes");
        }

        protected string dataType;
        protected string dataTypeNotes;
        protected string semantics;
        protected string semanticsNotes;
    }
}
