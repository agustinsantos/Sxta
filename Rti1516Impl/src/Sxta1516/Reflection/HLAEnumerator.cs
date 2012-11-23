using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents an Enumerator. An element of an enumerated data type.
    /// <code>
    /// <objectClass name="HLAEnumerator"
    ///          sharing="PublishSubscribe"
    ///         semantics="An element of an enumerated data type.">
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLAEnumerator",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "An element of an enumerated data type.")]
    public class HLAEnumerator : HLAReflectionObject
    {
        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "values",
                      Semantics = "TODO.")]
        public string Values
        {
            get { return values; }
            set { values = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "valuesNotes",
                      Semantics = "TODO.")]
        public string ValuesNotes
        {
            get { return valuesNotes; }
            set { valuesNotes = value; }
        }

        public HLAEnumerator()
            : base()
        { }

        public HLAEnumerator(System.Xml.XmlElement enumeratorDataElement)
            : base(enumeratorDataElement)
        {
            Values = enumeratorDataElement.GetAttribute("values");
            ValuesNotes = enumeratorDataElement.GetAttribute("valuesNotes");
        }


        protected string values;
        protected string valuesNotes;

    }
}
